using Unity.VisualScripting;
using UnityEngine;

namespace CameraScripts
{
    public class InteractionGrab : IInteractType
    {
        private Transform _selectedTransform;
        private Vector3 _newObjectPosition;
        private float _distanceFromCamera = 10f;
        private float speed = 5f;

        public InteractionGrab(Transform objectTransform)
        {
            _selectedTransform = objectTransform;
        }

        public void Execute()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            _newObjectPosition = ray.origin + ray.direction * _distanceFromCamera;

            float lerpedSpeed = Time.deltaTime * speed;

            _selectedTransform.position = Vector3.Lerp(_selectedTransform.position, _newObjectPosition, lerpedSpeed);
        }
    }
}