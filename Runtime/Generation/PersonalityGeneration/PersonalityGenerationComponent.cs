using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PersonBase))]
public class PersonalityGenerationComponent : MonoBehaviour
{
    [SerializeField]
    private PersonalityGenerationParameters parameters;

    [SerializeField]
    private TextAsset nameStructuredStringJsonAsset;

    private PersonBase person;
    private PersonalityGenerator personalityGenerator;
    
    private void Awake()
    {
        person = GetComponent<PersonBase>();
        personalityGenerator = new(nameStructuredStringJsonAsset);
        person.CharacterInfo = personalityGenerator.GeneratePersonInfo(parameters);
    }
}
