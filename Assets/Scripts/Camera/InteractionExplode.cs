using UnityEngine;

namespace CameraScripts
{
    public class InteractionExplode : IInteractType
    {
        private float _explosionRadius = 10f;
        private float _explosionStrength = 25f;
        private RaycastHit _hitPoint;
        public InteractionExplode(RaycastHit hit)
        {
            _hitPoint = hit;
        }

        public void Execute()
        {
            Collider[] objectsInRange = Physics.OverlapSphere(_hitPoint.point, _explosionRadius);

            foreach (Collider collider in objectsInRange)
            {
                if (collider.TryGetComponent<Rigidbody>(out Rigidbody objectRigidbody))
                {
                    Vector3 normalizedDirection = (collider.transform.position - _hitPoint.point).normalized;
                    float distance = Vector3.Distance(collider.transform.position, _hitPoint.point);
                    float lerpedStrength = _explosionStrength * (1f - distance / _explosionRadius);

                    objectRigidbody.AddForce(normalizedDirection * lerpedStrength, ForceMode.Impulse);
                }
            }
        }
    }
}