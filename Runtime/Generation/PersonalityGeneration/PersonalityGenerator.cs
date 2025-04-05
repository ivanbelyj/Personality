using System;
using UnityEngine;

public enum PersonalityGenerationType
{
    CompletelyRandom,
    Causal
}

[Serializable]
public class PersonalityGenerationParameters
{
    public PersonalityGenerationType generationType;
}

public class PersonalityGenerator
{
    private readonly CharacterNameGenerator characterNameGenerator;
    private readonly PersonNameSchemaGenerator personNameSchemaGenerator;

    public PersonalityGenerator(TextAsset nameStructuredStringJsonAsset)
    {
        personNameSchemaGenerator = new PersonNameSchemaGenerator();
        characterNameGenerator = new(nameStructuredStringJsonAsset);
    }

    public CharacterInfo GeneratePersonInfo(PersonalityGenerationParameters parameters)
    {
        return new() {
            characterName = characterNameGenerator.Generate(
                personNameSchemaGenerator.Generate(parameters))
        };
    }
}
