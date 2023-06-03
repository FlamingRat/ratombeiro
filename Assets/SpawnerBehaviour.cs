using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    public GameObject fire;
    public GameObject cheese;
    public LevelManagerScriptableObject levelManager;

    void Start()
    {
        int fireAmount = 2 + levelManager.level;

        for (int i = 0; i < fireAmount; i++)
        {
            float x = (Random.value * 12f - 6f);
            float y = (Random.value * 6f - 3f);

            Instantiate(fire, new Vector3(x, y, 0), Quaternion.identity);
        }

        int cheeseAmount = Mathf.FloorToInt(levelManager.level / 5);

        for (int i = 0; i < cheeseAmount; i++)
        {
            float x = (Random.value * 12f - 6f);
            float y = (Random.value * 6f - 3f);

            Instantiate(cheese, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}
