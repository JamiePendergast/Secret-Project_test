using UnityEngine;

namespace Control
{
    public sealed class CameraWasdMotion : MonoBehaviour
    {
        public ControlSchemeContainer controlScheme;
        private Vector3 internalLocation;

        [Range(0.0f,100.0f)]
        public float Range = 50.0f;

        [Range(1.0f,10.0f)]
        public float MovementSpeed = 3.50f;
    
        private void Update()
        {
            var scheme = controlScheme.Get();
            if(scheme.Forward())
            {
                internalLocation += transform.forward * Time.deltaTime * MovementSpeed;
            }

            if (scheme.Backward())
            {
                internalLocation -= transform.forward * Time.deltaTime * MovementSpeed;
            }

            if (scheme.Left())
            {
                internalLocation -= transform.right * Time.deltaTime * MovementSpeed;
            }

            if (scheme.Right())
            {
                internalLocation += transform.right * Time.deltaTime * MovementSpeed; ;
            }

            if(scheme.Up())
            {
                internalLocation += Vector3.up * MovementSpeed * Time.deltaTime;
            }

            if (scheme.Down())
            {
                internalLocation -= Vector3.up * MovementSpeed * Time.deltaTime;
            }

            internalLocation.x = Mathf.Clamp(internalLocation.x, -Range, Range);
            internalLocation.y = Mathf.Clamp(internalLocation.y, -Range, Range);
            internalLocation.z = Mathf.Clamp(internalLocation.z, -Range, Range);
            transform.position = internalLocation;
        }
    }
}