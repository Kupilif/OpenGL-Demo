using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Motion;

namespace OpenGlDemo.Rendering
{
    public interface IScene
    {
        void AddFigure(Model model);
        void Render(int width, int height, FigureShaderProgram figureShaderProgram);
        void RotateFigures(Rotation direction, float angle);
    }
}
