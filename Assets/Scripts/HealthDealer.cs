using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDealer : MonoBehaviour
{
    [SerializeField] int health = 10;

    public int GetHealth()
    {
        return health;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
