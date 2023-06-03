using UnityEngine;

public class StaminaIndicatorBehaviour : MonoBehaviour {
  public PlayerBehaviour player;
  void Update() {
    var staminaPercent = player.GetStaminaPercent();
    transform.localScale = new Vector3(0.5f * staminaPercent, transform.localScale.y, transform.localScale.z);
    transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.4f, player.transform.position.z);
  }
}
