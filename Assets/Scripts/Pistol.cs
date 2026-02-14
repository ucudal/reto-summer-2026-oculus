using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    private float bulletLifetime = 5f;

    public AudioClip clip;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        source.PlayOneShot(clip);

        if (rb != null)
        {
            rb.linearVelocity = firePoint.up * bulletSpeed;
        }

        Destroy(bullet, bulletLifetime);
    }
}
