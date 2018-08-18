using UnityEngine;
using PlatformRoyale.Interfaces;

namespace PlatformRoyale
{
    public class ShotgunBullet : SimpleBulletBase
    {
        private void Awake()
        {
            _bulletLiveTime = 0.8f;
            _bulletDrag = 0.3f;
            _yGravity = -10f;
        }
    }
}