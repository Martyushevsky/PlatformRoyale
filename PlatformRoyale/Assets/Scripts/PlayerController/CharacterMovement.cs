using UnityEngine;

namespace PlatformRoyale
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField]
        protected float _moveSpeed = 1f;
        [SerializeField]
        protected float _jumpVelocity = 1f;        // Скорость прыжка.

        private Rigidbody2D _rb;

        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Перемещение персонажа в указанном направлении
        /// </summary>
        /// <param name="movement">вектор направления</param>
        public void Move(Vector3 movement)
        {
            transform.position += movement * Time.deltaTime * _moveSpeed;
        }

        public void Jump()
        {
            _rb.velocity = Vector2.up * _jumpVelocity;
        }
    }
}
