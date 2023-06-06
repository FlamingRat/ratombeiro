using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildFireBehaviour : MonoBehaviour {
  public LevelManagerScriptableObject levelManager;

  private float timeToSpreadSeconds;
  private int maxOrganicGenerations = 4;
  private int generation = 1;

  private Rigidbody2D rb;
  private FireBehaviour fire;

  private void Awake() {
    rb = GetComponent<Rigidbody2D>();
    fire = GetComponent<FireBehaviour>();
    fire.baseColour = Color.white;
    timeToSpreadSeconds = levelManager.wildfireSpreadTime;
  }

  void Update() {
    if (timeToSpreadSeconds <= 0f && generation < maxOrganicGenerations) {
      Spread();
      timeToSpreadSeconds = levelManager.wildfireSpreadTime;
    }
    timeToSpreadSeconds -= Time.deltaTime;
  }
  public void SetInitialValues(int gen, float hp) {
    generation = gen;
    fire.hp = hp;
  }

  void Spread() {
    var fire1 = Instantiate(this, transform.position, transform.rotation);
    var fire2 = Instantiate(this, transform.position, transform.rotation);

    fire1.SetInitialValues(generation + 1, fire.hp * 0.75f);
    fire2.SetInitialValues(generation + 1, fire.hp * 0.75f);

    Destroy(gameObject);
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Tree") {
      Spread();
    }
  }
}
