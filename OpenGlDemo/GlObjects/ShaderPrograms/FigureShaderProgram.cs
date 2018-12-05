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
            UniformLocationModel = GetUniformLocation("model");
            UniformLocationView = GetUniformLocation("view");
            UniformLocationProjection = GetUniformLocation("projection");

            UniformLocationMaterialAmbient = GetUniformLocation("material.ambient");
            UniformLocationMaterialDiffuse = GetUniformLocation("material.diffuse");
            UniformLocationMaterialSpecular = GetUniformLocation("material.specular");
            UniformLocationMaterialShininess = GetUniformLocation("material.shininess");

            UniformLocationLightAmbient = GetUniformLocation("light.ambient");
            UniformLocationLightDiffuse = GetUniformLocation("light.diffuse");
            UniformLocationLightSpecular = GetUniformLocation("light.specular");
            UniformLocationLightPosition = GetUniformLocation("light.position");

            UniformLocationCameraPosition = GetUniformLocation("cameraPosition");
        }
    }
}
