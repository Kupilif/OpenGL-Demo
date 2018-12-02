using System;
using System.Text;
using OpenGL;

namespace OpenGlDemo.GlObjects
{
    public class Shader : IDisposable
    {
        private readonly int _logMaxLength = 1024;

        public uint Id { get; private set; }

        public Shader(ShaderType type, string[] source)
        {
            Compile(type, source);
            CheckCompileErrors();
        }

        public void Dispose()
        {
            Gl.DeleteShader(Id);
        }

        private void Compile(ShaderType type, string[] source)
        {
            Id = Gl.CreateShader(type);
            Gl.ShaderSource(Id, source);
            Gl.CompileShader(Id);
        }

        private void CheckCompileErrors()
        {
            Gl.GetShader(Id, ShaderParameterName.CompileStatus, out var isCompiled);

            if (isCompiled == 0)
            {              
                StringBuilder infolog = new StringBuilder(_logMaxLength);

                Gl.GetShaderInfoLog(Id, _logMaxLength, out _, infolog);

                throw new InvalidOperationException($"Unable to compile shader: {infolog}");
            }           
        }
    }
}
