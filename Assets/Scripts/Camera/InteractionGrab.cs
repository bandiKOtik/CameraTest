using UnityEngine;

namespace CameraScripts
{
    public class InteractionGrab : IInteractType
    {
        private Transform _selectedObjectTransform;
        private float _distanceFromCamera = 10f;
        private float speed = 10f;

        public InteractionGrab(Transform objectTransform)
        {
            _selectedObjectTransform = objectTransform;
        }

        public void Execute()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 newObjectPosition = ray.origin + ray.direction * _distanceFromCamera;

            float smoothedSpeed = Time.deltaTime * speed;

            _selectedObjectTransform.position = Vector3.Lerp(_selectedObjectTransform.position, newObjectPosition, smoothedSpeed);
        }
    }
}