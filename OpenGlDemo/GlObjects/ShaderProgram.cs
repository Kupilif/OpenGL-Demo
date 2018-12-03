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
            SetUpLocations();
            CreateVertexArrayObject();
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

        protected abstract void SetUpLocations();

        private void CreateVertexArrayObject()
        {
            _vao = new VertexArrayObject();

            BindVertexArrayObject();
            BindAttributes();
            UnbindVertexArrayObject();
        }

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
