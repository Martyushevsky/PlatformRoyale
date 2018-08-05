using UnityEngine;

namespace PlatformRoyale
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        protected float _maxSpeed = 1f;
        [SerializeField]
        protected float _force = 1f;
        [SerializeField]
        protected float _jumpVelocity = 1f;

        private Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Move(float xAxis)
        {
            if (xAxis == 0)
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y);
            }

            if (xAxis > 0 && _rb.velocity.x >= _maxSpeed)
            {
                _rb.velocity = new Vector2(_maxSpeed, _rb.velocity.y);
            }
            else if (xAxis < 0 && _rb.velocity.x <= -_maxSpeed)
            {
                _rb.velocity = new Vector2(-_maxSpeed, _rb.velocity.y);
            }
            else
            {
                _rb.AddForce(Vector2.right * _force * xAxis * Time.deltaTime, ForceMode2D.Impulse);
            }
        }

        public void Jump()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpVelocity);
        }

        public void WallJump()
        {
            _rb.velocity = new Vector2(-_rb.velocity.x / 2, _jumpVelocity);
        }
    }
}