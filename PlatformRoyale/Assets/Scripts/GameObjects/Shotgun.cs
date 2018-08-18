using UnityEngine;

namespace PlatformRoyale
{
    public class Shotgun : RangeWeapon
    {
        public Shotgun(RangeWeaponParams rangeWeaponParams) : base(rangeWeaponParams)
        {
        }

        private void Start()
        {
            bulletScatter.x = 1;
            bulletScatter.y = 5;

            bulletSpeed = 15f;
            damage = 15f;
            impactForce = 200f;
            fireRate = 1.2f;
        }

        override public void Shoot()
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                bulletInstance.name = "Bullet";

                var tempBullet = bulletInstance.GetComponent<ShotgunBullet>();

                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
                _tempBulletVelocity = direction.normalized * bulletSpeed;

                tempBullet.BulletVelocity = VelocityRNG(_tempBulletVelocity);
                tempBullet.BulletStartSpeed = bulletSpeed;
                tempBullet.ImpactForce = impactForce;
                tempBullet.BulletDamage = damage;
            }
        }
    }
}