using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(EntityProvider))]
public class CommunicationSubject : MonoBehaviour, ICommunicationSubject
{
    private readonly Dictionary<Guid, string> personNamesByEntityId = new();

    private EntityProvider entityProvider;
    private PersonCore person;

    public Guid EntityId => entityProvider.Entity.Id;
    public string CharacterName => person.CharacterName;

    private void Awake()
    {
        entityProvider = GetComponent<EntityProvider>();
        person = GetComponent<PersonCore>();
    }

    /// <summary>
    /// Gets the name of some other subject as this person knows
    /// </summary>
    public string GetKnownCharacterName(Guid otherCharacterEntityId)
    {
        if (personNamesByEntityId.TryGetValue(otherCharacterEntityId, out string cachedName))
        {
            return cachedName;
        }

        if (!EntityRegistry.Instance.TryGetEntity(otherCharacterEntityId, out var entity))
        {
            return "[Unknown entity]";
        }

        string name = GetCharacterName(entity);
        
        personNamesByEntityId[otherCharacterEntityId] = name;

        return name;
    }

    private string GetCharacterName(Entity entity)
    {
        return entity?.GetComponent<Character>()?.CharacterName
            ?? entity?.GetComponentInChildren<Character>()?.CharacterName
            ?? "[Unknown person]";
    }
}
