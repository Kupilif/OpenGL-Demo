using System;
using OpenGL;

namespace OpenGlDemo.GlObjects
{
    public class BufferObject<T> : IDisposable
        where T : struct
    {
        private readonly BufferTarget _target;
        private readonly int _typeSize;

        public uint Id { get; private set; }

        public BufferObject(T[] items, BufferTarget target, int typeSize)
        {
            _target = target;
            _typeSize = typeSize;
            InitializeBuffer(items, BufferUsage.StaticDraw);
        }

        public BufferObject(T[] items, BufferUsage usage, BufferTarget target, int typeSize)
        {
            _target = target;
            _typeSize = typeSize;
            InitializeBuffer(items, usage);
        }

        public void Bind()
        {
            Gl.BindBuffer(_target, Id);
        }

        public void Dispose()
        {
            Gl.DeleteBuffers(Id);
        }

        private void InitializeBuffer(T[] items, BufferUsage usage)
        {
            Id = Gl.GenBuffer();
            Bind();
            Gl.BufferData(_target, (uint)(_typeSize * items.Length), items, usage);
        }
    }
}
