using System;
using Newtonsoft.Json;
using UnityEngine;

public class CharacterNameGenerator
{
    private readonly StructuredStringGenerator structuredStringGenerator;

    public CharacterNameGenerator(TextAsset jsonAsset)
    {
        if (jsonAsset == null)
        {
            throw new ArgumentNullException(nameof(jsonAsset));
        }

        var data = DeserializeData(jsonAsset);

        structuredStringGenerator = new StructuredStringGenerator(new(data));
    }

    private StructuredStringContainer DeserializeData(TextAsset jsonAsset)
    {
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
            {
                NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
            }
        };

        return JsonConvert.DeserializeObject<StructuredStringContainer>(
            jsonAsset.text,
            settings);
    }

    public string Generate(TaggedStringGenerationSchema schema)
    {
        return structuredStringGenerator.Generate(schema);
    }
}
