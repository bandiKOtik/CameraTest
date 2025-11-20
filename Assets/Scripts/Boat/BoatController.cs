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
                _boatTransform.rotation = LerpedRotation(-Vector3.up);

            if (Input.GetKey(_rightMoveKey))
                _boatTransform.rotation = LerpedRotation(Vector3.up);
        }

        private Quaternion LerpedRotation(Vector3 offset)
        {
            Quaternion rotationWithOffset = _boatTransform.rotation * Quaternion.Euler(offset);

            float lerpedSpeed = Time.deltaTime * _rotationSpeed;

            return Quaternion.Lerp(_boatTransform.rotation, rotationWithOffset, lerpedSpeed);
        }
    }
}