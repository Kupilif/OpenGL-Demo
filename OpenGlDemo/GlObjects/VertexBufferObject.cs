using OpenGL;

namespace OpenGlDemo.GlObjects
{
    public class VertexBufferObject : BufferObject<float>
    {
        public VertexBufferObject(float[] vertexes) 
            : base(vertexes, BufferTarget.ArrayBuffer, sizeof(float))
        {
        }

        public VertexBufferObject(float[] vertexes, BufferUsage usage) 
            : base(vertexes, usage, BufferTarget.ArrayBuffer, sizeof(float))
        {
        }
    }
}
