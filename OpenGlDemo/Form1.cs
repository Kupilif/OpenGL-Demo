using System.IO;
using System.Windows.Forms;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Rendering;
using OpenGlDemo.Settings;
using OpenGL;

namespace OpenGlDemo
{
    public partial class Form1 : Form
    {
        private readonly Model _triangle = new Model
        {
            UseIndices = false,
            VertexesCount = 3,
            Vertexes = new [] 
            {
                -0.5f, -0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                0.0f,  0.5f, 0.0f
            }
        };

        private readonly Model _rectangle = new Model
        {
            UseIndices = true,
            VertexesCount = 6,
            Vertexes = new[]
            {
                0.5f, 0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
                -0.5f, 0.5f, 0.0f
            },
            Indices = new uint[]
            {
                0, 1, 3,
                1, 2, 3
            }
        };

        private IScene _scene;
        private FigureShaderProgram _figureShaderProgram;

        private bool _isTriangle = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void AddFigure()
        {
            if (_isTriangle)
            {
                _scene.AddFigure(_triangle);
            }
            else
            {
                _scene.AddFigure(_rectangle);
            }
        }

        private void glControl_ContextCreated(object sender, GlControlEventArgs e)
        {
            Gl.Enable(EnableCap.DepthTest);

            string vertexShader =
                File.ReadAllText(
                    $"{GlobalConfig.CurrentDirectory}/{GlobalConfig.ShadersDirectory}/{GlobalConfig.FigureVertexShader}");
            string fragmentShader =
                File.ReadAllText(
                    $"{GlobalConfig.CurrentDirectory}/{GlobalConfig.ShadersDirectory}/{GlobalConfig.FigureFragmentShader}");
            
            _figureShaderProgram = new FigureShaderProgram(new[] { vertexShader }, new[] { fragmentShader });
            _figureShaderProgram.SetUpLocations();
            _scene = new SimpleScene(_figureShaderProgram);

            AddFigure();
        }

        private void glControl_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _scene.Render();
        }

        private void glControl_ContextDestroying(object sender, GlControlEventArgs e)
        {
            _scene?.Dispose();
            _figureShaderProgram?.Dispose();
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            _isTriangle = !_isTriangle;
            AddFigure();
            glControl.Refresh();
        }
    }
}
