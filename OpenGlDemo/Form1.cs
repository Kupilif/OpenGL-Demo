using System.IO;
using System.Numerics;
using System.Windows.Forms;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Rendering;
using OpenGlDemo.Rendering.Factory;
using OpenGlDemo.Settings;
using OpenGL;

namespace OpenGlDemo
{
    public partial class Form1 : Form
    {
        private Model _triangle;
        private Model _rectangle;
        private IScene _scene;
        private FigureShaderProgram _figureShaderProgram;

        public Form1()
        {
            InitializeComponent();
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
            
            _triangle = ModelFactory.CreateTriangle(new Vector3(0.3f, 0.1f, 0f));
            _rectangle = ModelFactory.CreateRectangle(new Vector3(-0.3f, 0f, 0f));

            _figureShaderProgram = new FigureShaderProgram(new[] { vertexShader }, new[] { fragmentShader });
            _scene = new SimpleScene();

            _scene.AddFigure(_triangle);
            _scene.AddFigure(_rectangle);
        }

        private void glControl_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _scene.Render(_figureShaderProgram);
        }

        private void glControl_ContextDestroying(object sender, GlControlEventArgs e)
        {
            _figureShaderProgram?.Dispose();
            _triangle?.Dispose();
            _rectangle?.Dispose();
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            glControl.Refresh();
        }
    }
}
