using UnityEngine;

namespace Control
{
    public sealed class ControlSchemeContainer : MonoBehaviour
    {
        private readonly IControlScheme controlScheme = new DefaultControlScheme();

        public IControlScheme Get()
        {
            return controlScheme;
        }
    }
}