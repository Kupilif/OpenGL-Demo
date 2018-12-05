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

        public int ULightType { get; private set; }
        public int ULightPosition { get; private set; }
        public int ULightDirection { get; private set; }
        public int ULightAmbient { get; private set; }
        public int ULightDiffuse { get; private set; }
        public int ULightSpecular { get; private set; }
        public int ULightConstant { get; private set; }
        public int ULightLinear { get; private set; }
        public int ULightQuadratic { get; private set; }
        public int ULightInnerCutOff { get; private set; }
        public int ULightOuterCutOff { get; private set; }

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

            ULightType = GetUniformLocation("light.type");
            ULightPosition = GetUniformLocation("light.position");
            ULightDirection = GetUniformLocation("light.direction");
            ULightAmbient = GetUniformLocation("light.ambient");
            ULightDiffuse = GetUniformLocation("light.diffuse");
            ULightSpecular = GetUniformLocation("light.specular");
            ULightConstant = GetUniformLocation("light.constant");
            ULightLinear = GetUniformLocation("light.linear");
            ULightQuadratic = GetUniformLocation("light.quadratic");
            ULightInnerCutOff = GetUniformLocation("light.innerCutOff");
            ULightOuterCutOff = GetUniformLocation("light.outerCutOff");

            UCameraPosition = GetUniformLocation("cameraPosition");
        }
    }
}
