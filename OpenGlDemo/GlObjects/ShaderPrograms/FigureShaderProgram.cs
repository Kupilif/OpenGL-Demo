using System;
using System.Numerics;
using OpenGlDemo.Lighting;
using OpenGlDemo.Materials;
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

        public void SetMaterial(Material material)
        {
            Gl.Uniform3(UMaterialAmbient, material.Ambient.X, material.Ambient.Y, material.Ambient.Z);
            Gl.Uniform3(UMaterialDiffuse, material.Diffuse.X, material.Diffuse.Y, material.Diffuse.Z);
            Gl.Uniform3(UMaterialSpecular, material.Specular.X, material.Specular.Y, material.Specular.Z);
            Gl.Uniform1(UMaterialShininess, material.Shininess);
        }

        public void SetLight(Light light, Vector3 position, Vector3 direction)
        {
            Gl.Uniform1(ULightType, (int)light.Type);

            Gl.Uniform3(ULightPosition, position.X, position.Y, position.Z);
            Gl.Uniform3(ULightDirection, direction.X, direction.Y, direction.Z);

            Gl.Uniform3(ULightAmbient, light.Ambient.X, light.Ambient.Y, light.Ambient.Z);
            Gl.Uniform3(ULightDiffuse, light.Diffuse.X, light.Diffuse.Y, light.Diffuse.Z);
            Gl.Uniform3(ULightSpecular, light.Specular.X, light.Specular.Y, light.Specular.Z);

            Gl.Uniform1(ULightConstant, light.ConstantCoefficient);
            Gl.Uniform1(ULightLinear, light.LinearCoefficient);
            Gl.Uniform1(ULightQuadratic, light.QuadraticCoefficient);

            Gl.Uniform1(ULightInnerCutOff, light.InnerCutOff);
            Gl.Uniform1(ULightOuterCutOff, light.OuterCutOff);
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
