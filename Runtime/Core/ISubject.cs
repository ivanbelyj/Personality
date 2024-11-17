using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject : ICharacter
{
    string GetKnownCharacterName(CharacterId otherCharacterId);
}
