using System;
using OpenGL;

namespace OpenGlDemo.GlObjects.ShaderPrograms
{
    public class FigureShaderProgram : ShaderProgram
    {
        public int AttributeLocationPosition { get; private set; }
        public int UniformLocationColor { get; private set; }
        public int UniformLocationModel { get; private set; }

        public FigureShaderProgram(string[] vertexShaderSource, string[] fragmentShaderSource)
            : base(vertexShaderSource, fragmentShaderSource)
        {

        }

        public override void SetUpLocations()
        {
            AttributeLocationPosition = Gl.GetAttribLocation(Id, "position");

            if (AttributeLocationPosition < 0)
            {
                throw new InvalidOperationException("No attribute 'position'");
            }

            UniformLocationColor = Gl.GetUniformLocation(Id, "color");

            if (UniformLocationColor < 0)
            {
                throw new InvalidOperationException("No uniform 'color'");
            }

            UniformLocationModel = Gl.GetUniformLocation(Id, "model");

            if (UniformLocationModel < 0)
            {
                throw new InvalidOperationException("No uniform 'model'");
            }
        }

        protected override void BindAttributes()
        {
            Gl.VertexAttribPointer((uint)AttributeLocationPosition, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray((uint)AttributeLocationPosition);
        }
    }
}
