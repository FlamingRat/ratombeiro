using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {
  public WildFireBehaviour wildFire;
  public FireSpiritBehaviour fireSpirit;
  public GameObject cheese;
  public LevelManagerScriptableObject levelManager;

  void Start() {
    int fireSpiritCount = Mathf.FloorToInt(levelManager.level / 4);
    int wildFireCount = 2 + levelManager.level - fireSpiritCount;

    for (int i = 0; i < wildFireCount; i++) {
      float x = (Random.value * 12f - 6f);
      float y = (Random.value * 6f - 3f);

      Instantiate(wildFire, new Vector3(x, y, 0), Quaternion.identity);
    }

    for (int i = 0; i < fireSpiritCount; i++) {
      float x = (Random.value * 12f - 6f);
      float y = (Random.value * 6f - 3f);

      Instantiate(fireSpirit, new Vector3(x, y, 0), Quaternion.identity);
    }

    int cheeseAmount = Mathf.FloorToInt(levelManager.level / 6);

    for (int i = 0; i < cheeseAmount; i++) {
      float x = (Random.value * 12f - 6f);
      float y = (Random.value * 6f - 3f);

      Instantiate(cheese, new Vector3(x, y, 0), Quaternion.identity);
    }
  }
}
