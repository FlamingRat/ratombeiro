using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    public LevelManagerScriptableObject levelManager;

    void Update()
    {
        var text = GetComponent<TextMeshProUGUI>();
        text.SetText("Nível: " + levelManager.level);
    }
}
