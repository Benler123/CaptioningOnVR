using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
    /// <summary>
    /// Manages input fallback for <see cref="XRGazeInteractor"/> when eye tracking is not available.
    /// </summary>
    public class GazeInputManager : MonoBehaviour
    {
        // This is the name of the layout that is registered by EyeGazeInteraction in the OpenXR Plugin package
        const string k_EyeGazeLayoutName = "EyeGaze";

        [SerializeField]
        [Tooltip("Enable fallback to head tracking if eye tracking is unavailable.")]
        bool m_FallbackIfEyeTrackingUnavailable = true;

        /// <summary>
        /// Enable fallback to head tracking if eye tracking is unavailable.
        /// </summary>
        public bool fallbackIfEyeTrackingUnavailable
        {
            get => m_FallbackIfEyeTrackingUnavailable;
            set => m_FallbackIfEyeTrackingUnavailable = value;
        }


        bool m_EyeTrackingDeviceFound;

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void Awake()
        {
            // Check if we have eye tracking support
            var inputDeviceList = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.EyeTracking, inputDeviceList);
            if (inputDeviceList.Count > 0)
            {
                Debug.Log("Eye tracking device found!", this);
                m_EyeTrackingDeviceFound = true;
                return;
            }

            foreach (var device in InputSystem.InputSystem.devices)
            {
                if (device.layout == k_EyeGazeLayoutName)
                {
                    Debug.Log("Eye gaze device found!", this);
                    m_EyeTrackingDeviceFound = true;
                    return;
                }
            }

            Debug.LogWarning($"Could not find a device that supports eye tracking on Awake. {this} has subscribed to device connected events and will activate the GameObject when an eye tracking device is connected.", this);

            InputDevices.deviceConnected += OnDeviceConnected;
            InputSystem.InputSystem.onDeviceChange += OnDeviceChange;

            gameObject.SetActive(m_FallbackIfEyeTrackingUnavailable);
        }

        /// <summary>
        /// See <see cref="MonoBehaviour"/>.
        /// </summary>
        protected void OnDestroy()
        {
            InputDevices.deviceConnected -= OnDeviceConnected;
            InputSystem.InputSystem.onDeviceChange -= OnDeviceChange;
        }

        void OnDeviceConnected(InputDevice inputDevice)
        {
            if (m_EyeTrackingDeviceFound || !inputDevice.characteristics.HasFlag(InputDeviceCharacteristics.EyeTracking))
                return;

            Debug.Log("Eye tracking device found!", this);
            m_EyeTrackingDeviceFound = true;
            gameObject.SetActive(true);
        }

        void OnDeviceChange(InputSystem.InputDevice device, InputDeviceChange change)
        {
            if (m_EyeTrackingDeviceFound || change != InputDeviceChange.Added)
                return;

            if (device.layout == k_EyeGazeLayoutName)
            {
                Debug.Log("Eye gaze device found!", this);
                m_EyeTrackingDeviceFound = true;
                gameObject.SetActive(true);
            }
        }

        /* IsGazing is a method that returns a boolean to check if the user is currently gazing at a particular
        * game object, like the captions.
        */
        public bool IsGazing(GameObject target) {

            if (!m_EyeTrackingDeviceFound) return false;

            var eyeGazeDevice = InputSystem.InputSystem.devices.Find(d => d.layout == k_EyeGazeLayoutName);
            if (eyeGazeDevice == null) return false;

            var eyeGazeControl = eyeGazeDevice.TryGetChildControl<Vector3Control>("gazePoint");
            if (eyeGazeControl == null) return false;

            var gazePoint = eyeGazeControl.ReadValue();
            var direction = (gazePoint - Camera.main.transform.position).normalized;

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit)) {
                return hit.collider.gameObject == target;
            }

            return false;

        }
    }
}
