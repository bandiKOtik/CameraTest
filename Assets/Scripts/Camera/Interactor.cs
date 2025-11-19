using UnityEngine;

namespace CameraScripts
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private LayerMask interactableLayer;
        private IInteractType currentInteractType;
        private KeyCode grabInteractKey = KeyCode.Mouse0;
        private KeyCode explodeInteractKey = KeyCode.Mouse1;

        private void Update()
        {
            if (Input.GetKey(grabInteractKey))
            {
                RaycastHit rayHit = GetRaycastHit(interactableLayer);

                if (rayHit.transform == null)
                    return;

                currentInteractType = new InteractionGrab(rayHit.transform);
                currentInteractType.Execute();
            }

            if (Input.GetKeyDown(explodeInteractKey))
            {
                currentInteractType = new InteractionExplode(GetRaycastHit());
                currentInteractType.Execute();
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