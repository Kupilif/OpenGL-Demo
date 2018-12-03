using System;

namespace OpenGlDemo.Extensions
{
    public static class MathExtensions
    {
        public static float ToRadians(this float degree)
        {
            return (float) degree * (float)Math.PI / 180;
        }
    }
}
