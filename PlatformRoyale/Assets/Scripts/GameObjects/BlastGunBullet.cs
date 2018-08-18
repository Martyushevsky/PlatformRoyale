using UnityEngine;
using PlatformRoyale.Interfaces;

namespace PlatformRoyale
{
    public class BlastGunBullet : BouncingBulletBase
    {
        private void Awake()
        {
            _bulletLiveTime = 2f;
            _bulletDrag = 0.3f;
            _yGravity = -10f;
        }
    }
}