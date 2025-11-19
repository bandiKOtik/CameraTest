using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

namespace CameraScripts
{
    public class CameraSwitch : MonoBehaviour
    {
        [SerializeField] private List<CinemachineVirtualCamera> _virtualCamerasList;

        private Dictionary<KeyCode, int> CameraKeyCodes = new Dictionary<KeyCode, int>()
        {
            { KeyCode.Alpha1, 0 },
            { KeyCode.Alpha2, 1 },
            { KeyCode.Alpha3, 2},
        };

        private void Awake()
        {
            if (_virtualCamerasList.Count != CameraKeyCodes.Count)
                Debug.LogWarning($"{gameObject.name}: Keycode Dictionary and Virtual Camera List count are not the same! This may cause some errors.");
        }

        private void Update()
        {
            foreach (var keyCode in CameraKeyCodes)
            {
                if (Input.GetKeyDown(keyCode.Key))
                    SwithCamera(_virtualCamerasList[keyCode.Value]);
            }
        }

        private void SwithCamera(CinemachineVirtualCamera priorityCamera)
        {
            foreach (var camera in _virtualCamerasList)
                camera.Priority = 1;

            priorityCamera.Priority = 2;
        }
    }
}