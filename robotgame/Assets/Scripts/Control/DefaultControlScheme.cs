using UnityEngine;

namespace Control
{
    public class DefaultControlScheme : IControlScheme
    {
        public bool Backward()
        {
            return Input.GetKey(KeyCode.S);
        }

        public bool DestroyCube()
        {
            return Input.GetMouseButtonDown(1);
        }

        public bool Down()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }

        public bool Forward()
        {
            return Input.GetKey(KeyCode.W);
        }

        public bool Left()
        {
            return Input.GetKey(KeyCode.A);
        }

        public bool PlaceCube()
        {
            return Input.GetMouseButtonDown(0);
        }

        public bool Right()
        {
            return Input.GetKey(KeyCode.D);
        }

        public bool Up()
        {
            return Input.GetKey(KeyCode.Space);
        }
    }
}