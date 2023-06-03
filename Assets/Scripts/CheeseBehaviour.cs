using UnityEngine;

public class CheeseBehaviour : MonoBehaviour {
  private AudioSource audioSource;

  public void PlaySound() {
    audioSource.Play();
  }

  private void Start() {
    audioSource = GetComponent<AudioSource>();
  }
}
