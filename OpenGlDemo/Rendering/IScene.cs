using OpenGlDemo.GlObjects.ShaderPrograms;

namespace OpenGlDemo.Rendering
{
    public interface IScene
    {
        void AddFigure(Model model);
        void Render(int width, int height, FigureShaderProgram figureShaderProgram);
    }
}
