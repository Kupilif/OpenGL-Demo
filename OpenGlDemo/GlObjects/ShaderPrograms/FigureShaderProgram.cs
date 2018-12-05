using System;
using OpenGL;

namespace OpenGlDemo.GlObjects.ShaderPrograms
{
    public class FigureShaderProgram : ShaderProgram
    {
        public int UniformLocationModel { get; private set; }
        public int UniformLocationView { get; private set; }
        public int UniformLocationProjection { get; private set; }

        public int UniformLocationMaterialAmbient { get; private set; }
        public int UniformLocationMaterialDiffuse { get; private set; }
        public int UniformLocationMaterialSpecular { get; private set; }
        public int UniformLocationMaterialShininess { get; private set; }
        public int UniformLocationLightPosition { get; private set; }
        public int UniformLocationLightAmbient { get; private set; }
        public int UniformLocationLightDiffuse { get; private set; }
        public int UniformLocationLightSpecular { get; private set; }
        public int UniformLocationCameraPosition { get; private set; }

        public FigureShaderProgram(string[] vertexShaderSource, string[] fragmentShaderSource)
            : base(vertexShaderSource, fragmentShaderSource)
        {

        }

        public override void BindAttributes()
        {
            Gl.VertexAttribPointer(0, 3, VertexAttribType.Float, false, 6 * sizeof(float), IntPtr.Zero);
            Gl.EnableVertexAttribArray(0);
            Gl.VertexAttribPointer(1, 3, VertexAttribType.Float, false, 6 * sizeof(float), new IntPtr(3 * sizeof(float)));
            Gl.EnableVertexAttribArray(1);
        }

        protected override void SetUpLocations()
        {
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

            UniformLocationMaterialAmbient = Gl.GetUniformLocation(Id, "material.ambient");

            if (UniformLocationMaterialAmbient < 0)
            {
                throw new InvalidOperationException("No uniform 'material.ambient'");
            }

            UniformLocationMaterialDiffuse = Gl.GetUniformLocation(Id, "material.diffuse");

            if (UniformLocationMaterialDiffuse < 0)
            {
                throw new InvalidOperationException("No uniform 'material.diffuse'");
            }

            UniformLocationMaterialSpecular = Gl.GetUniformLocation(Id, "material.specular");

            if (UniformLocationMaterialSpecular < 0)
            {
                throw new InvalidOperationException("No uniform 'material.specular'");
            }

            UniformLocationMaterialShininess = Gl.GetUniformLocation(Id, "material.shininess");

            if (UniformLocationMaterialShininess < 0)
            {
                throw new InvalidOperationException("No uniform 'material.shininess'");
            }

            UniformLocationLightAmbient = Gl.GetUniformLocation(Id, "light.ambient");

            if (UniformLocationLightAmbient < 0)
            {
                throw new InvalidOperationException("No uniform 'light.ambient'");
            }

            UniformLocationLightDiffuse = Gl.GetUniformLocation(Id, "light.diffuse");

            if (UniformLocationLightDiffuse < 0)
            {
                throw new InvalidOperationException("No uniform 'light.diffuse'");
            }

            UniformLocationLightSpecular = Gl.GetUniformLocation(Id, "light.specular");

            if (UniformLocationLightSpecular < 0)
            {
                throw new InvalidOperationException("No uniform 'light.specular'");
            }

            UniformLocationLightPosition = Gl.GetUniformLocation(Id, "light.position");

            if (UniformLocationLightPosition < 0)
            {
                throw new InvalidOperationException("No uniform 'light.position'");
            }

            UniformLocationCameraPosition = Gl.GetUniformLocation(Id, "cameraPosition");

            if (UniformLocationCameraPosition < 0)
            {
                throw new InvalidOperationException("No uniform 'cameraPosition'");
            }
        }
    }
}
