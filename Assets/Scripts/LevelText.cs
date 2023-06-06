using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour {
  public LevelManagerScriptableObject levelManager;

  void Update() {
    var text = GetComponent<TextMeshProUGUI>();
    text.SetText(levelManager.level.ToString());
  }
}
