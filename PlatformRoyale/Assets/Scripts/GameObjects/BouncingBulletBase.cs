using UnityEngine;
using PlatformRoyale.Interfaces;

namespace PlatformRoyale
{
    public class BouncingBulletBase : MonoBehaviour, IDamageDealer
    {
        #region Variables
        [SerializeField]
        protected GameObject _impactEffect;
        protected Vector2 _bulletDirection;
        protected Vector2 _bulletVelocity;
        protected float _bulletStartSpeed;
        protected float _bulletDamage;
        protected float _impactForce;
        protected float _bulletLiveTime;
        protected float _bulletDrag;

        protected float _yGravity;
        protected Vector2 _gravity;

        protected int _layerMask;
        protected int _predictionStepsPerFrame = 1;
        protected bool _bulletBounced = false;
        #endregion

        #region Properties
        public Vector2 BulletVelocity { get; set; }
        public float BulletStartSpeed { get; set; }
        public float BulletDamage { get; set; }
        public float ImpactForce { get; set; }
        //public Vector2 BulletVelocity
        //{
        //    get
        //    {
        //        return _bulletVelocity;
        //    }

        //    set
        //    {
        //        _bulletVelocity = value;
        //    }
        //}
        //public float BulletStartSpeed
        //{
        //    get
        //    {
        //        return _bulletStartSpeed;
        //    }

        //    set
        //    {
        //        _bulletStartSpeed = value;
        //    }
        //}
        //public float BulletDamage
        //{
        //    get
        //    {
        //        return _bulletDamage;
        //    }

        //    set
        //    {
        //        _bulletDamage = value;
        //    }
        //}
        //public float ImpactForce
        //{
        //    get
        //    {
        //        return _impactForce;
        //    }

        //    set
        //    {
        //        _impactForce = value;
        //    }
        //}
        #endregion

        /// <summary>
        /// IDamageDealer implementation
        /// </summary>
        /// <returns></returns>
        public float GetDamage()
        {
            return BulletDamage;
        }

        private void Awake()
        {
            _bulletLiveTime = 10f;
            _bulletDrag = 0.3f;
            _yGravity = -10f;
        }

        protected void Start()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Ladder") | 1 << LayerMask.NameToLayer("LadderTop");
            _layerMask = ~_layerMask;

            _gravity = new Vector2(0, _yGravity);

            Destroy(gameObject, _bulletLiveTime);
        }

        protected void FixedUpdate()
        {
            Vector2 point1 = transform.position;

            float stepSize = 1f / _predictionStepsPerFrame;
            for (float step = 0; step < 1; step += stepSize)
            {
                _bulletDirection = BulletVelocity.normalized;

                BulletVelocity += _gravity * stepSize * Time.fixedDeltaTime;
                BulletVelocity *= 1 - _bulletDrag * stepSize * Time.fixedDeltaTime;

                Vector2 point2 = point1 + BulletVelocity * stepSize * Time.fixedDeltaTime;
                //Debug.DrawLine(point1, point2, Color.red);

                RaycastHit2D hit = Physics2D.Raycast(point1, _bulletDirection, (point2 - point1).magnitude, _layerMask);
                if (hit)
                {
                    Debug.Log("Hit " + hit.collider.name);

                    ResolveCollision(hit);

                    BulletVelocity = Vector2.Reflect(_bulletDirection, hit.normal) * BulletStartSpeed / 3;

                    point2 = hit.point - _bulletDirection / 100;
                    //Debug.DrawLine(point1, point2, Color.green);
                }

                point1 = point2;
            }

            transform.position = point1;
        }

        protected void ResolveCollision(RaycastHit2D hit)
        {
            var effect = Instantiate(_impactEffect, new Vector3(hit.point.x, hit.point.y, -1), Quaternion.LookRotation(hit.normal));
            Destroy(effect, 1f);

            var damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                _bulletBounced = true;
                damageable.ApplyDamage(this);
            }

            var rb = hit.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                _bulletBounced = true;
                rb.AddForceAtPosition(BulletVelocity * ImpactForce, hit.point);
            }

            if (gameObject && _bulletBounced)
            {
                Destroy(gameObject);
            }

            _bulletBounced = true;
        }
    }
}