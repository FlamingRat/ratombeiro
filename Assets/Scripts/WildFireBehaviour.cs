using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildFireBehaviour : MonoBehaviour {
  private float timeToSpreadSeconds = 15f;
  private int maxOrganicGenerations = 5;
  private int generation = 1;

  private Rigidbody2D rb;
  private FireBehaviour fire;

  private void Awake() {
    rb = GetComponent<Rigidbody2D>();
    fire = GetComponent<FireBehaviour>();
    fire.baseColour = Color.white;
  }

  void Update() {
    if (timeToSpreadSeconds <= 0f) {
      Spread();
    }
    timeToSpreadSeconds -= Time.deltaTime;
  }
  public void SetInitialValues(int gen, float hp, Vector2 velocity) {
    generation = gen;
    fire.hp = hp;
    rb.velocity = velocity;
  }

  void Spread() {
    if (generation >= maxOrganicGenerations) return;

    timeToSpreadSeconds = 15f;

    var fire1 = Instantiate(this, transform.position, transform.rotation);
    var fire2 = Instantiate(this, transform.position, transform.rotation);

    fire1.SetInitialValues(generation + 1, fire.hp * 0.75f, rb.velocity);
    fire2.SetInitialValues(generation + 1, fire.hp * 0.75f, rb.velocity * new Vector2(-1, -1));

    Destroy(gameObject);
  }
}
