using UnityEngine;

namespace BoatScripts
{
    public class BoatController : MonoBehaviour
    {
        private Transform _boatTransform;
        private float _rotationSpeed = 10f;

        private KeyCode _leftMoveKey = KeyCode.A;
        private KeyCode _rightMoveKey = KeyCode.D;

        private void Awake()
        {
            _boatTransform = GetComponent<Transform>();
        }

        private void Update()
        {
            MovementControl();
        }

        private void MovementControl()
        {
            if (Input.GetKey(_leftMoveKey))
            {
                RotateBoat(-Vector3.up);
            }

            if (Input.GetKey(_rightMoveKey))
            {
                RotateBoat(Vector3.up);
            }
        }

        private void RotateBoat(Vector3 offset)
        {
            Quaternion rotationWithOffset = _boatTransform.rotation * Quaternion.Euler(offset);

            float smoothedSpeed = Time.deltaTime * _rotationSpeed;

            _boatTransform.rotation = Quaternion.RotateTowards(_boatTransform.rotation, rotationWithOffset, smoothedSpeed);
        }
    }
}