using System;
using System.Collections.Generic;
using System.Numerics;
using OpenGlDemo.Extensions;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Motion;
using OpenGL;

namespace OpenGlDemo.Rendering
{
    public class SimpleScene : IScene
    {
        private readonly Camera _camera;
        private readonly List<Model> _figures;

        public SimpleScene(Vector3 cameraPosition)
        {
            _camera = new Camera(cameraPosition);
            _figures = new List<Model>();
        }

        public void AddFigure(Model model)
        {
            _figures.Add(model);
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

            foreach (var figure in _figures)
            {
                figure.Transform(matrix);
            }
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
            figureShaderProgram.BindVertexArrayObject();

            float color = 1f;
            Gl.Uniform4f(figureShaderProgram.UniformLocationColor, 1, ref color);

            var matrix = _camera.GetViewMatrix();
            Gl.UniformMatrix4f(figureShaderProgram.UniformLocationView, 1, false, ref matrix);

            matrix = Matrix4x4.CreatePerspectiveFieldOfView(_camera.Zoom.ToRadians(), (float) width / (float) height, 0.1f, 100f);
            Gl.UniformMatrix4f(figureShaderProgram.UniformLocationProjection, 1, false, ref matrix);

            foreach (var figure in _figures)
            {
                matrix = figure.Matrix;
                Gl.UniformMatrix4f(figureShaderProgram.UniformLocationModel, 1, false, ref matrix);
                figure.Draw();
            }

            figureShaderProgram.UnbindVertexArrayObject();
        }
    }
}
