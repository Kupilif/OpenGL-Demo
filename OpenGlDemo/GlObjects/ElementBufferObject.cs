using OpenGL;

namespace OpenGlDemo.GlObjects
{
    public class ElementBufferObject : BufferObject<uint>
    {
        public ElementBufferObject(uint[] indices)
            : base(indices, BufferTarget.ElementArrayBuffer, sizeof(uint))
        {
        }

        public ElementBufferObject(uint[] indices, BufferUsage usage)
            : base(indices, usage, BufferTarget.ElementArrayBuffer, sizeof(uint))
        {
        }
    }
}
