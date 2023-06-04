using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritBehaviour : MonoBehaviour {
  public WildFireBehaviour wildFire;

  private readonly float timeBetweenShots = 10f;

  private float timeToShootSeconds;
  private FireBehaviour fire;

  private void Start() {
    fire = GetComponent<FireBehaviour>();
    fire.baseColour = Color.green;

    timeToShootSeconds = timeBetweenShots;
  }

  void Update() {
    if (timeToShootSeconds <= 0) Shoot();
    timeToShootSeconds -= Time.deltaTime;
  }

  void Shoot() {
    timeToShootSeconds = timeBetweenShots;
    var player = GameObject.FindGameObjectsWithTag("Player") [0];
    var dir = player.transform.position - transform.position;
    var force = new Vector2(dir.x > 0 ? 1 : -1, dir.y > 0 ? 1 : -1);
    var f = Instantiate(wildFire, transform.position + new Vector3(dir.x * 0.1f, dir.y * 0.1f, 0), transform.rotation);
    f.GetComponent<Rigidbody2D>().AddForce(force * 50);
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "FireSpirit") {
      var fire = collision.gameObject.GetComponent<FireBehaviour>();
      var isOlder = fire.GetInstanceID() < GetInstanceID();
      if (!isOlder) return;

      var newMaxHP = fire.maxHP + fire.GetMaxHP();
      transform.localScale = new Vector3(1, 1, 1) * Mathf.Max(transform.localScale.x, fire.transform.localScale.x, Mathf.Min(newMaxHP / fire.maxHP, 1.5f));
      fire.maxHP = newMaxHP;
      fire.hp += fire.GetHP();
      Destroy(collision.gameObject);
    }
  }
}
