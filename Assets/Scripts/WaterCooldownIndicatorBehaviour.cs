using UnityEngine;

public class WaterCooldownIndicatorBehaviour : MonoBehaviour {
  public PlayerBehaviour player;

  void Update() {
    var waterPercent = player.GetWaterPercent();
    transform.localScale = new Vector3(0.5f * waterPercent, transform.localScale.y, transform.localScale.z);
    transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.35f, player.transform.position.z);
  }
}
