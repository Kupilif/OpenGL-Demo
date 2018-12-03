using System.Collections.Generic;
using System.Numerics;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGL;

namespace OpenGlDemo.Rendering
{
    public class SimpleScene : IScene
    {
        private readonly List<(Model obj, Matrix4x4 viewMatrix)> _figures;

        public SimpleScene()
        {
            _figures = new List<(Model obj, Matrix4x4 viewMatrix)>();
        }

        public void AddFigure(Model model, Vector3 position)
        {
            _figures.Add((model, Matrix4x4.CreateTranslation(position)));
        }

        public void Render(FigureShaderProgram figureShaderProgram)
        {
            figureShaderProgram.Use();
            figureShaderProgram.BindVertexArrayObject();

            float color = 1f;
            Gl.Uniform4f(figureShaderProgram.UniformLocationColor, 1, ref color);

            foreach (var figure in _figures)
            {
                var matrix = figure.obj.Matrix;
                Gl.UniformMatrix4f(figureShaderProgram.UniformLocationModel, 1, false, ref matrix);

                matrix = figure.viewMatrix;
                Gl.UniformMatrix4f(figureShaderProgram.UniformLocationView, 1, false, ref matrix);

                figure.obj.Draw();
            }

            figureShaderProgram.UnbindVertexArrayObject();
        }
    }
}
