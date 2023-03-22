using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projecttilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 2f;
    [SerializeField] float punishmentRate = 1f;
    float lockRate;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        lockRate = baseFiringRate;
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        FirerateDealer firerateDealer = collision.GetComponent<FirerateDealer>();
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            baseFiringRate += punishmentRate;

            if (baseFiringRate < lockRate)
            {
                baseFiringRate = lockRate;
            }
        }

        if (firerateDealer != null)
        {
            baseFiringRate -= firerateDealer.GetFirerate();
            firerateDealer.Hit();

            if (baseFiringRate < 0.1f)
            {
                baseFiringRate = 0.1f;
            }
        }
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projecttilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
