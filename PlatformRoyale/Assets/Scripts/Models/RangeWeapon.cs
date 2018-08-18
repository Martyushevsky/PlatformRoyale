using UnityEngine;

namespace PlatformRoyale
{
    public struct RangeWeaponParams
    {
        public WeaponParams weaponParams;
    }

    public class RangeWeapon : Weapon
    {
        //private Animation _animation;
        //public Light boom;
        public Transform bulletSpawn;
        public GameObject bullet;
        public float bulletSpeed = 30f;
        public float damage = 20f;
        public float impactForce = 100f;
        public float fireRate = 10f;
        public Vector2 bulletScatter;

        protected float _nextTimeToFire = 0f;
        protected float _weaponChangeDelay = 0f;
        protected Vector2 _tempBulletVelocity;

        public RangeWeapon(RangeWeaponParams rangeWeaponParams) : base(rangeWeaponParams.weaponParams)
        {

        }

        private void Start()
        {
            //_animation = GetComponent<Animation>();
            bulletScatter.x = 0;
            bulletScatter.y = 0;
        }

        protected void OnEnable()
        {
            _weaponChangeDelay = Time.time + 0.5f;
        }

        protected void Update()
        {
            if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire && Time.time >= _weaponChangeDelay)
            {
                _nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        public virtual void Shoot()
        {
            // StartCoroutine(Boom());
            //_animation.Play();

            GameObject bulletInstance = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
            bulletInstance.name = "Bullet";

            var tempBullet = bulletInstance.GetComponent<BouncingBulletBase>();

            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
            _tempBulletVelocity = direction.normalized * bulletSpeed;

            tempBullet.BulletVelocity = VelocityRNG(_tempBulletVelocity);
            tempBullet.BulletStartSpeed = bulletSpeed;
            tempBullet.ImpactForce = impactForce;
            tempBullet.BulletDamage = damage;
        }

        protected Vector2 VelocityRNG(Vector2 velocity)
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
}