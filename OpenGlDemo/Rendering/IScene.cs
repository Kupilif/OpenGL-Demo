using System;

namespace OpenGlDemo.Rendering
{
    public interface IScene : IDisposable
    {
        void AddFigure(Model model);
        void Render();
    }
}
