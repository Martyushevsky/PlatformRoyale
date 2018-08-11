using UnityEngine;

namespace PlatformRoyale
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        protected float _maxSpeed = 10f;
        [SerializeField]
        protected float _force = 1000f;
        [SerializeField]
        protected float _jumpVelocity = 20f;
        [SerializeField]
        protected float fallMultiplier = 2.5f;
        [SerializeField]
        protected float lowJumpMultiplier = 2.5f;

        private Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            }
            else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
            }
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

        public void WallJump(bool facingRight)
        {
            if (facingRight)
            {
                _rb.velocity = new Vector2(-_maxSpeed, _jumpVelocity);
            }
            else
            {
                _rb.velocity = new Vector2(_maxSpeed, _jumpVelocity);
            }
        }
    }
}