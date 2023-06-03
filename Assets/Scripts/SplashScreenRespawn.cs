using UnityEngine;

public class SplashScreenRespawn : MonoBehaviour {
  void Update() {
    GetComponent<Rigidbody2D>().velocity = new Vector3(0, -5, 0);

    if (transform.position.y < -5f) {
      transform.position = new Vector3(transform.position.x, 5, 0);
    }
  }
}
