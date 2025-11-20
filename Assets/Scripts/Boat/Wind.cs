using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoatScripts
{
    public class Wind : MonoBehaviour
    {
        [SerializeField] private bool _useTimer;
        [SerializeField] private float _rotationSpeed = 500f;
        [SerializeField] private float _timeBeforeNewRotation = 15f;

        private Transform _windTransform;
        private Quaternion _newDirection;
        private bool _isRotating;
        private float _timer;

        private void Awake()
        {
            _windTransform = GetComponent<Transform>();

            _newDirection = Quaternion.Euler(GetNewDirection());

            _windTransform.rotation = _newDirection;
        }

        private void Update()
        {
            if (_useTimer)
            {
                if (_timer >= _timeBeforeNewRotation)
                {
                    if (_isRotating == false)
                    {
                        _newDirection = Quaternion.Euler(GetNewDirection());
                        _isRotating = true;
                    }

                    while (_windTransform.rotation != _newDirection)
                    {
                        SmoothRotation(_newDirection);
                        return;
                    }

                    _isRotating = false;
                    _timer = 0;
                }

                _timer += Time.deltaTime;
            }
        }

        private void SmoothRotation(Quaternion angle)
        {
            float smoothedSpeed = Time.deltaTime * _rotationSpeed;

            _windTransform.rotation = Quaternion.RotateTowards(_windTransform.rotation, angle, Time.deltaTime * smoothedSpeed);
        }

        private Vector3 GetNewDirection()
        {
            float minY = 0;
            float maxY = 360;

            float newWindY = Random.Range(minY, maxY);

            Debug.Log("New rotation: " + newWindY);
            return new Vector3(0, newWindY, 0);
        }
    }
}