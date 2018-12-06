using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using OpenGlDemo.GlObjects;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Lighting;
using OpenGlDemo.Materials;
using OpenGlDemo.Models;
using OpenGlDemo.Motion;
using OpenGlDemo.Rendering;
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
        private readonly MaterialType _defaultMaterial = MaterialType.Bronze;
        private readonly LightType _defaultLightType = LightType.Ambient;

        private Model _figure;
        private IScene _currentScene;
        private IScene _singleObjectScene;
        private IScene _multipleObjectsSceneStartInCenter;
        private IScene _multipleObjectsSceneStartOutside;
        private Light _light;
        private ShaderProgram _figureShaderProgram;
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

            _light = LightBuilder.CreateWhiteLight(LightType.Ambient);
            _light.Type = _defaultLightType;

            _figure = ModelFactory.CreateCube(new Vector3(0f, 0f, 0f), _figureShaderProgram.BindAttributes);
            _figure.Material = _defaultMaterial;

            _singleObjectScene = new SingleObjectScene(new Vector3(0f, 0f, 3f));
            _singleObjectScene.AddFigure(_figure);

            _multipleObjectsSceneStartInCenter = new MultipleRandomObjectsScene(new MultipleRandomObjectsScene.Args
            {
                CameraPosition = new Vector3(0f, 0f, 0f),
                MinObjectPostion = new Vector3(-50f, -50f, -50f),
                MaxObjectPosition = new Vector3(50f, 50f, 50f),
                ObjectsCount = 10000
            });
            _multipleObjectsSceneStartInCenter.AddFigure(_figure);

            _multipleObjectsSceneStartOutside = new MultipleRandomObjectsScene(new MultipleRandomObjectsScene.Args
            {
                CameraPosition = new Vector3(60f, 60f, 60f),
                MinObjectPostion = new Vector3(-50f, -50f, -50f),
                MaxObjectPosition = new Vector3(50f, 50f, 50f),
                ObjectsCount = 10000
            });
            _multipleObjectsSceneStartOutside.AddFigure(_figure);

            _currentScene = _singleObjectScene;
        }

        private void glControl_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _currentScene.Render(senderControl.ClientSize.Width, senderControl.ClientSize.Height, _light, _figureShaderProgram);
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
                        _currentScene.MoveCamera(CameraMove.Left, _cameraMoveStep);
                        break;
                    case Keys.D:
                        _currentScene.MoveCamera(CameraMove.Right, _cameraMoveStep);
                        break;
                    case Keys.W:
                        _currentScene.MoveCamera(CameraMove.Forward, _cameraMoveStep);
                        break;
                    case Keys.S:
                        _currentScene.MoveCamera(CameraMove.Backward, _cameraMoveStep);
                        break;
                }
            }

            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        _light = LightBuilder.CreateWhiteLight(_light.Type);
                        break;
                    case Keys.D2:
                        _light = LightBuilder.CreateDimWhiteLight(_light.Type);
                        break;
                    case Keys.F1:
                        _currentScene = _singleObjectScene;
                        break;
                    case Keys.F2:
                        _currentScene = _multipleObjectsSceneStartInCenter;
                        break;
                    case Keys.F3:
                        _currentScene = _multipleObjectsSceneStartOutside;
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.NumPad4:
                        _currentScene.RotateFigures(FigureRotation.OY, -_figureRotationAngle);
                        break;
                    case Keys.NumPad6:
                        _currentScene.RotateFigures(FigureRotation.OY, _figureRotationAngle);
                        break;
                    case Keys.NumPad8:
                        _currentScene.RotateFigures(FigureRotation.OX, -_figureRotationAngle);
                        break;
                    case Keys.NumPad2:
                        _currentScene.RotateFigures(FigureRotation.OX, _figureRotationAngle);
                        break;
                    case Keys.NumPad7:
                        _currentScene.RotateFigures(FigureRotation.OZ, _figureRotationAngle);
                        break;
                    case Keys.NumPad9:
                        _currentScene.RotateFigures(FigureRotation.OZ, -_figureRotationAngle);
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
            }

            glControl.Refresh();
        }

        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isCameraMoveEnabled)
            {
                _currentScene.ChangeCameraView(
                    e.Location.X - _previousMousPosition.X, _previousMousPosition.Y - e.Location.Y, _mouseSensitivity);
                glControl.Refresh();
            }

            _previousMousPosition = e.Location;
        }

        private void glControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_isCameraMoveEnabled)
            {
                _currentScene.ChangeCameraZoom(e.Delta, _mouseWheelSensitivity);
                glControl.Refresh();
            }
        }
    }
}
