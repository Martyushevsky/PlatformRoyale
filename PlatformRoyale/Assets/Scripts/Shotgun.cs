using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    //private Animation _animation;
    //public Light boom;
    //public float range = 100f;

    public Transform bulletSpawn;
    public GameObject bullet;
    public float bulletSpeed = 20f;
    public float damage = 10f;
    public float impactForce = 200f;
    public float fireRate = 1.2f;
    public Vector2 bulletScatter;

    private float _nextTimeToFire = 0f;
    private float _weaponChangeDelay = 0f;
    private Vector2 _tempBulletVelocity;

    private void Start()
    {
        //_animation = GetComponent<Animation>();
        bulletScatter.x = 1;
        bulletScatter.y = 5;
    }

    private void OnEnable()
    {
        _weaponChangeDelay = Time.time + 0.5f;
    }

    public void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire && Time.time >= _weaponChangeDelay)
        {
            _nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // StartCoroutine(Boom());
        //_animation.Play();

        for (int i = 0; i < 10; i++)
        {
            GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            bulletInstance.name = "Bullet";

            var tempBullet = bulletInstance.GetComponent<ShotgunBullet>();

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
            _tempBulletVelocity = direction.normalized * bulletSpeed;

            tempBullet.SetDamage(damage);
            tempBullet.ImpactForce = impactForce;
            tempBullet.BulletVelocity = VelocityRNG(_tempBulletVelocity);
        }
    }

    Vector2 VelocityRNG(Vector2 velocity)
    {
        return velocity + new Vector2(Random.Range(-bulletScatter.x, bulletScatter.x), Random.Range(-bulletScatter.y, bulletScatter.y));
    }


    //IEnumerator Boom()
    //{

    //    boom.gameObject.SetActive(true);
    //    yield return new WaitForSeconds(0.06f);
    //    boom.gameObject.SetActive(false);
    //}
}
