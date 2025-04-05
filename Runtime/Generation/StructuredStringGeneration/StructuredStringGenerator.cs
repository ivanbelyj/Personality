using System;
using System.Text;
using UnityEngine;

public class StructuredStringGenerator
{
    private readonly WeightedStringSelector selector;

    public StructuredStringGenerator(WeightedStringSelector selector)
    {
        this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
    }

    public string Generate(TaggedStringGenerationSchema schema)
    {
        if (schema == null)
        {
            throw new ArgumentNullException(nameof(schema));
        }

        var result = new StringBuilder();
        var tagGroups = schema.GetTagGroups();

        foreach (var tags in tagGroups)
        {
            var item = selector.SelectRandomItemByTags(tags);
            if (item != null)
            {
                result.Append(item).Append(" ");
            }
        }

        return result.ToString().Trim();
    }
}
