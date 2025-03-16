using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AvrianNameSchemaProvider : ITaggedStringSchemaProvider
{
    public TaggedStringGenerationSchema GetSchema()
    {
        // TODO: implement getting schema using data about a character

        return new TaggedStringGenerationSchema(new List<List<string>> {
            new() { "first-name", Random.value < 0.5 ? "male" : "female" },
            new() { "surname", "lakoar" }
        });
    }
}
