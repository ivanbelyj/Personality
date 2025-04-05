using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PersonalityTrait
{
    public string name;

    [Range(0f, 1f)]
    public float value;
}
