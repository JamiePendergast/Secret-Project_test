using UnityEngine;

namespace Build_Mode
{
    public sealed class RaycastComponent : MonoBehaviour
    {
        public LayerMask Layers;
        private RaycastHit raycastHit;

        [SerializeField]
        [Range(0.0f,1000.0f)]
        private float range = 100.0f;

        public bool Test()
        {
            return Physics.Raycast(new Ray(transform.position, transform.forward), out raycastHit, range, Layers, QueryTriggerInteraction.Ignore);
        }

        public Vector3 GetNormal()
        {
            return raycastHit.normal;
        }

        public Vector3 GetPoint()
        {
            return raycastHit.point;
        }

        public Transform GetTransform()
        {
            return raycastHit.transform;
        }
    }
}