using SFML.System;

namespace Open_0._0._0.Utils
{
    internal static class MathHelper
    {
        public static Vector2f Normalize(Vector2f direction)
        {
            if (direction.X != 0 && direction.Y != 0)
            {
                // Länge berechnen
                float length = (float)Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);

                // Normalisieren
                direction.X /= length;
                direction.Y /= length;
            }
            return direction;
        }
        public static Vector2f CalculateVectorF(Vector2f vector1, Vector2f vector2)
        {
            return new Vector2f(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }
        public static Vector2i CalculateVectorI(Vector2i vector1, Vector2i vector2)
        {
            return new Vector2i(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }
    }
}