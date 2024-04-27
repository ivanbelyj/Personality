using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some object that can speak or reproduce words
/// </summary>
[RequireComponent(typeof(ISoundEmitter))]
public class Speaker : MonoBehaviour, ISpeaker
{
    public float voiceVolumeMultiplier = 1f;
    private ISoundEmitter soundEmitter;
    
    [SerializeField]
    private SpeechVolume initialSpeechVolume = SpeechVolume.Normal;

    public ISoundEmitter SoundEmitter => soundEmitter;

    public void Awake()
    {
        soundEmitter = GetComponent<ISoundEmitter>();
        SetSpeechVolume(initialSpeechVolume);
    }

    public void SetSpeechVolume(SpeechVolume speechVolume) {
        soundEmitter.SoundIntensity = GetSoundIntensity(speechVolume);
    }

    public void Speak(SpeechSoundData speechSoundData,
        SpeechVolume? volume = null) {
        bool shouldRestorePrevSoundIntensity = false;
        float prevSoundIntensity = soundEmitter.SoundIntensity;
        if (volume != null) {
            shouldRestorePrevSoundIntensity = true;
            soundEmitter.SoundIntensity = GetSoundIntensity(volume.Value);
        }
        
        soundEmitter.Emit(speechSoundData);

        if (shouldRestorePrevSoundIntensity) {
            soundEmitter.SoundIntensity = prevSoundIntensity;
        }
    }

    public float GetSoundIntensity(SpeechVolume speechVolume) {
        return speechVolume switch {
            SpeechVolume.Whisper => 5f,
            SpeechVolume.Normal => 100f,
            SpeechVolume.Shout => 10_000f,
            _ => throw new ArgumentException("Unknown speech volume")
        } * voiceVolumeMultiplier;
    }
}
