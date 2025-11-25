using UnityEngine;

namespace BoatScripts
{
    public class BoatController : MonoBehaviour
    {
        [SerializeField] private Transform _boatTransform;
        [SerializeField] private Transform _sailTransform;
        [SerializeField] private Wind _wind;
        private Transform _windTransform;

        [SerializeField] private float _maxForwardSpeed = 5f;
        [SerializeField] private float _maxSailAngle = 90f;
        [SerializeField] private float _rotationSpeed = 20f;

        private KeyCode _leftMoveKey = KeyCode.A;
        private KeyCode _rightMoveKey = KeyCode.D;
        private KeyCode _sailLeftMoveKey = KeyCode.Q;
        private KeyCode _sailRightMoveKey = KeyCode.E;

        private Rotator _rotator;

        private void Awake()
        {
            _rotator = new Rotator();

            _windTransform = _wind.transform;
        }

        private void Update()
        {
            MovementControl();

            MoveForward();
        }

        private void MovementControl()
        {
            if (Input.GetKey(_leftMoveKey))
                _rotator.Rotate(_boatTransform, -Vector3.up, _rotationSpeed);

            if (Input.GetKey(_rightMoveKey))
                _rotator.Rotate(_boatTransform, Vector3.up, _rotationSpeed);

            if (Input.GetKey(_sailLeftMoveKey))
                _rotator.Rotate(_sailTransform, -Vector3.up, _rotationSpeed, _boatTransform, _maxSailAngle);

            if (Input.GetKey(_sailRightMoveKey))
                _rotator.Rotate(_sailTransform, Vector3.up, _rotationSpeed, _boatTransform, _maxSailAngle);
        }

        private void MoveForward()
            => transform.Translate(transform.forward * ForwardSpeed() * Time.deltaTime, Space.World);

        private float ForwardSpeed()
        {
            float speed = 0;
            Vector3 windDirection = _windTransform.forward;

            float dotProduct = Vector3.Dot(windDirection, _sailTransform.forward);
            float angle = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
            float angleFactor = 1 - (angle / _maxSailAngle);
            float speedForce = dotProduct * angleFactor;

            if (angle < _maxSailAngle)
            {
                speed = speedForce * _maxForwardSpeed;
                Debug.Log("Boat speed: " + speed.ToString("0.00") + " / " + _maxForwardSpeed);
            }

            return speed;
        }
    }
}