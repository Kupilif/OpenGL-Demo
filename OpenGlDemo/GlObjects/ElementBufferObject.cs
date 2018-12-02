using OpenGL;

namespace OpenGlDemo.GlObjects
{
    public class ElementBufferObject : BufferObject<int>
    {
        public ElementBufferObject(int[] indices)
            : base(indices, BufferTarget.ArrayBuffer, sizeof(int))
        {
        }

        public ElementBufferObject(int[] indices, BufferUsage usage)
            : base(indices, usage, BufferTarget.ArrayBuffer, sizeof(int))
        {
        }
    }
}
