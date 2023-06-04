using UnityEngine;

public class FireExtinguishSoundBehaviour : MonoBehaviour {
  private int fireCount = 0;

  void Update() {
    var fires = GameObject.FindGameObjectsWithTag("Fire");
    var fireSpirits = GameObject.FindGameObjectsWithTag("FireSpirit");
    var newCount = fires.Length + fireSpirits.Length;
    if (newCount < fireCount) {
      GetComponent<AudioSource>().Play();
    }

    fireCount = fires.Length + fireSpirits.Length;
  }
}
