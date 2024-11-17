using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/// <summary>
/// An entity considered as a character with its own identity
/// </summary>
public class Character : NetworkBehaviour, ICharacter
{
    public CharacterId GetCharacterId()
    {
        return new CharacterId(netIdentity.netId);
    }
}
