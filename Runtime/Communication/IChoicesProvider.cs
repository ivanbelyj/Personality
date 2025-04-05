using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for providing available communication commands
/// in the given situation
/// </summary>
public interface IChoicesProvider
{
    IEnumerable<CommunicationCommand> GetChoices(Guid? recipientEntityId);
}
