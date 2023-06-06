using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggleBehaviour : MonoBehaviour
{
  public MusicBehaviour musicBehaviour;
  public Sprite musicOnSprite;
  public Sprite musicOffSprite;
  public Image musicBtnImage;

  public void ToggleMusic() {
    var isPlaying = musicBehaviour.ToggleMusic();

    if (isPlaying) {
      musicBtnImage.sprite = musicOnSprite;
    } else {
      musicBtnImage.sprite = musicOffSprite;
    }
  }
}
