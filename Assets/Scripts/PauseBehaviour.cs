using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
  public bool isPaused = false;

  public void TogglePause() {
    if (isPaused) {
      isPaused = false;
      Time.timeScale = 1;
    } else {
      isPaused = true;
      Time.timeScale = 0;
    }
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.P)) {
      TogglePause();
    }
  }
}
