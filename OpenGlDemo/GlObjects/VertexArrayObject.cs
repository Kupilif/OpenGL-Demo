using System;
using OpenGL;

namespace OpenGlDemo.GlObjects
{
    public class VertexArrayObject : IDisposable
    {
        public uint Id { get; }

        public VertexArrayObject()
        {
            Id = Gl.GenVertexArray();
        }

        public void Bind()
        {
            Gl.BindVertexArray(Id);
        }

        public void Unbind()
        {
            Gl.BindVertexArray(0);
        }

        public void Dispose()
        {
            Gl.DeleteVertexArrays(Id);
        }
    }
}
