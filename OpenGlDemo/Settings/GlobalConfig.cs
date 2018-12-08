using System.IO;

namespace OpenGlDemo.Settings
{
    public static class GlobalConfig
    {
        private static string _currentDirectory;

        public static string CurrentDirectory
        {
            get
            {
                if (_currentDirectory == null)
                {
                    _currentDirectory = Directory.GetCurrentDirectory();
                }

                return _currentDirectory;
            }
        }

        public static string ShadersDirectory => "assets/shaders";
        public static string FigureVertexShader => "material-vertex-shader.glsl";
        public static string FigureFragmentShader => "material-fragment-shader.glsl";
    }
}
