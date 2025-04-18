using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public record SpeechSoundData : SoundData
{
    public SpeechSoundData(Guid soundId) : base(soundId)
    {
    }

    public string Message { get; set; }

    /// <summary>
    /// In real life, the recipient is not always determined by speech,
    /// but let's assume that the character addressed
    /// the speech to a certain recipient via some other ways.
    /// null - recipient is not defined
    /// </summary>
    public Guid? RecipientEntityId { get; set; }

    /// <summary>
    /// null - speaker's identity is not defined
    /// </summary>
    public Guid? SpeakerEntityId { get; set; }

    public override string ToDisplayText()
    {
        return $"{Message}";
    }
    // There also can be different speech signs
    // gender, legibility, etc.
}
