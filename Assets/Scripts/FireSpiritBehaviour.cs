using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritBehaviour : MonoBehaviour {
  public WildFireBehaviour wildFire;

  private readonly float timeBetweenShots = 15f;
  private float timeToShootSeconds = 0;
  private FireBehaviour fire;

  private void Awake() {
    fire = GetComponent<FireBehaviour>();
    fire.baseColour = Color.green;
    fire.moveSpeed = 0;
  }

  private void Start() {
    fire.maxHP *= 1.5f;
    fire.hp = fire.maxHP;
    fire.hpRegen = 0;
    timeToShootSeconds = timeBetweenShots;
  }

  void Update() {
    if (timeToShootSeconds <= 0) {
      timeToShootSeconds = timeBetweenShots;
      Shoot();
    }
    timeToShootSeconds -= Time.deltaTime;
  }

  void Shoot() {
    var player = GameObject.FindGameObjectsWithTag("Player") [0];
    var dir = player.transform.position - transform.position;
    var f = Instantiate(wildFire, transform.position + new Vector3(Mathf.Max(-1, Mathf.Min(1, dir.x)), Mathf.Max(-1, Mathf.Min(1, dir.y)), 0), transform.rotation);
    f.GetComponent<Rigidbody2D>().AddForce(dir * 30);
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "FireSpirit") {
      var fire = collision.gameObject.GetComponent<FireBehaviour>();
      var isOlder = fire.GetInstanceID() < GetInstanceID();
      if (!isOlder) return;

      var newMaxHP = fire.maxHP + fire.GetMaxHP();
      fire.maxHP = newMaxHP;
      fire.hp += fire.GetHP();
      Destroy(collision.gameObject);
    }
  }
}
