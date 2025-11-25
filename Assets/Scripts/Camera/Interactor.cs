using UnityEngine;

namespace CameraScripts
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayer;
        private IInteractType selectedInteractionType;

        private Transform _grabbedTransform;
        private Rigidbody _grabbedRigidbody;

        private KeyCode interactionGrabKey = KeyCode.Mouse0;
        private KeyCode interactionExplodeKey = KeyCode.Mouse1;

        private void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            if (Input.GetKey(interactionGrabKey))
            {
                RaycastHit rayHit = GetRaycastHit(interactableLayer);

                if (rayHit.transform == null && _grabbedTransform == null)
                    return;

                if (_grabbedTransform == null)
                {
                    _grabbedTransform = rayHit.transform;
                    selectedInteractionType = new InteractionGrab(_grabbedTransform);

                    if (rayHit.rigidbody != null)
                    {
                        _grabbedRigidbody = rayHit.rigidbody;
                        _grabbedRigidbody.isKinematic = true; 
                    }
                }

                selectedInteractionType.Execute();
            }

            if (Input.GetKeyUp(interactionGrabKey) && _grabbedTransform != null)
            {
                selectedInteractionType = null;
                _grabbedTransform = null;

                if (_grabbedRigidbody != null)
                    _grabbedRigidbody.isKinematic = false;
            }

            if (Input.GetKeyDown(interactionExplodeKey))
            {
                selectedInteractionType = new InteractionExplode(GetRaycastHit());
                selectedInteractionType.Execute();
            }
        }

        private RaycastHit GetRaycastHit()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit hit);

            return hit;
        }

        private RaycastHit GetRaycastHit(LayerMask includeLayer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, includeLayer);

            return hit;
        }
    }
}