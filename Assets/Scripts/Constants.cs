namespace Assets.Materials.Resources.Scripts
{
    public enum Axis2D
    {
        None = -1,
        X = 0,
        Y = 1
    }

    public enum Direction
    {
        Left = -1,
        None = 0,
        Right = 1
    }

    public static class Constants
    {
        public static readonly string[] Axes = new string[] { "Horizontal", "Jump" };
        public const float NormalGravity = 2f;
    }
}
