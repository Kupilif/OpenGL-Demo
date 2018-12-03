using System.Collections.Generic;
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

        public void Render(FigureShaderProgram figureShaderProgram)
        {
            figureShaderProgram.Use();
            figureShaderProgram.BindVertexArrayObject();

            float color = 1f;
            Gl.Uniform4f(figureShaderProgram.UniformLocationColor, 1, ref color);

            foreach (var figure in _figures)
            {
                var modelMatrix = figure.Matrix;
                Gl.UniformMatrix4f(figureShaderProgram.UniformLocationModel, 1, false, ref modelMatrix);
                figure.Draw();
            }

            figureShaderProgram.UnbindVertexArrayObject();
        }
    }
}
