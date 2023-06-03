using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBehaviour : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
