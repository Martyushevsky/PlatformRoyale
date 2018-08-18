﻿using UnityEngine;
using PlatformRoyale.Interfaces;

namespace PlatformRoyale
{
    public class AutoRifleBullet : BouncingBulletBase
    {
        private void Awake()
        {
            _bulletLiveTime = 10f;
            _bulletDrag = 0.3f;
            _yGravity = -10f;
        }
    }
}