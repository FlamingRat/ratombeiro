using UnityEngine;

public class WaterBehaviour : MonoBehaviour {
  public LevelManagerScriptableObject levelManager;

  void Start() {
    transform.localScale = new Vector3(transform.localScale.x * levelManager.splashSizeMultiplier, transform.localScale.y * levelManager.splashSizeMultiplier, 1);
  }
}
