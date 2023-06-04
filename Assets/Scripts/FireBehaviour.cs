using UnityEngine;

public class FireBehaviour : MonoBehaviour {
  public LevelManagerScriptableObject levelManager;

  private float maxSpeed = 0.1f;
  private float velocityX;
  private float velocityY;

  public float maxHP = 30f;
  public float hp = 5f;
  public float hpRegen = 1;
  public bool hosingDown = false;
  public Color baseColour = Color.white;

  private float shakeFrequency = 0.075f;
  private float damageShakeMin = 0.005f;
  private float damageShakeMax = 0.02f;
  private float lastShake = 0f;
  private int shakeDir = 1;

  private Rigidbody2D rb;
  private SpriteRenderer sprite;

  private void Awake() {
    rb = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
  }

  private void Start() {
    velocityX = Random.value * (maxSpeed * 2) - maxSpeed;
    velocityY = Random.value * (maxSpeed * 2) - maxSpeed;
    rb.AddForce(new Vector2(velocityX, velocityY) * 25);

    transform.localScale = new Vector3(1, 1, 1);
    maxHP = levelManager.fireMaxHp;
    hp = maxHP / 6;
    hpRegen = hp / 5;
  }

  void Lifecycle() {
    if (hosingDown) {
      hp -= Time.deltaTime * levelManager.damage;
      sprite.color = new Color(0.8207547f, 0.4824313f, 0.127759f, 1);

      if (lastShake <= 0) {
        lastShake = shakeFrequency;
        var shake = Random.value * (damageShakeMax - damageShakeMin) + damageShakeMin;
        transform.position += new Vector3(shake * shakeDir, shake * shakeDir, 0);
        shakeDir *= -1;
      } else {
        lastShake -= Time.deltaTime;
      }
    } else if (hp < maxHP) {
      hp += Time.deltaTime * hpRegen;

      sprite.color = baseColour;
    }
    
    if (hp <= 0) {
      Destroy(gameObject);
    }
  }

  void Update() {
    gameObject.transform.rotation = Quaternion.identity;

    if (levelManager.levelEnd || levelManager.gameOver) return;

    Lifecycle();
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Tree") {
      var treePosition = collision.gameObject.transform;
      Destroy(collision.gameObject);
      Instantiate(this, treePosition.position, Quaternion.identity);
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Water") {
      hosingDown = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision) {
    if (collision.gameObject.tag == "Water") {
      hosingDown = false;
    }
  }

  public float GetHPPercent() {
    return hp / maxHP;
  }

  public float GetHP() {
    return hp;
  }

  public float GetMaxHP() {
    return maxHP;
  }
}
