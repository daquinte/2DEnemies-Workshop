using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// On trigger, this class plays a clip of sound once.
/// </summary>
public class SoundOnTrigger : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float volumen;

    public AudioClip sound;


    private void OnTriggerEnter(Collider other)
    {
        AudioSource.PlayClipAtPoint(sound, transform.position, volumen);
    }
}
