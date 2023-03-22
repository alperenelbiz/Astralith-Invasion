using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirerateDealer : MonoBehaviour
{
    [SerializeField] float firerate = 10;

    public float GetFirerate()
    {
        return firerate;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
