using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Accepts tagged string generation schema in format
/// 'tag-name_tag-name-1 other-tag_other-tag-1 ...'
/// </summary>
/// <remarks>
/// Example: 'first-name_lakoar_female last-name_lakoar'
/// </remarks>
public class TaggedStringGenerationSchema
{
    private readonly List<List<string>> tagGroups;

    public TaggedStringGenerationSchema(List<List<string>> tagGroups)
    {
        this.tagGroups = tagGroups;
    }

    public TaggedStringGenerationSchema(string schema)
    {
        if (string.IsNullOrEmpty(schema))
        {
            throw new ArgumentException("Schema cannot be null or empty.");
        }

        var parts = schema.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        tagGroups = new List<List<string>>();

        foreach (var part in parts)
        {
            var tags = part
                .Split(
                    new[] { '_' },
                    StringSplitOptions.RemoveEmptyEntries)
                .ToList();
            tagGroups.Add(tags);
        }
    }

    public List<List<string>> GetTagGroups()
    {
        return tagGroups;
    }

    public override string ToString()
    {
        return string.Join(" ", tagGroups.Select(tagGroup => string.Join("_", tagGroup)));
    }
}
