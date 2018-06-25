namespace Control
{
    public interface IControlScheme
    {
        bool Forward();
        bool Backward();
        bool Left();
        bool Right();
        bool Up();
        bool Down();
        bool PlaceCube();
        bool DestroyCube();
    }
}
