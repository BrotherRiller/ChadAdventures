using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelAudio : MonoBehaviour
{
    [SerializeField] AudioClip Clip;

    private AudioSource Audio;

    private void Start()
    {
        Audio.clip = Clip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Audio.Stop();
        }
    }
}
