using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
  public LevelManagerScriptableObject levelManager;
  public GameObject waterObject;
  public GameObject stepSoundObject;
  public GameObject sprintSoundObject;
  public CheeseBehaviour cheeseSoundObject;

  private Rigidbody2D rb;
  private Vector2 fireDirection = new Vector2(0, 0);
  private float isOnFireCooldown = 0;
  private float fireStunSeconds = 0.5f;

  private float playerWalkSpeed = 1.5f;
  private float playerSprintSpeed = 3f;
  private float playerMaxSprintSeconds = 3f;
  private float playerCurrentSprintStamina = 3f;

  private float waterMaxCapacity = 6;
  private float waterCurrentCapacitySeconds = 6;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
  }

  void MovePlayer() {
    if (isOnFireCooldown > 0) {
      rb.velocity = new Vector2(fireDirection.x * 25, fireDirection.y * 25);
      isOnFireCooldown -= Time.deltaTime;
      stepSoundObject.SetActive(false);
      sprintSoundObject.SetActive(false);
      return;
    }

    if (rb.velocity.x == 0 && rb.velocity.y == 0) {
      stepSoundObject.SetActive(false);
      sprintSoundObject.SetActive(false);
    }

    float speed;
    bool isRunning = Input.GetKey(KeyCode.LeftShift) && playerCurrentSprintStamina > 0;
    if (isRunning) {
      speed = playerSprintSpeed;
    } else {
      speed = playerWalkSpeed;
    }

    if (Input.GetKey(KeyCode.W)) {
      rb.velocity = new Vector2(rb.velocity.x, speed);

      if (isRunning) {
        playerCurrentSprintStamina -= Time.deltaTime;
        stepSoundObject.SetActive(false);
        sprintSoundObject.SetActive(true);
      } else {
        sprintSoundObject.SetActive(false);
        stepSoundObject.SetActive(true);
      }
    } else if (Input.GetKey(KeyCode.S)) {
      rb.velocity = new Vector2(rb.velocity.x, -speed);

      if (isRunning) {
        playerCurrentSprintStamina -= Time.deltaTime;
        stepSoundObject.SetActive(false);
        sprintSoundObject.SetActive(true);
      } else {
        sprintSoundObject.SetActive(false);
        stepSoundObject.SetActive(true);
      }
    } else {
      rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    if (Input.GetKey(KeyCode.A)) {
      rb.velocity = new Vector2(-speed, rb.velocity.y);

      if (isRunning) {
        playerCurrentSprintStamina -= Time.deltaTime;
        stepSoundObject.SetActive(false);
        sprintSoundObject.SetActive(true);
      } else {
        sprintSoundObject.SetActive(false);
        stepSoundObject.SetActive(true);
      }
    } else if (Input.GetKey(KeyCode.D)) {
      rb.velocity = new Vector2(speed, rb.velocity.y);

      if (isRunning) {
        playerCurrentSprintStamina -= Time.deltaTime;
        stepSoundObject.SetActive(false);
        sprintSoundObject.SetActive(true);
      } else {
        sprintSoundObject.SetActive(false);
        stepSoundObject.SetActive(true);
      }
    } else {
      rb.velocity = new Vector2(0, rb.velocity.y);
    }
  }

  void FaceCursor() {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
  }

  void SprayWater() {
    if (levelManager.gameOver || levelManager.levelEnd) {
      waterObject.SetActive(false);
      return;
    }

    var waterAvailable = waterCurrentCapacitySeconds > 0f;

    var waterKeyDown = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space);
    if (waterKeyDown && waterAvailable && !levelManager.gameOver && !levelManager.levelEnd) {
      waterObject.SetActive(true);
    }

    var waterKeysUp = (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space));
    if (waterKeysUp && !waterKeyDown || !waterAvailable) {
      waterObject.SetActive(false);
    }
  }

  void Update() {
    MovePlayer();
    FaceCursor();

    if (waterObject.activeSelf) {
      waterCurrentCapacitySeconds -= Time.deltaTime;
    } else if (waterCurrentCapacitySeconds < waterMaxCapacity) {
      waterCurrentCapacitySeconds += Time.deltaTime * 2;
    }

    SprayWater();
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.tag == "Fire") {
      fireDirection = gameObject.transform.position - collision.gameObject.transform.position;
      isOnFireCooldown = fireStunSeconds;

      var burnSound = GetComponent<AudioSource>();
      burnSound.Play();
    }
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.gameObject.tag == "Cheese") {
      playerCurrentSprintStamina = playerMaxSprintSeconds;
      cheeseSoundObject.PlaySound();
      Destroy(collision.gameObject);
    }
  }

  public float GetWaterPercent() {
    return waterCurrentCapacitySeconds / waterMaxCapacity;
  }

  public float GetStaminaPercent() {
    return playerCurrentSprintStamina / playerMaxSprintSeconds;
  }
}
