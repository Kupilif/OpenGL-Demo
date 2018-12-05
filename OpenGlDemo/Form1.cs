using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Lighting;
using OpenGlDemo.Materials;
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
        private readonly float _mouseSensitivity = 0.25f;
        private readonly float _mouseWheelSensitivity = 0.05f;
        private readonly Vector3 _cameraStartPosition = new Vector3(0f, 0f, 3f);
        private readonly MaterialType _defaultMaterial = MaterialType.Obsidian;
        private readonly LightType _defaultLightType = LightType.Point;

        private Model _figure;
        private IScene _scene;
        private Light _light;
        private FigureShaderProgram _figureShaderProgram;
        private bool _isCameraMoveEnabled = false;
        private Point _previousMousPosition;

        public Form1()
        {
            InitializeComponent();
            glControl.MouseWheel += glControl_MouseWheel;
        }

        private void glControl_ContextCreated(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;

            _previousMousPosition = new Point(senderControl.ClientSize.Width / 2, senderControl.ClientSize.Height / 2);

            Gl.Enable(EnableCap.DepthTest);

            string vertexShader =
                File.ReadAllText(
                    $"{GlobalConfig.CurrentDirectory}/{GlobalConfig.ShadersDirectory}/{GlobalConfig.FigureVertexShader}");
            string fragmentShader =
                File.ReadAllText(
                    $"{GlobalConfig.CurrentDirectory}/{GlobalConfig.ShadersDirectory}/{GlobalConfig.FigureFragmentShader}");

            _figureShaderProgram = new FigureShaderProgram(new[] { vertexShader }, new[] { fragmentShader });
            _scene = new SingleObjectScene(_cameraStartPosition);

            _light = LightBuilder.CreateWhiteLight();
            _light.Type = _defaultLightType;

            _figure = ModelFactory.CreateCube(new Vector3(0f, 0f, 0f), _figureShaderProgram.BindAttributes);
            _figure.Material = _defaultMaterial;

            _scene.AddFigure(_figure);
        }

        private void glControl_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _scene.Render(senderControl.ClientSize.Width, senderControl.ClientSize.Height, _light, _figureShaderProgram);
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
                case Keys.D1:
                    _figure.Material = MaterialType.Gold;
                    break;
                case Keys.D2:
                    _figure.Material = MaterialType.Silver;
                    break;
                case Keys.D3:
                    _figure.Material = MaterialType.Bronze;
                    break;
                case Keys.D4:
                    _figure.Material = MaterialType.GreenRubber;
                    break;
                case Keys.D5:
                    _figure.Material = MaterialType.RedPlastic;
                    break;
                case Keys.D6:
                    _figure.Material = MaterialType.Obsidian;
                    break;
                case Keys.D7:
                    _figure.Material = MaterialType.Emerald;
                    break;
                case Keys.D8:
                    _figure.Material = MaterialType.Ruby;
                    break;
                case Keys.D9:
                    _figure.Material = MaterialType.Brass;
                    break;
                case Keys.D0:
                    _figure.Material = MaterialType.Chrome;
                    break;
                case Keys.OemMinus:
                    _figure.Material = MaterialType.Copper;
                    break;
                case Keys.Oemplus:
                    _figure.Material = MaterialType.Pearl;
                    break;
                case Keys.F1:
                    _light.Type = LightType.Ambient;
                    break;
                case Keys.F2:
                    _light.Type = LightType.Directional;
                    break;
                case Keys.F3:
                    _light.Type = LightType.Point;
                    break;
                case Keys.F4:
                    _light.Type = LightType.Spotlight;
                    break;
            }

            glControl.Refresh();
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isCameraMoveEnabled)
            {
                _scene.ChangeCameraView(
                    e.Location.X - _previousMousPosition.X, _previousMousPosition.Y - e.Location.Y, _mouseSensitivity);
                _previousMousPosition = e.Location;
                glControl.Refresh();
            }
        }

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_isCameraMoveEnabled)
            {
                _scene.ChangeCameraZoom(e.Delta, _mouseWheelSensitivity);
                glControl.Refresh();
            }
        }
    }
}
