using UnityEngine;

public class FireBehaviour : MonoBehaviour {
  public LevelManagerScriptableObject levelManager;

  private float maxForce = 0.1f;

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

  public float moveSpeed = 0.25f;
  private float movementForce = 100f;

  private Rigidbody2D rb;
  private Collider2D col;
  private SpriteRenderer sprite;

  private void Awake() {
    rb = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    col = GetComponent<Collider2D>();

    maxHP = levelManager.fireMaxHp;
    hp = maxHP / 6;
    hpRegen = hp / 5;
  }

  void Pathfind() {
    var trees = GameObject.FindGameObjectsWithTag("Tree");

    Vector3 pos = transform.position;
    float closestDistance = float.PositiveInfinity;
    GameObject target = null;
    foreach (var tree in trees) {
      var treeDist = (pos - tree.transform.position).sqrMagnitude;
      if (treeDist < closestDistance) {
        target = tree;
        closestDistance = treeDist;
      }
    }

    if (!target) return;

    var treePos = target.transform.position;
    var direction = treePos - transform.position;

    var forceX = Mathf.Min(maxForce, Mathf.Max(-maxForce, direction.x));
    var forceY = Random.value * (maxForce * 2) - maxForce;
    rb.AddForce(new Vector2(forceX, forceY) * movementForce);
  }

  public void ChooseMovement() {
    var velocityX = Random.value * (moveSpeed * 2) - moveSpeed;
    var velocityY = Random.value * (moveSpeed * 2) - moveSpeed;
    rb.velocity = new Vector2(velocityX, velocityY);
  }

  private void Start() {
    ChooseMovement();
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

    rb.velocity = new Vector2(Mathf.Min(moveSpeed, Mathf.Max(-moveSpeed, rb.velocity.x)), Mathf.Min(moveSpeed, Mathf.Max(-moveSpeed, rb.velocity.y)));

    Lifecycle();
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Tree") {
      Destroy(collision.gameObject);
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
