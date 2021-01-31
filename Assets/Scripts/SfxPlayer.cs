using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] List<Sfx> _soundEffects;

    public void PlaySfx(string name)
    {
        var clip = _soundEffects.Find(sfx => sfx.name == name).clip;
        _audioSource.PlayOneShot(clip);
    }
}

[System.Serializable]
public class Sfx
{
    public string name;
    public AudioClip clip;
}
