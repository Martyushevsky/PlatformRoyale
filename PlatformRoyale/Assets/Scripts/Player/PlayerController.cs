using UnityEngine;
using PlatformRoyale.Interfaces;

namespace PlatformRoyale
{
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterMovement _characterMovement;
        private GameObject _body;
        private ILadderClimber _climber;
        private Animator _anim;

        [SerializeField]
        private Transform _groundCheck;
        [SerializeField]
        private Transform _wallCheck;
        [SerializeField]
        private GameObject[] _weapons;

        private int _defaultLayer;
        private int _onLadderLayer;

        private float _checkRadius = 0.8f;
        private float _xAxis;
        private float _yAxis;

        private int _doubleJump = 0;
        private bool _isGrounded = false;
        private bool _isOnWall = false;
        private bool _isWallJumping = false;
        private bool _isCrouching = false;
        private bool _isClimbing = false;
        private bool _isFacingRight = true;


        void Awake()
        {


            _defaultLayer = LayerMask.NameToLayer("Default");
            _onLadderLayer = LayerMask.NameToLayer("OnLadder");

            _characterMovement = GetComponent<CharacterMovement>();
            _body = GetComponentInChildren<Collider2D>().gameObject;
            _anim = _body.GetComponent<Animator>();
            _climber = GetComponentInChildren<ILadderClimber>();
        }

        void Update()
        {
            _xAxis = Input.GetAxisRaw("Horizontal");
            _yAxis = Input.GetAxisRaw("Vertical");

            LadderChecking();

            if (_isClimbing)
            {
                _characterMovement.LadderClimbing(_yAxis);
                _anim.SetInteger("MoveX", 0);
            }
            else
            {
                HorizontalMovement();

                Jumping();

                Crouching();

                WeaponChange();
            }
        }

        private void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, 1 << LayerMask.NameToLayer("Ground"));

            if (_isGrounded)
            {
                _anim.SetBool("Jumping", false);

                _isWallJumping = false;
                _doubleJump = 0;
            }

            _anim.SetBool("SecondJump", false);

            _isOnWall = Physics2D.Linecast(transform.position, _wallCheck.position, 1 << LayerMask.NameToLayer("Wall"));
        }

        void HorizontalMovement()
        {
            if (!_isWallJumping)
            {
                if (_xAxis > 0 && !_isFacingRight)
                {
                    Flip();
                }
                else if (_xAxis < 0 && _isFacingRight)
                {
                    Flip();
                }

                _characterMovement.Move(_xAxis);

                _anim.SetInteger("MoveX", (int)_xAxis);
            }
        }

        void Jumping()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                if (!_isCrouching)
                {
                    _characterMovement.Jump();
                    _anim.SetBool("Jumping", true);
                }
            }

            if (Input.GetButtonDown("Jump") && !_isGrounded && !_isCrouching && _doubleJump < 1)
            {
                _doubleJump++;
                _characterMovement.Jump();
                _anim.SetBool("SecondJump", true);
            }

            if (Input.GetButtonDown("Jump") && !_isGrounded && !_isCrouching && _isOnWall)
            {
                Flip();
                _characterMovement.WallJump(!_isFacingRight);
                _doubleJump = 0;
                _isWallJumping = true;
                _anim.SetBool("SecondJump", true);
            }
        }

        void Crouching()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && _isCrouching == false)
            {
                Vector3 scale = _body.transform.localScale;
                scale.y *= 0.5f;
                _body.transform.localScale = scale;

                Vector3 position = _body.transform.position;
                position.y -= 0.25f;
                _body.transform.position = position;

                _isCrouching = true;
            }

            bool hit = Physics2D.OverlapCircle(_groundCheck.position + new Vector3(0, 1, 0), _checkRadius - 0.3f, 1 << LayerMask.NameToLayer("Wall"));
            if (Input.GetButtonDown("Jump") && _isCrouching == true && !hit)
            {
                Vector3 scale = _body.transform.localScale;
                scale.y *= 2;
                _body.transform.localScale = scale;

                Vector3 position = _body.transform.position;
                position.y += 0.25f;
                _body.transform.position = position;

                _isCrouching = false;
            }
        }

        void LadderChecking()
        {
            if (_climber.IsTouchingLadder)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _isClimbing = !_isClimbing;
                    _body.layer = _onLadderLayer;
                }
            }
            else
            {
                _isClimbing = false;
                _body.layer = _defaultLayer;
            }

            _characterMovement.PositionOnLadder(_isClimbing, _climber.LadderPosition);
        }

        void Flip()
        {
            _isFacingRight = !_isFacingRight;
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
            if (Input.GetKeyDown(KeyCode.Alpha3) && !_weapons[2].activeSelf)
            {
                for (int i = 0; i < _weapons.Length; i++)
                {
                    _weapons[i].SetActive(false);
                }
                _weapons[2].SetActive(true);
            }
        }
    }
}