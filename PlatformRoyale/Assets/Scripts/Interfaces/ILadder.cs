using UnityEngine;

namespace PlatformRoyale.Interfaces
{
    public class ILadder : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var other = collision.GetComponent<ILadderClimber>();
            if (other)
            {
                Debug.Log(collision.gameObject.name + " entered");
                other.IsTouchingLadder = true;
                other.LadderPosition = transform.position;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var other = collision.GetComponent<ILadderClimber>();
            if (other)
            {
                Debug.Log(collision.gameObject.name + " left");
                other.IsTouchingLadder = false;
            }
        }
    }
}