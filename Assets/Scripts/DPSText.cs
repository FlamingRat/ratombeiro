using TMPro;
using UnityEngine;

public class DPSText : MonoBehaviour {
  public LevelManagerScriptableObject levelManager;

  private void Update() {
    var text = GetComponent<TextMeshProUGUI>();
    float dps = Mathf.Ceil(levelManager.damage * 10f) / 10f;
    text.SetText(dps.ToString());
  }
}
