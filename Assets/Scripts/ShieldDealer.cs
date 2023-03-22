using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDealer : MonoBehaviour
{
    [SerializeField] int shield = 10;

    public int GetShield()
    {
        return shield;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
