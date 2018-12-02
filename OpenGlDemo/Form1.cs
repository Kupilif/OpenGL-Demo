using System;
using System.IO;
using System.Numerics;
using System.Windows.Forms;
using OpenGlDemo.GlObjects;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Settings;
using OpenGL;

namespace OpenGlDemo
{
    public partial class Form1 : Form
    {
        private readonly float[] _triangle =
        {
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f,  0.5f, 0.0f
        };
        private readonly float[] _rectangle =
        {
            0.5f,  0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f
        };

        private readonly uint[] _rectangleIndices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private bool _isTriangle = true;

        private FigureShaderProgram _figureShaderProgram;
        private VertexBufferObject _vboTriangle;
        private VertexBufferObject _vboRectangle;
        private ElementBufferObject _ebo;

        public Form1()
        {
            InitializeComponent();
        }

        private void CreateFigure()
        {
            if (_isTriangle)
            {
                _figureShaderProgram.CreateVertexArrayObject(_vboTriangle);
            }
            else
            {
                _figureShaderProgram.CreateVertexArrayObject(_vboRectangle, _ebo);
            }
        }

        private void glControl_ContextCreated(object sender, GlControlEventArgs e)
        {       
            string vertexShader =
                File.ReadAllText(
                    $"{GlobalConfig.CurrentDirectory}/{GlobalConfig.ShadersDirectory}/{GlobalConfig.FigureVertexShader}");
            string fragmentShader =
                File.ReadAllText(
                    $"{GlobalConfig.CurrentDirectory}/{GlobalConfig.ShadersDirectory}/{GlobalConfig.FigureFragmentShader}");

            _vboTriangle = new VertexBufferObject(_triangle);
            _vboRectangle = new VertexBufferObject(_rectangle);
            _ebo = new ElementBufferObject(_rectangleIndices);

            _figureShaderProgram = new FigureShaderProgram(new [] { vertexShader }, new [] { fragmentShader });
            _figureShaderProgram.SetUpLocations();
            
            CreateFigure();
        }

        private void glControl_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Gl.Enable(EnableCap.DepthTest);

            float color = 1f;

            _figureShaderProgram.Use();
            _figureShaderProgram.BindVao();
            Gl.Uniform4f(_figureShaderProgram.UniformLocationColor, 1, ref color);

            var model = Matrix4x4.CreateTranslation(0f, 0f, 0f);
            Gl.UniformMatrix4f(_figureShaderProgram.UniformLocationModel, 1, false, ref model);

            if (_isTriangle)
            {
                Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
            }
            else
            {
                Gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, null);
            }

            color = 0.5f;
            Gl.Uniform4f(_figureShaderProgram.UniformLocationColor, 1, ref color);
            model = Matrix4x4.CreateTranslation(0.2f, 0f, -0.5f);
            Gl.UniformMatrix4f(_figureShaderProgram.UniformLocationModel, 1, false, ref model);

            if (_isTriangle)
            {
                Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
            }
            else
            {
                Gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, null);
            }

            color = 0.2f;
            Gl.Uniform4f(_figureShaderProgram.UniformLocationColor, 1, ref color);
            model = Matrix4x4.CreateTranslation(-0.2f, 0f, 0.5f);
            Gl.UniformMatrix4f(_figureShaderProgram.UniformLocationModel, 1, false, ref model);

            if (_isTriangle)
            {
                Gl.DrawArrays(PrimitiveType.Triangles, 0, 3);
            }
            else
            {
                Gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, null);
            }

            _figureShaderProgram.UnbindVao();
        }

        private void glControl_ContextDestroying(object sender, GlControlEventArgs e)
        {
            _figureShaderProgram?.Dispose();
            _vboTriangle?.Dispose();
            _vboRectangle?.Dispose();
            _ebo?.Dispose();
        }

        private void glControl_KeyDown(object sender, KeyEventArgs e)
        {
            _isTriangle = !_isTriangle;
            CreateFigure();
            glControl.Refresh();
        }
    }
}
