using UnityEngine;

namespace CameraScripts
{
    public class InteractionGrab : IInteractType
    {
        private Transform _selectedObjectTransform;
        private float _distanceFromCamera = 10f;
        private float speed = 5f;

        public InteractionGrab(Transform objectTransform)
        {
            _selectedObjectTransform = objectTransform;
        }

        public void Execute()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 objectPosition = _selectedObjectTransform.position;
            Vector3 newObjectPosition = ray.origin + ray.direction * _distanceFromCamera;

            float smoothedSpeed = Time.deltaTime * speed;

            objectPosition = Vector3.MoveTowards(objectPosition, newObjectPosition, smoothedSpeed);
        }
    }
}