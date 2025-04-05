using UnityEngine;

public class DemoCharacterNameGenerator : MonoBehaviour
{
    [SerializeField]
    private TextAsset namesStructuredData;

    [SerializeField]
    private int namesCount = 5;

    private CharacterNameGenerator nameGenerator;
    private PersonNameSchemaGenerator personNameSchemaGenerator;

    private void Start()
    {
        nameGenerator = new(namesStructuredData);
        personNameSchemaGenerator = new PersonNameSchemaGenerator();

        Debug.Log("Generated character names:");
        for (int i = 0; i < namesCount; i++)
        {
            Debug.Log(GenerateName());
        }
    }
    
    public string GenerateName()
    {
        return nameGenerator.Generate(personNameSchemaGenerator.Generate(GenerateParameters()));
    }

    private PersonalityGenerationParameters GenerateParameters()
    {
        // Todo:
        return new() {

        };
    }
}
