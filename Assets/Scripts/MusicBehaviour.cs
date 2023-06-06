using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBehaviour : MonoBehaviour
{
  private AudioSource audioSource;

  private void Awake() {
    audioSource = GetComponent<AudioSource>();
  }

  public bool ToggleMusic() {
    if (audioSource.isPlaying) audioSource.Stop();
    else audioSource.Play();

    return audioSource.isPlaying;
  }
}
