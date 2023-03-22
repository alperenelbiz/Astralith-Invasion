using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField] GameObject healthPrefab;
    [SerializeField] GameObject fireratePrefab;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] float ballSpeed = 10f;
    [SerializeField] float ballLifeTime = 5f;

    public void Drop()
    {
        int a = Random.Range(1, 10);

        if (a == 1)
        {
            DropHealth();
        }

        else if (a == 2)
        {
            DropFire();
        }

        else if (a == 3)
        {
            DropShield();
        }
    }

    void DropHealth()
    {
        GameObject instance = Instantiate(healthPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = transform.up * ballSpeed;
        }

        Destroy(instance, ballLifeTime);
    }

    void DropFire()
    {
        GameObject instance = Instantiate(fireratePrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = transform.up * ballSpeed;
        }

        Destroy(instance, ballLifeTime);
    }

    void DropShield()
    {
        GameObject instance = Instantiate(shieldPrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = transform.up * ballSpeed;
        }

        Destroy(instance, ballLifeTime);
    }
}
