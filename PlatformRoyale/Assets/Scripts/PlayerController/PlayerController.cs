/* 
 Пока выполняет только роль PlayerInputController 
 т.е. получает ввод и двигает за счет этого персонажа
 */
using UnityEngine;

namespace PlatformRoyale.SceneObjects
{
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerController : BaseSceneObject
    {
        private CharacterMovement _characterMovement;
        private Vector3 _movement = Vector3.zero;

        [SerializeField]
        private Transform _groundCheck;                  // Ссылка на трансформ объекта groundCheck.

        private bool _grounded = false;                  // Флаг нахождения на земле.



        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
        }

        void Update()
        {
            _movement.x = Input.GetAxis("Horizontal");
            //_movement.y = Input.GetAxis("Vertical");            

            _characterMovement.Move(_movement);

            JumpHandler();
        }

        private void FixedUpdate()
        {
            // Пускает луч до объекта groundCheck. Если луч сталкивается с чем либо в слое Ground, возвращает true.
            _grounded = Physics2D.Linecast(transform.position, _groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        }

        void JumpHandler()
        {
            // Если нажата клавиша прыжжок, тело стоит на земле, тело может прыгать.
            if (Input.GetButtonDown("Jump") && _grounded)
            {
                _characterMovement.Jump();
            }
        }
    }
}