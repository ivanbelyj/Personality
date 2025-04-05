using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommunicationSubject : IEntity
{
    string GetKnownCharacterName(Guid otherCharacterEntityId);
    string CharacterName { get; }
}
