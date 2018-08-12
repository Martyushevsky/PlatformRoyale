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
        [SerializeField]
        private GameObject[] _weapons;
        private float _checkRadius = 0.8f;
        private float _xAxis;
        private float _yAxis;
        private bool _facingRight = true;
        private bool _grounded = false;
        private int _doubleJump = 0;
        private bool _onWall = false;
        private bool _wallJumping = false;
        private bool _crouching = false;
        private GameObject _body;


        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _body = GetComponentInChildren<Collider2D>().gameObject;
        }

        void Update()
        {
            _xAxis = Input.GetAxisRaw("Horizontal");
            _yAxis = Input.GetAxisRaw("Vertical");

            HorizontalMovement();

            Jumping();

            Crouching();

            WeaponChange();
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

            if (Input.GetButtonDown("Jump") && !_grounded && _onWall/* && _xAxis != 0*/)
            {
                Flip();
                _characterMovement.WallJump(!_facingRight);
                _doubleJump = 0;
                _wallJumping = true;
            }
        }

        void Crouching()
        {
            if (_yAxis < 0 && _crouching == false)
            {
                Vector3 scale = _body.transform.localScale;
                scale.y *= 0.5f;
                _body.transform.localScale = scale;

                Vector3 position = _body.transform.position;
                position.y -= 0.25f;
                _body.transform.position = position;

                _crouching = true;
            }

            bool hit = Physics2D.OverlapCircle(_groundCheck.position + new Vector3(0, 1, 0), _checkRadius - 0.3f, 1 << LayerMask.NameToLayer("Wall"));
            if (_yAxis > 0 && _crouching == true && !hit)
            {
                Vector3 scale = _body.transform.localScale;
                scale.y *= 2;
                _body.transform.localScale = scale;

                Vector3 position = _body.transform.position;
                position.y += 0.25f;
                _body.transform.position = position;

                _crouching = false;
            }
        }

        void Flip()
        {
            _facingRight = !_facingRight;
            Vector3 theScale = _body.transform.localScale;
            theScale.x *= -1;
            _body.transform.localScale = theScale;
        }

        void WeaponChange()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !_weapons[0].activeSelf)
            {
                for (int i = 0; i < _weapons.Length; i++)
                {
                    _weapons[i].SetActive(false);
                }
                _weapons[0].SetActive(true);

            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && !_weapons[1].activeSelf)
            {
                for (int i = 0; i < _weapons.Length; i++)
                {
                    _weapons[i].SetActive(false);
                }
                _weapons[1].SetActive(true);
            }
        }
    }
}