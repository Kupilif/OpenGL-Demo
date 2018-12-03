using System.Numerics;
using OpenGlDemo.GlObjects.ShaderPrograms;

namespace OpenGlDemo.Rendering
{
    public interface IScene
    {
        void AddFigure(Model model, Vector3 position);
        void Render(FigureShaderProgram figureShaderProgram);
    }
}
