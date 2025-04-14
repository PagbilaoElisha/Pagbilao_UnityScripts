using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public AudioSource soundEffect;

    private void Start()
    {
        soundEffect.Play(); // Play the sound effect when the scene loads
    }
}
