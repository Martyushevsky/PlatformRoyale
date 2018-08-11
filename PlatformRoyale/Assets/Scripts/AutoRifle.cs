using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : MonoBehaviour
{
    //private Animation _animation;
    //public Light boom;
    public Transform bulletSpawn;
    public GameObject bullet;

    public float bulletSpeed = 20f;
    public float damage = 10f;
    //public float range = 100f;
    public float fireRate = 1f;
    public float impactForce = 30f;

    private float nextTimeToFire = 0f;
    private Vector2 tempBulletVelocity;

    private void Start()
    {
        //_animation = GetComponent<Animation>();
    }

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // StartCoroutine(Boom());
        //_animation.Play();

        GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        bulletInstance.name = "Bullet";

        var tempBullet = bulletInstance.GetComponent<AutoRifleBullet>();

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
        tempBulletVelocity = direction.normalized * bulletSpeed;

        tempBullet.SetDamage(damage);
        tempBullet.bulletVelocity = VelocityRNG(tempBulletVelocity);
    }

    Vector2 VelocityRNG(Vector2 velocity)
    {
        return velocity + new Vector2(Random.Range(0f, 2f), Random.Range(-2f, 2f));
    }


    //IEnumerator Boom()
    //{

    //    boom.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(0.06f);
    //    boom.gameObject.SetActive(false);
    //}
}
