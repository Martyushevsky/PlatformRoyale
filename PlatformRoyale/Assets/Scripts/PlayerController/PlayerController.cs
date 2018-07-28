using UnityEngine;

namespace PlatformRoyale.SceneObjects
{
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerController : BaseSceneObject
    {
        private CharacterMovement _characterMovement;

        [SerializeField]
        private Transform _groundCheck;                  // Ссылка на трансформ объекта groundCheck.
        private bool _grounded = false;                  // Флаг нахождения на земле.


        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
        }

        void Update()
        {
            HorizontalMovement();

            Jumping();
        }

        private void FixedUpdate()
        {
            // Пускает луч до объекта groundCheck. Если луч сталкивается с чем либо в слое Ground, возвращает true.
            _grounded = Physics2D.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        }

        void HorizontalMovement()
        {
            _characterMovement.Move(Input.GetAxis("Horizontal"));
        }

        void Jumping()
        {
            // Если нажата клавиша прыжжок, тело стоит на земле, тело может прыгать.
            if (Input.GetButtonDown("Jump") && _grounded)
            {
                _characterMovement.Jump();
            }
        }
    }
}