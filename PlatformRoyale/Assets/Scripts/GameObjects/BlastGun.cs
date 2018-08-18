using UnityEngine;

namespace PlatformRoyale
{
    public class BlastGun : RangeWeapon
    {
        public BlastGun(RangeWeaponParams rangeWeaponParams) : base(rangeWeaponParams)
        {
        }

        private void Start()
        {
            bulletScatter.x = 50;
            bulletScatter.y = 50;

            bulletSpeed = 50f;
            damage = 1f;
            impactForce = 50f;
            fireRate = 10f;
        }

        override public void Shoot()
        {
            for (int i = 0; i < 26; i++)
            {
                GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
                bulletInstance.name = "Bullet";

                var tempBullet = bulletInstance.GetComponent<BlastGunBullet>();

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