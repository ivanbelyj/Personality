using System.Collections.Generic;
using UnityEngine;

public class PersonNameSchemaGenerator
{
    public TaggedStringGenerationSchema Generate(
        PersonalityGenerationParameters parameters)
    {
        // TODO: implement
        return new TaggedStringGenerationSchema(new List<List<string>> {
            new() { "first-name", Random.value < 0.5 ? "male" : "female" },
            new() { "surname", "lakoar" }
        });
    }
}
