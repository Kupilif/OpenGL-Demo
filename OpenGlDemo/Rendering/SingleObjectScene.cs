using System;
using System.Numerics;
using OpenGlDemo.Extensions;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Lighting;
using OpenGlDemo.Materials;
using OpenGlDemo.Motion;
using OpenGL;

namespace OpenGlDemo.Rendering
{
    public class SingleObjectScene : IScene
    {
        private readonly Camera _camera;
        private Model _figure;

        public SingleObjectScene(Vector3 cameraPosition)
        {
            _camera = new Camera(cameraPosition);
        }

        public void AddFigure(Model model)
        {
            _figure = model;
        }

        public void RotateFigures(FigureRotation direction, float angle)
        {
            Matrix4x4 matrix = Matrix4x4.Identity;

            switch (direction)
            {
                case FigureRotation.OX:
                    matrix = Matrix4x4.CreateRotationX(angle);
                    break;
                case FigureRotation.OY:
                    matrix = Matrix4x4.CreateRotationY(angle);
                    break;
                case FigureRotation.OZ:
                    matrix = Matrix4x4.CreateRotationZ(angle);
                    break;
            }

            _figure.Transform(matrix);
        }

        public void MoveCamera(CameraMove direction, float distance)
        {
            _camera.Move(direction, distance);
        }

        public void ChangeCameraView(float xOffset, float yOffset, float sensitivity)
        {
            _camera.ChangeView(xOffset, yOffset, sensitivity);
        }

        public void ChangeCameraZoom(float offset, float sensitivity)
        {
            _camera.ChangeZoom(offset, sensitivity);
        }

        public void Render(int width, int height, Light light, FigureShaderProgram figureShaderProgram)
        {
            figureShaderProgram.Use();

            Matrix4x4 matrix = _camera.GetViewMatrix();
            Gl.UniformMatrix4f(figureShaderProgram.UView, 1, false, ref matrix);
            matrix = Matrix4x4.CreatePerspectiveFieldOfView(_camera.Zoom.ToRadians(), (float) width / (float) height, 0.1f, 100f);
            Gl.UniformMatrix4f(figureShaderProgram.UProjection, 1, false, ref matrix);
            matrix = _figure.Matrix;
            Gl.UniformMatrix4f(figureShaderProgram.UModel, 1, false, ref matrix);

            Material material = MaterialBuilder.Create(_figure.Material);
            Gl.Uniform3(figureShaderProgram.UMaterialAmbient, material.Ambient.X, material.Ambient.Y, material.Ambient.Z);
            Gl.Uniform3(figureShaderProgram.UMaterialDiffuse, material.Diffuse.X, material.Diffuse.Y, material.Diffuse.Z);
            Gl.Uniform3(figureShaderProgram.UMaterialSpecular, material.Specular.X, material.Specular.Y, material.Specular.Z);
            Gl.Uniform1(figureShaderProgram.UMaterialShininess, material.Shininess);

            Gl.Uniform1(figureShaderProgram.ULightType, (int) light.Type);

            Gl.Uniform3(figureShaderProgram.ULightPosition, _camera.Position.X, _camera.Position.Y, _camera.Position.Z);
            Gl.Uniform3(figureShaderProgram.ULightDirection, _camera.Front.X, _camera.Front.Y, _camera.Front.Z);

            Gl.Uniform3(figureShaderProgram.ULightAmbient, light.Ambient.X, light.Ambient.Y, light.Ambient.Z);
            Gl.Uniform3(figureShaderProgram.ULightDiffuse, light.Diffuse.X, light.Diffuse.Y, light.Diffuse.Z);
            Gl.Uniform3(figureShaderProgram.ULightSpecular, light.Specular.X, light.Specular.Y, light.Specular.Z);

            Gl.Uniform1(figureShaderProgram.ULightConstant, light.ConstantCoefficient);
            Gl.Uniform1(figureShaderProgram.ULightLinear, light.LinearCoefficient);
            Gl.Uniform1(figureShaderProgram.ULightQuadratic, light.QuadraticCoefficient);

            Gl.Uniform1(figureShaderProgram.ULightInnerCutOff, light.InnerCutOff);
            Gl.Uniform1(figureShaderProgram.ULightOuterCutOff, light.OuterCutOff);

            Gl.Uniform3(figureShaderProgram.UCameraPosition, _camera.Position.X, _camera.Position.Y, _camera.Position.Z);

            _figure.Draw();
        }
    }
}
