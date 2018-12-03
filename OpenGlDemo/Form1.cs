using System;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Motion;
using OpenGlDemo.Rendering;
using OpenGlDemo.Rendering.Factory;
using OpenGlDemo.Settings;
using OpenGL;

namespace OpenGlDemo
{
    public partial class Form1 : Form
    {
        private readonly float _figureRotationAngle = (float) Math.PI / 36;
        private readonly float _cameraMoveStep = 0.05f;
        private readonly Vector3 _cameraStartPosition = new Vector3(0f, 0f, 5f);

        private Model _figure;
        private IScene _scene;
        private FigureShaderProgram _figureShaderProgram;
        private bool _isCameraMoveEnabled = false;

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
            
            _figure = ModelFactory.CreateCube(new Vector3(0f, 0f, 0f));

            _figureShaderProgram = new FigureShaderProgram(new[] { vertexShader }, new[] { fragmentShader });
            _scene = new SimpleScene(_cameraStartPosition);

            _scene.AddFigure(_figure);
        }

        private void glControl_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _scene.Render(senderControl.ClientSize.Width, senderControl.ClientSize.Height, _figureShaderProgram);
        }

        private void glControl_ContextDestroying(object sender, GlControlEventArgs e)
        {
            _figureShaderProgram?.Dispose();
            _figure?.Dispose();
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isCameraMoveEnabled)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        _scene.MoveCamera(CameraMove.Left, _cameraMoveStep);
                        break;
                    case Keys.D:
                        _scene.MoveCamera(CameraMove.Right, _cameraMoveStep);
                        break;
                    case Keys.W:
                        _scene.MoveCamera(CameraMove.Forward, _cameraMoveStep);
                        break;
                    case Keys.S:
                        _scene.MoveCamera(CameraMove.Backward, _cameraMoveStep);
                        break;
                }
            }

            switch (e.KeyCode)
            {
                case Keys.NumPad4:
                    _scene.RotateFigures(FigureRotation.OY, -_figureRotationAngle);
                    break;
                case Keys.NumPad6:
                    _scene.RotateFigures(FigureRotation.OY, _figureRotationAngle);
                    break;
                case Keys.NumPad8:
                    _scene.RotateFigures(FigureRotation.OX, -_figureRotationAngle);
                    break;
                case Keys.NumPad2:
                    _scene.RotateFigures(FigureRotation.OX, _figureRotationAngle);
                    break;
                case Keys.NumPad7:
                    _scene.RotateFigures(FigureRotation.OZ, _figureRotationAngle);
                    break;
                case Keys.NumPad9:
                    _scene.RotateFigures(FigureRotation.OZ, -_figureRotationAngle);
                    break;
                case Keys.Space:
                    _isCameraMoveEnabled = !_isCameraMoveEnabled;
                    break;
            }

            glControl.Refresh();
        }
    }
}
