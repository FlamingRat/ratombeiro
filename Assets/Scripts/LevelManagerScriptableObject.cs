using UnityEngine;

[CreateAssetMenu]
public class LevelManagerScriptableObject : ScriptableObject {
  public int level = 1;
  public bool levelEnd = false;
  public bool gameOver = false;
  public float splashSizeMultiplier = 1f;
  public float damage = 5f;
}
