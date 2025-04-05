using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public abstract class PersonBase : PersonCore
{
    public abstract IEnumerable<PersonalityTrait> GetAllTraits();
}
