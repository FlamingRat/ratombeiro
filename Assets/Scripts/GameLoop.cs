using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour {
  public LevelManagerScriptableObject levelManager;
  public GameObject nextLevelUI;
  public GameObject gameOverUI;
  public GameObject upgradeUI;

  private bool hasUpgraded = false;

  void Start() {
    levelManager.levelEnd = false;
    levelManager.gameOver = false;
  }

  void Update() {
    var fires = GameObject.FindGameObjectsWithTag("Fire");
    if (fires.Length == 0) {
      levelManager.levelEnd = true;
      nextLevelUI.SetActive(true);
      if (!hasUpgraded) upgradeUI.SetActive(true);
    }

    var trees = GameObject.FindGameObjectsWithTag("Tree");
    if (trees.Length == 0) {
      levelManager.gameOver = true;
      gameOverUI.SetActive(true);
    }
  }

  private void ResumeGame() {
    nextLevelUI.SetActive(false);
    gameOverUI.SetActive(false);
    levelManager.levelEnd = false;
    levelManager.gameOver = false;
  }

  public void NextLevel() {
    ResumeGame();
    levelManager.level += 1;
    SceneManager.LoadScene(1);
  }

  public void Restart() {
    ResumeGame();
    levelManager.level = 1;
    SceneManager.LoadScene(1);
  }

  public void UpgradeDamage() {
    levelManager.damage *= 1.05f;
    upgradeUI.SetActive(false);
    hasUpgraded = true;
  }

  public void UpgradeSplash() {
    levelManager.splashSizeMultiplier *= 1.05f;
    upgradeUI.SetActive(false);
    hasUpgraded = true;
  }
}
