using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public AudioClip[] randomFiringSounds;
    private AudioSource source;
    [Range(0.1f, 0.5f)]
    public float volumeChangeMuliplier = 0.2f;

    [Range(0.1f, 0.5f)]
    public float pitchChangeMuliplier = 0.2f;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void OnFire()
    {
        Shoot();
    }

    private void playRandomGunSound()
    {
        source.clip = randomFiringSounds[Random.Range(0, randomFiringSounds.Length)];
        source.volume = Random.Range(0.1f - volumeChangeMuliplier, 0.5f);
        source.pitch = Random.Range(1 - pitchChangeMuliplier, 1 + pitchChangeMuliplier);
        source.PlayOneShot(source.clip);
    }

    private void Shoot()
    {
        playRandomGunSound();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
