using UnityEngine;

[CreateAssetMenu]
public class LevelManagerScriptableObject : ScriptableObject {
  public int level = 1;
  public bool levelEnd = false;
  public bool gameOver = false;
  public float splashSizeMultiplier = 1f;
  public float damage = 5f;
  public float fireMaxHp = 30f;
  public float fireStartingHpPercent = 16.6f;
  public float fireHpRegenPercent = 3.3f;
}
