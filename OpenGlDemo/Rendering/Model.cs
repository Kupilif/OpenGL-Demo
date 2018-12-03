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

        private VertexBufferObject _vbo;
        private ElementBufferObject _ebo;
        private VertexArrayObject _vao;

        public Model(float[] vertexes, int vertexesCount, Vector3 position, Action bindAttributes)
        {
            _useIndices = false;
            _vertexesCount = vertexesCount;
            Matrix = Matrix4x4.CreateTranslation(position);
            Initialize(vertexes, null, bindAttributes);
        }

        public Model(float[] vertexes, uint[] indices, int vertexesCount, Vector3 position, Action bindAttributes)
        {
            _useIndices = true;
            _vertexesCount = vertexesCount;
            Matrix = Matrix4x4.CreateTranslation(position);
            Initialize(vertexes, indices, bindAttributes);
        }

        public void Transform(Matrix4x4 transformMatrix)
        {
            Matrix *= transformMatrix;
        }

        public void Draw()
        {
            _vao.Bind();

            if (_useIndices)
            {
                Gl.DrawElements(PrimitiveType.Triangles, _vertexesCount, DrawElementsType.UnsignedInt, null);
            }
            else
            {
                Gl.DrawArrays(PrimitiveType.Triangles, 0, _vertexesCount);
            }

            _vao.Unbind();
        }

        public void Dispose()
        {
            _vbo?.Dispose();
            _ebo?.Dispose();
            _vao?.Dispose();
        }

        private void Initialize(float[] vertexes, uint[] indices, Action bindAttributes)
        {
            _vao = new VertexArrayObject();

            _vao.Bind();

            _vbo = new VertexBufferObject(vertexes);

            if (indices != null)
            {
                _ebo = new ElementBufferObject(indices);
            }

            bindAttributes();

            _vao.Unbind();
        }
    }
}
