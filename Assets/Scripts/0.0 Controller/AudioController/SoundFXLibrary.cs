using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundFXLibrary", menuName = "Audio/Sound FX Library")]
public class SoundFXLibrary : ScriptableObject
{
    public enum SoundFXName
    {
        None,     
        Jump,
        Explosion,
        GunShot,
        Footstep,
        UI_Click,
        Lighting,
        Tornado,
        IceBreak,
        Canon,
        PlayerHurt,
        PlayerDeath
    }

    [Serializable]
    public class SoundEntry
    {
        public SoundFXName soundName;        
        public AudioClip clip;     
    }
    public List<SoundEntry> sounds = new List<SoundEntry>();

    public AudioClip GetClip(SoundFXName name)
    {
        var entry = sounds.Find(s => s.soundName == name);
        return entry != null ? entry.clip : null;
    }
}
