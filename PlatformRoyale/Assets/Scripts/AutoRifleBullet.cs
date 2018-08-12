using UnityEngine;

public class AutoRifleBullet : MonoBehaviour, IDamageDealer
{
    [SerializeField]
    private GameObject _impactEffect;
    private float _damage;
    private float _impactForce;
    private Vector2 _bulletVelocity;
    private Vector2 _gravity;
    private float _yGravity = -10f;
    private float _liveTime = 2f;
    private int _predictionStepsPerFrame = 1;

    public float ImpactForce
    {
        get
        {
            return _impactForce;
        }

        set
        {
            _impactForce = value;
        }
    }
    public Vector2 BulletVelocity
    {
        get
        {
            return _bulletVelocity;
        }

        set
        {
            _bulletVelocity = value;
        }
    }
    public float GetDamage()
    {
        return _damage;
    }
    public void SetDamage(float value)
    {
        _damage = value;
    }


    private void Start()
    {
        _gravity = new Vector2(0, _yGravity);
        Destroy(gameObject, _liveTime);
    }

    void Update()
    {
        Vector2 point1 = transform.position;
        float stepSize = 1f / _predictionStepsPerFrame;
        for (float step = 0; step < 1; step += stepSize)
        {
            BulletVelocity += _gravity * stepSize * Time.deltaTime;
            Vector2 point2 = point1 + BulletVelocity * stepSize * Time.deltaTime;

            RaycastHit2D hit = Physics2D.Raycast(point1, point2 - point1, (point2 - point1).magnitude);
            if (hit)
            {
                Debug.Log("Hit " + hit.collider.name);
                ResolveCollision(hit);
            }

            point1 = point2;
        }

        transform.position = point1;
    }

    void ResolveCollision(RaycastHit2D hit)
    {
        var effect = Instantiate(_impactEffect, new Vector3(hit.point.x, hit.point.y, -1), Quaternion.LookRotation(hit.normal));
        Destroy(effect, 1f);

        var damageable = hit.collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.ApplyDamage(this);
        }

        var rb = hit.collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForceAtPosition(BulletVelocity * ImpactForce, hit.point);
        }

        if (gameObject)
        {
            Destroy(gameObject);
        }

    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector2 point1 = transform.position;
    //    Vector2 predictedBulletVelocity = bulletVelocity;
    //    float stepSize = 0.01f;
    //    for (float step = 0; step < 1; step += stepSize)
    //    {
    //        predictedBulletVelocity += gravity * stepSize;
    //        Vector2 point2 = point1 + predictedBulletVelocity * stepSize;
    //        Gizmos.DrawLine(point1, point2);
    //        point1 = point2;
    //    }
    //}
}