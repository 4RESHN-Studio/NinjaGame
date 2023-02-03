using Assets.Materials.Resources.Scripts;

namespace Assets.Scripts.Helpers
{
    public class Helper
    {
        public static Direction GetOppositeDirection(Direction direction)
        {
            if (direction == Direction.None)
                return Direction.None;

            return direction == Direction.Right ? Direction.Left : Direction.Right;
        }

    }
}
