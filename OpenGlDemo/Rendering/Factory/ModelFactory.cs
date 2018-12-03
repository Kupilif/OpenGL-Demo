using System.Numerics;

namespace OpenGlDemo.Rendering.Factory
{
    public static class ModelFactory
    {
        private static readonly float[] _triangleVertexes =
        {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f,  0.5f, 0.0f
        };
        private static readonly float[] _rectangleVertexes =
        {
            0.5f, 0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f, 0.5f, 0.0f
        };

        private static readonly uint[] _rectangleIndices =
        {
            0, 1, 3,
            1, 2, 3
        };

        public static Model CreateTriangle(Vector3 position)
        {
            return new Model(_triangleVertexes, 3, position);
        }

        public static Model CreateRectangle(Vector3 position)
        {
            return new Model(_rectangleVertexes, _rectangleIndices, 6, position);
        }
    }
}
