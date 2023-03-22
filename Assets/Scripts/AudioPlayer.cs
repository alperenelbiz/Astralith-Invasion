using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Taking Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1f;

    [Header("Taking Shield")]
    [SerializeField] AudioClip shieldCLip;
    [SerializeField][Range(0f, 1f)] float shieldVolume = 1f;

    [Header("Taking Health")]
    [SerializeField] AudioClip healthClip;
    [SerializeField][Range(0f, 1f)] float healthVolume = 1f;

    static AudioPlayer instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayShieldClip()
    {
        PlayClip(shieldCLip, shieldVolume);
    }

    public void PlayHealthClip()
    {
        PlayClip(healthClip, healthVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
