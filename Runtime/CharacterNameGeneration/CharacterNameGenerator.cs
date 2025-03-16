using System;
using Newtonsoft.Json;
using UnityEngine;

public class CharacterNameGenerator
{
    private readonly StructuredStringGenerator structuredStringGenerator;
    private readonly ITaggedStringSchemaProvider generationSchemaProvider;

    public CharacterNameGenerator(
        TextAsset jsonAsset,
        ITaggedStringSchemaProvider generationSchemaProvider)
    {
        if (jsonAsset == null)
        {
            throw new ArgumentNullException(nameof(jsonAsset));
        }
            
        if (generationSchemaProvider == null)
        {
            throw new ArgumentNullException(nameof(generationSchemaProvider));
        }

        var data = DeserializeData(jsonAsset);

        structuredStringGenerator = new StructuredStringGenerator(new(data));
        this.generationSchemaProvider = generationSchemaProvider;
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

    public string Generate()
    {
        var schema = generationSchemaProvider.GetSchema();
        return structuredStringGenerator.Generate(schema);
    }
}
