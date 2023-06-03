using UnityEngine;

public class FireHPBehaviour : MonoBehaviour {
  public FireBehaviour fire;

  void Update() {
    var hp = fire.GetHPPercent();
    transform.localScale = new Vector3(0.5f * hp, transform.localScale.y, transform.localScale.z);
  }
}
