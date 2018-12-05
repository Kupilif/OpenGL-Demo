using System;
using OpenGL;

namespace OpenGlDemo.GlObjects.ShaderPrograms
{
    public class FigureShaderProgram : ShaderProgram
    {
        public int UModel { get; private set; }
        public int UView { get; private set; }
        public int UProjection { get; private set; }

        public int UMaterialAmbient { get; private set; }
        public int UMaterialDiffuse { get; private set; }
        public int UMaterialSpecular { get; private set; }
        public int UMaterialShininess { get; private set; }

        public int ULightPosition { get; private set; }
        public int ULightAmbient { get; private set; }
        public int ULightDiffuse { get; private set; }
        public int ULightSpecular { get; private set; }

        public int UCameraPosition { get; private set; }

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
            UModel = GetUniformLocation("model");
            UView = GetUniformLocation("view");
            UProjection = GetUniformLocation("projection");

            UMaterialAmbient = GetUniformLocation("material.ambient");
            UMaterialDiffuse = GetUniformLocation("material.diffuse");
            UMaterialSpecular = GetUniformLocation("material.specular");
            UMaterialShininess = GetUniformLocation("material.shininess");

            ULightAmbient = GetUniformLocation("light.ambient");
            ULightDiffuse = GetUniformLocation("light.diffuse");
            ULightSpecular = GetUniformLocation("light.specular");
            ULightPosition = GetUniformLocation("light.position");

            UCameraPosition = GetUniformLocation("cameraPosition");
        }
    }
}
