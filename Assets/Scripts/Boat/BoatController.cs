using UnityEngine;

namespace BoatScripts
{
    public class BoatController : MonoBehaviour
    {
        private Transform _boatTransform;
        private Transform _windTransform;
        [SerializeField] private Wind _wind;

        [SerializeField] private float _sailAngle = 90f;

        [SerializeField] private float _maxForwardSpeed = 10f;
        private float _rotationSpeed = 10f;

        private KeyCode _leftMoveKey = KeyCode.A;
        private KeyCode _rightMoveKey = KeyCode.D;

        private void Awake()
        {
            _boatTransform = GetComponent<Transform>();

            if (_wind != null)
                _windTransform = _wind.transform;
        }

        private void Update()
        {
            MovementControl();

            ForwardMove();
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

        private void ForwardMove()
        {
            transform.Translate(transform.forward * ForwardSpeed() * Time.deltaTime, Space.World);
        }

        private float ForwardSpeed()
        {
            float speed = 0;

            Vector3 windDirection = _windTransform.forward;

            float dotProduct = Vector3.Dot(windDirection, transform.forward);

            float cos = dotProduct / (windDirection.magnitude * transform.forward.magnitude);

            float angle = Mathf.Acos(cos) * Mathf.Rad2Deg;

            if (angle < _sailAngle / 2)
                speed = Mathf.Clamp(_sailAngle / 2 - angle, 0, _maxForwardSpeed);

            return speed;
        }
    }
}