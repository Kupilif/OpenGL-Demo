using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Lighting;
using OpenGlDemo.Models;
using OpenGlDemo.Motion;

namespace OpenGlDemo.Rendering
{
    public interface IScene
    {
        void AddFigure(Model model);
        void Render(int width, int height, Light light, FigureShaderProgram figureShaderProgram);
        void RotateFigures(FigureRotation direction, float angle);
        void MoveCamera(CameraMove direction, float distance);
        void ChangeCameraView(float xOffset, float yOffset, float sensitivity);
        void ChangeCameraZoom(float offset, float sensitivity);
    }
}
