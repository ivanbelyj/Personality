using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

/// <summary>
/// An entity considered as a character
/// </summary>
public class Character : MonoBehaviour
{
    [SerializeField]
    protected CharacterInfo characterInfo;

    public string CharacterName => characterInfo.characterName;
    public CharacterInfo CharacterInfo { get => characterInfo; set => characterInfo = value; }
}
