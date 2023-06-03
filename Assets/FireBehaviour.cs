using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    public LevelManagerScriptableObject levelManager;

    private float maxSpeed = 0.1f;
    private float velocityX;
    private float velocityY;

    private float maxFireHealth = 30f;
    private float spreadHealth = 20f;
    private float hp = 5f;
    private bool hosingDown = false;

    private int maxGenerations = 5;
    private int generation = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        velocityX = Random.value * (maxSpeed * 2) - maxSpeed;
        velocityY = Random.value * (maxSpeed * 2) - maxSpeed;

        rb.velocity = new Vector2(velocityX, velocityY);

        spreadHealth = spreadHealth + ((maxFireHealth - spreadHealth) / (maxGenerations - 1)) * generation;
    }

    public void SetGeneration(int gen)
    {
        generation = gen;
    }

    void Spread()
    {
        var fire1 = Instantiate(this, transform.position + new Vector3(0.25f, 0.25f, 0), transform.rotation);
        var fire2 = Instantiate(this, transform.position + new Vector3(-0.25f, -0.25f, 0), transform.rotation);

        fire1.SetGeneration(generation + 1);
        fire2.SetGeneration(generation + 1);

        Destroy(gameObject);
    }

    private float shakeFrequency = 0.075f;
    private float damageShakeMin = 0.005f;
    private float damageShakeMax = 0.02f;
    private float lastShake = 0f;
    private int shakeDir = 1;

    void Lifecycle()
    {
        if (transform.position.x > 6 || transform.position.x < -6 || transform.position.y > 3 || transform.position.y < -3)
        {
            Destroy(gameObject);
        }

        if (hosingDown) {
            hp -= Time.deltaTime * levelManager.damage;
            sprite.color = new Color(0.8207547f, 0.4824313f, 0.127759f, 1);

            if (lastShake <= 0)
            {
                lastShake = shakeFrequency;
                var shake = Random.value * (damageShakeMax - damageShakeMin) + damageShakeMin;
                transform.position += new Vector3(shake * shakeDir, shake * shakeDir, 0);
                shakeDir *= -1;
            } else
            {
                lastShake -= Time.deltaTime;
            }
        } else if (hp < maxFireHealth)
        {
            hp += Time.deltaTime;
            sprite.color = Color.white;
        }
        
        if (hp >= spreadHealth && generation < maxGenerations)
        {
            Spread();
        }
        else if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        gameObject.transform.rotation = Quaternion.identity;

        if (levelManager.levelEnd || levelManager.gameOver) return;

        Lifecycle();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tree")
        {
            var treePosition = collision.gameObject.transform;
            Destroy(collision.gameObject);
            Instantiate(this, treePosition.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "Boundary")
        {
            var collisionSource = collision.gameObject.transform.position - gameObject.transform.position;

            rb.velocity = new Vector2(collisionSource.x > 0 ? -maxSpeed : maxSpeed, collisionSource.y > 0 ? -maxSpeed : maxSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            hosingDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            hosingDown = false;
        }
    }

    public float GetHPPercent()
    {
        return hp / maxFireHealth;
    }
}
