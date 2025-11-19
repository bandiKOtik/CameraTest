using UnityEngine;

namespace CameraScripts
{
    public class InteractionGrab : IInteractType
    {
        private Transform _selectedObject;
        private Vector3 _moveToPosition;
        private float _distanceFromCamera = 10f;

        public InteractionGrab(Transform objectTransform)
        {
            _selectedObject = objectTransform;
        }

        public void Execute()
        {
            _moveToPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            _selectedObject.Translate(_moveToPosition * Time.deltaTime, Space.World);
        }
    }
}
