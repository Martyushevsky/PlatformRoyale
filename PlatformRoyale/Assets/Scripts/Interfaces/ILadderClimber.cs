using UnityEngine;

namespace PlatformRoyale.Interfaces
{
    public class ILadderClimber : MonoBehaviour
    {
        private bool _isTouchingLadder = false;
        private Vector3 _ladderPosition;

        public bool IsTouchingLadder
        {
            get
            {
                return _isTouchingLadder;
            }

            set
            {
                _isTouchingLadder = value;
            }
        }

        public Vector3 LadderPosition
        {
            get
            {
                return _ladderPosition;
            }

            set
            {
                _ladderPosition = value;
            }
        }
    }
}
