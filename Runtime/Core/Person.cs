using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

/// <summary>
/// Person is a character with personality and intelligence
/// </summary>
public class Person : Character, ISubject
{
    [SerializeField]
    private string personName;
    public string PersonName {
        get => personName;
        set => personName = value;
    }

    /// <summary>
    /// Gets the name of some other subject as this person knows
    /// </summary>
    public string GetKnownCharacterName(CharacterId id) {
        // Todo:
        NetworkIdentity identity = null;
        try {
            identity = NetworkServer.spawned[id.Value];
        } catch (KeyNotFoundException) {
            Debug.LogWarning("networkIdentity is not found by " + id);
        }
        
        return GetPersonName(identity) ?? id.ToString();
    }

    private string GetPersonName(NetworkIdentity identity) {
        return identity.GetComponent<Person>()?.PersonName
            ?? identity?.GetComponentInChildren<Person>()?.PersonName;
    }
}
