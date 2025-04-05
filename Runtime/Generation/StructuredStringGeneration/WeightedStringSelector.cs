using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeightedStringSelector
{
    private StructuredStringContainer structuredStringContainer;

    public WeightedStringSelector(StructuredStringContainer data)
    {
        structuredStringContainer = data;
    }

    public string SelectRandomItemByTags(List<string> tags)
    {
        var filteredGroups = structuredStringContainer.Groups
            .Where(g => tags.All(t => g.Tags.Contains(t)))
            .ToList();

        if (filteredGroups.Count == 0)
        {
            return null;
        }

        var weightedItemGroups = filteredGroups
            .SelectMany(g => g.ItemGroups)
            .ToList();

        return SelectRandomItemFromGroups(weightedItemGroups);
    }

    private string SelectRandomItemFromGroups(List<WeightedStringGroup> weightedGroups)
    {
        var items = new List<string>();
        var weights = new List<float>();

        foreach (var group in weightedGroups)
        {
            items.AddRange(group.Items);
            weights.AddRange(Enumerable.Repeat(group.Weight, group.Items.Count));
        }

        return RandomUtils.GetRandomWeighted(items, weights);
    }
}
