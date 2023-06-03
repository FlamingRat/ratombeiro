using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextLevelMessageBehaviour : MonoBehaviour {
  public LevelManagerScriptableObject levelManager;
  private List<string> sentences = new List<string>();

  void Start() {
    sentences.Add("Ratombeiro salvou a selva novamente!\n\nHoje a noite tem churrasco na clareira.");
    sentences.Add("Ratombeiro apagou todo o fogo!\n\nAgora todos podem dormir tranquilos.");
    sentences.Add("A amea�a do fogo n�o nos assombra mais.\n\nGra�as ao Ratombeiro!");
    sentences.Add("O inc�ndio finalmente se apagou\n\ne s� o que brilha agora � a luz das estrelas.");
  }

  void Update() {
    var text = GetComponent<TextMeshProUGUI>();
    text.SetText(sentences [levelManager.level % sentences.Count]);
  }
}
