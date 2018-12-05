using System.Collections.Generic;
using System.Numerics;
using OpenGlDemo.Extensions;
using OpenGlDemo.GlObjects.ShaderPrograms;
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

        public void Render(int width, int height, FigureShaderProgram figureShaderProgram)
        {
            figureShaderProgram.Use();

            var matrix = _camera.GetViewMatrix();
            Gl.UniformMatrix4f(figureShaderProgram.UniformLocationView, 1, false, ref matrix);

            matrix = Matrix4x4.CreatePerspectiveFieldOfView(_camera.Zoom.ToRadians(), (float) width / (float) height, 0.1f, 100f);
            Gl.UniformMatrix4f(figureShaderProgram.UniformLocationProjection, 1, false, ref matrix);

            Gl.Uniform3(figureShaderProgram.UniformLocationLightPosition, _camera.Position.X, _camera.Position.Y, _camera.Position.Z);

            Gl.Uniform3(figureShaderProgram.UniformLocationLightAmbient, 1f, 1f, 1f);
            Gl.Uniform3(figureShaderProgram.UniformLocationLightDiffuse, 1f, 1f, 1f);
            Gl.Uniform3(figureShaderProgram.UniformLocationLightSpecular, 1f, 1f, 1f);

            Material material = MaterialBuilder.Create(_figure.Material);

            Gl.Uniform3(figureShaderProgram.UniformLocationMaterialAmbient, material.Ambient.X, material.Ambient.Y, material.Ambient.Z);
            Gl.Uniform3(figureShaderProgram.UniformLocationMaterialDiffuse, material.Diffuse.X, material.Diffuse.Y, material.Diffuse.Z);
            Gl.Uniform3(figureShaderProgram.UniformLocationMaterialSpecular, material.Specular.X, material.Specular.Y, material.Specular.Z);
            Gl.Uniform1(figureShaderProgram.UniformLocationMaterialShininess, material.Shininess);

            Gl.Uniform3(figureShaderProgram.UniformLocationCameraPosition, _camera.Position.X, _camera.Position.Y, _camera.Position.Z);

            matrix = _figure.Matrix;
            Gl.UniformMatrix4f(figureShaderProgram.UniformLocationModel, 1, false, ref matrix);
            _figure.Draw();
        }
    }
}
