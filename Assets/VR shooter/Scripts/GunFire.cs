using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public float velocity;
    public GameObject bulletPrefab;
    public Transform barrel;
    public AudioSource audioSource;
    public ParticleSystem ps;

    public void Fire() {
        GameObject spawnedBullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = velocity * barrel.forward;
        audioSource.Play();
        Animator anim;//don't worry if no animations
        if (TryGetComponent<Animator>(out anim))
        {
            anim.SetTrigger("Fire");
        }
        if(ps != null)
        {
            ps.Play();
        }
        Destroy(spawnedBullet, 2f);
    }
}
