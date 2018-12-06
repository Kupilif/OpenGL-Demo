using System.Numerics;
using OpenGlDemo.GlObjects;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Lighting;
using OpenGlDemo.Models;
using OpenGlDemo.Motion;

namespace OpenGlDemo.Rendering
{
    public abstract class Scene : IScene
    {
        protected readonly Camera _camera;

        public Scene(Vector3 cameraPosition)
        {
            _camera = new Camera(cameraPosition);
        }

        public abstract void AddFigure(Model model);

        public abstract void RotateFigures(FigureRotation direction, float angle);

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

        public void Render(int width, int height, Light light, ShaderProgram shaderProgram)
        {
            if (shaderProgram is FigureShaderProgram figureShaderProgram)
            {
                RenderWithShader(width, height, light, figureShaderProgram);
            }
        }

        protected abstract void RenderWithShader(int width, int height, Light light, FigureShaderProgram shaderProgram);

        protected Matrix4x4 GetRotationMatrix(FigureRotation direction, float angle)
        {
            switch (direction)
            {
                case FigureRotation.OX:
                    return Matrix4x4.CreateRotationX(angle);
                case FigureRotation.OY:
                    return Matrix4x4.CreateRotationY(angle);
                case FigureRotation.OZ:
                    return Matrix4x4.CreateRotationZ(angle);
                default:
                    return Matrix4x4.Identity;
            }
        }
    }
}
