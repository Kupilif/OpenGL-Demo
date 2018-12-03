using System;
using OpenGL;

namespace OpenGlDemo.GlObjects.ShaderPrograms
{
    public class FigureShaderProgram : ShaderProgram
    {
        public int AttributeLocationPosition { get; private set; }
        public int UniformLocationColor { get; private set; }
        public int UniformLocationModel { get; private set; }
        public int UniformLocationView { get; private set; }
        public int UniformLocationProjection { get; private set; }

        public FigureShaderProgram(string[] vertexShaderSource, string[] fragmentShaderSource)
            : base(vertexShaderSource, fragmentShaderSource)
        {

        }

        public override void BindAttributes()
        {
            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(0);
        }

        protected override void SetUpLocations()
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

            UniformLocationView = Gl.GetUniformLocation(Id, "view");

            if (UniformLocationView < 0)
            {
                throw new InvalidOperationException("No uniform 'view'");
            }

            UniformLocationProjection = Gl.GetUniformLocation(Id, "projection");

            if (UniformLocationProjection < 0)
            {
                throw new InvalidOperationException("No uniform 'projection'");
            }
        }
    }
}
