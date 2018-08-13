using UnityEngine;
using PlatformRoyale.Interfaces;

namespace PlatformRoyale
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField]
        protected float _maxHealth = 100;
        [SerializeField]
        protected float _currentHealth;
        protected bool _isDead;


        void Start()
        {
            _currentHealth = _maxHealth;
            _isDead = false;
        }

        /// <summary>
        /// Метод для получения урона
        /// </summary>
        /// <param name="damageDealer">Ссылка на источник урона</param>
        public void ApplyDamage(IDamageDealer damageDealer)
        {
            if (!_isDead)
            {
                StartInjuringAnimation();
                _currentHealth = Mathf.Clamp(_currentHealth - CalculateDamage(damageDealer), 0, _maxHealth);
            }
            if (_currentHealth <= 0)
            {
                _isDead = true;
                Die();
            }
        }

        /// <summary>
        /// Запуск анимации смерти, и другие необходимые действия при смерти игрока.
        /// Например, отключение каких-нибудь компонентов, которые не должны отрабатывать после смерти персонажа.
        /// </summary>
        protected void Die()
        {
            StartDeathAnimation();
            Debug.Log("Player died :(");
            ///Тут можно будет выполнить ещё какие-нибудь действия при смерти
        }

        /// <summary>
        /// Рассчитать получаемый урон с учётом разных факторов (броня, усиление повреждений и т.д.)
        /// </summary>
        /// <param name="damageDealer">Ссылка на источник урона</param>
        /// <returns>Количество получаемого урона после обработки</returns>
        protected float CalculateDamage(IDamageDealer damageDealer)
        {
            //do something with damage
            return damageDealer.GetDamage();
        }

        /// <summary>
        /// Проиграть анимацию смерти
        /// </summary>
        protected void StartDeathAnimation()
        {

        }

        /// <summary>
        /// Проиграть анимацию получения урона
        /// </summary>
        protected void StartInjuringAnimation()
        {

        }
    }
}