using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeal : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] SpriteRenderer shipRenderer;
    [SerializeField] Sprite normalShip;
    [SerializeField] Sprite shieldShip;

    [Header("Values")]
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;
    int shield = 0;

    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;
    Upgrader upgrader;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        upgrader = FindObjectOfType<Upgrader>();
        shipRenderer = GetComponent<SpriteRenderer>();
        shipRenderer.sprite = normalShip;
    }

    void Update()
    {
        if (shield == 0)
        {
            shipRenderer.sprite = normalShip;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        HealthDealer healthDealer = collision.GetComponent<HealthDealer>();
        ShieldDealer shieldDealer = collision.GetComponent<ShieldDealer>();

        if (shieldDealer != null)
        {
            shipRenderer.sprite = shieldShip;
            shield += shieldDealer.GetShield();
            shieldDealer.Hit();
        }

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.Hit();
        }

        else if (healthDealer != null)
        {
            //audio ekle
            health += healthDealer.GetHealth();
            healthDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        if (shield > 0 && damage > shield)
        {
            shield -= damage;
            health += shield;
            shield = 0;
        }

        else if (shield > 0)
        {
            shield -= damage;
        }

        else
        {
            health -= damage;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
            upgrader.Drop();
        }

        else
        {
            levelManager.LoadGameOver();
        }

        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    public int GetHealth()
    {
        return health;
    }
}
