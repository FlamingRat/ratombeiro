using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {
  public WildFireBehaviour wildFire;
  public FireSpiritBehaviour fireSpirit;
  public GameObject cheese;
  public LevelManagerScriptableObject levelManager;

  void Start() {
    int ancientFireAmount = Mathf.FloorToInt(levelManager.level / 5);
    int fireAmount = 2 + levelManager.level - ancientFireAmount;

    for (int i = 0; i < fireAmount; i++) {
      float x = (Random.value * 12f - 6f);
      float y = (Random.value * 6f - 3f);

      Instantiate(wildFire, new Vector3(x, y, 0), Quaternion.identity);
    }

    for (int i = 0; i < ancientFireAmount; i++) {
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
