using System;
using System.Collections.Generic;
using System.Numerics;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGL;

namespace OpenGlDemo.Rendering
{
    public class SimpleScene : IScene
    {
        private readonly List<Model> _figures;

        public SimpleScene()
        {
            _figures = new List<Model>();
        }

        public void AddFigure(Model model)
        {
            _figures.Add(model);
        }

        public void Render(int width, int height, FigureShaderProgram figureShaderProgram)
        {
            figureShaderProgram.Use();
            figureShaderProgram.BindVertexArrayObject();

            float color = 1f;
            Gl.Uniform4f(figureShaderProgram.UniformLocationColor, 1, ref color);

            var matrix = Matrix4x4.CreateLookAt(new Vector3(0f, 0f, 5f), new Vector3(0f, 0f, 0f),
                new Vector3(0f, 1f, 0f));
            Gl.UniformMatrix4f(figureShaderProgram.UniformLocationView, 1, false, ref matrix);

            matrix = Matrix4x4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float) width / (float) height, 0.1f, 100f);
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
