using System;
using System.Text;
using OpenGL;

namespace OpenGlDemo.GlObjects
{
    public abstract class ShaderProgram : IDisposable
    {
        private readonly int _logMaxLength = 1024;
        private VertexArrayObject _vao;

        public uint Id { get; private set; }

        /// <summary>
        /// Call <see cref="SetUpLocations"/> after creation.
        /// </summary>
        /// <param name="vertexShaderSource"></param>
        /// <param name="fragmentShaderSource"></param>
        public ShaderProgram(string[] vertexShaderSource, string[] fragmentShaderSource)
        {
            Link(vertexShaderSource, fragmentShaderSource);
            CheckLinkErrors();
        }

        /// <summary>
        /// Should be called after creation.
        /// </summary>
        public abstract void SetUpLocations();

        public void CreateVertexArrayObject(VertexBufferObject vbo)
        {
            _vao?.Dispose();

            _vao = new VertexArrayObject();

            BindVertexArrayObject();
            vbo.Bind();

            BindAttributes();

            UnbindVertexArrayObject();
        }

        public void CreateVertexArrayObject(VertexBufferObject vbo, ElementBufferObject ebo)
        {
            _vao?.Dispose();

            _vao = new VertexArrayObject();

            BindVertexArrayObject();
            vbo.Bind();
            ebo.Bind();

            BindAttributes();

            UnbindVertexArrayObject();
        }

        public void Use()
        {
            Gl.UseProgram(Id);
        }

        public void BindVertexArrayObject()
        {
            _vao?.Bind();
        }

        public void UnbindVertexArrayObject()
        {
            _vao?.Unbind();
        }

        public void Dispose()
        {
            Gl.DeleteProgram(Id);
            _vao?.Dispose();
        }

        protected abstract void BindAttributes();

        private void Link(string[] vertexShaderSource, string[] fragmentShaderSource)
        {
            using (var vertexShader = new Shader(ShaderType.VertexShader, vertexShaderSource))
            using (var fragmentShader = new Shader(ShaderType.FragmentShader, fragmentShaderSource))
            {
                Id = Gl.CreateProgram();
                Gl.AttachShader(Id, vertexShader.Id);
                Gl.AttachShader(Id, fragmentShader.Id);
                Gl.LinkProgram(Id);
            }
        }

        private void CheckLinkErrors()
        {
            Gl.GetProgram(Id, ProgramProperty.LinkStatus, out var isLinked);

            if (isLinked == 0)
            {
                StringBuilder infolog = new StringBuilder(_logMaxLength);

                Gl.GetProgramInfoLog(Id, _logMaxLength, out _, infolog);

                throw new InvalidOperationException($"Unable to link program: {infolog}");
            }
        }
    }
}
