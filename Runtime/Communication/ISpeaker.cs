using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpeaker
{
    void SetSpeechVolume(SpeechVolume volume);
    void Speak(SpeechSoundData speechSoundData, SpeechVolume? volume = null);
    float GetSoundIntensity(SpeechVolume speechVolume);
}
