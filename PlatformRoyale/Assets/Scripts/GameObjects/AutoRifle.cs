using UnityEngine;

namespace PlatformRoyale
{
    public class AutoRifle : RangeWeapon
    {
        public AutoRifle(RangeWeaponParams rangeWeaponParams) : base(rangeWeaponParams)
        {
        }

        private void Start()
        {
            bulletScatter.x = 0;
            bulletScatter.y = 2;

            bulletSpeed = 30f;
            damage = 20f;
            impactForce = 100f;
            fireRate = 10f;
        }
    }
}