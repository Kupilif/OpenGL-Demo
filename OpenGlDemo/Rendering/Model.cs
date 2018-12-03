using System;
using System.Numerics;
using OpenGlDemo.GlObjects;
using OpenGL;

namespace OpenGlDemo.Rendering
{
    public class Model : IDisposable
    {
        public Matrix4x4 Matrix { get; private set; }

        private readonly bool _useIndices;
        private readonly int _vertexesCount;

        private readonly VertexBufferObject _vbo;
        private readonly ElementBufferObject _ebo;

        public Model(float[] vertexes, int vertexesCount)
        {
            _useIndices = false;
            _vertexesCount = vertexesCount;
            _vbo = new VertexBufferObject(vertexes);
            Matrix = Matrix4x4.Identity;
        }

        public Model(float[] vertexes, uint[] indices, int vertexesCount)
        {
            _useIndices = true;
            _vertexesCount = vertexesCount;
            _vbo = new VertexBufferObject(vertexes);
            _ebo = new ElementBufferObject(indices);
            Matrix = Matrix4x4.Identity;
        }

        public void Transform(Matrix4x4 transformMatrix)
        {
            Matrix *= transformMatrix;
        }

        public void Draw()
        {
            _vbo.Bind();

            if (_useIndices)
            {
                _ebo.Bind();
                Gl.DrawElements(PrimitiveType.Triangles, _vertexesCount, DrawElementsType.UnsignedInt, null);
            }
            else
            {
                Gl.DrawArrays(PrimitiveType.Triangles, 0, _vertexesCount);
            }
        }

        public void Dispose()
        {
            _vbo?.Dispose();
            _ebo?.Dispose();
        }
    }
}
