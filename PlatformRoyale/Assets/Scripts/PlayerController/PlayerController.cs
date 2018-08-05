using UnityEngine;

namespace PlatformRoyale.SceneObjects
{
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerController : BaseSceneObject
    {
        private CharacterMovement _characterMovement;

        [SerializeField]
        private Transform _groundCheck;
        [SerializeField]
        private Transform _wallCheck;
        private float _checkRadius = 0.8f;
        private float _xAxis;
        private float _yAxis;
        private bool _facingRight = true;
        private bool _grounded = false;
        private int _doubleJump = 0;
        private bool _onWall = false;
        private bool _wallJumping = false;
        private bool _crouching = false;


        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
        }

        void Update()
        {
            _xAxis = Input.GetAxisRaw("Horizontal");
            _yAxis = Input.GetAxisRaw("Vertical");

            HorizontalMovement();

            Jumping();

            Crouching();
        }

        private void FixedUpdate()
        {
            _grounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, 1 << LayerMask.NameToLayer("Ground"));

            if (_grounded)
            {
                _wallJumping = false;
                _doubleJump = 0;
            }

            _onWall = Physics2D.Linecast(transform.position, _wallCheck.position, 1 << LayerMask.NameToLayer("Wall"));
        }

        void HorizontalMovement()
        {
            if (!_wallJumping)
            {
                if (_xAxis > 0 && !_facingRight)
                {
                    Flip();
                }
                else if (_xAxis < 0 && _facingRight)
                {
                    Flip();
                }
                _characterMovement.Move(_xAxis);
            }
        }

        void Jumping()
        {
            if (Input.GetButtonDown("Jump") && _grounded)
            {
                _characterMovement.Jump();
            }

            if (Input.GetButtonDown("Jump") && !_grounded && _doubleJump < 1)
            {
                _doubleJump++;
                _characterMovement.Jump();
            }

            if (Input.GetButtonDown("Jump") && !_grounded && _onWall && _xAxis != 0)
            {
                Flip();
                _characterMovement.WallJump();
                _doubleJump = 0;
                _wallJumping = true;
            }
        }

        void Crouching()
        {
            if (_yAxis < 0 && _crouching == false)
            {
                Vector3 scale = transform.localScale;
                scale.y *= 0.5f;
                transform.localScale = scale;

                Vector3 position = transform.position;
                position.y -= 0.25f;
                transform.position = position;

                _crouching = true;
            }

            if (_yAxis > 0 && _crouching == true)
            {
                Vector3 scale = transform.localScale;
                scale.y *= 2;
                transform.localScale = scale;

                Vector3 position = transform.position;
                position.y += 0.25f;
                transform.position = position;

                _crouching = false;
            }
        }

        void Flip()
        {
            _facingRight = !_facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}