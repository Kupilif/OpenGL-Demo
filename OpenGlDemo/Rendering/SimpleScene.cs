using System.Numerics;
using OpenGlDemo.GlObjects;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGL;

namespace OpenGlDemo.Rendering
{
    public class SimpleScene : IScene
    {
        private readonly FigureShaderProgram _figureShaderProgram;

        private Model _figure; 
        private VertexBufferObject _figureVbo;
        private ElementBufferObject _figureEbo;

        private Matrix4x4 _figureModelMatrix;

        public SimpleScene(FigureShaderProgram figureShaderProgram)
        {
            _figureShaderProgram = figureShaderProgram;
            
            _figureModelMatrix = Matrix4x4.CreateTranslation(0f, 0f, 0f);
        }

        public void AddFigure(Model model)
        {
            _figure = model;

            _figureVbo?.Dispose();
            _figureEbo?.Dispose();

            _figureVbo = new VertexBufferObject(model.Vertexes);

            if (model.UseIndices)
            {
                _figureEbo = new ElementBufferObject(model.Indices);
                _figureShaderProgram.CreateVertexArrayObject(_figureVbo, _figureEbo);
            }
            else
            {
                _figureShaderProgram.CreateVertexArrayObject(_figureVbo);
            }
        }

        public void Render()
        {
            if (_figure == null)
            {
                return;
            }

            _figureShaderProgram.Use();
            _figureShaderProgram.BindVertexArrayObject();

            float color = 1f;
            Gl.Uniform4f(_figureShaderProgram.UniformLocationColor, 1, ref color);
            Gl.UniformMatrix4f(_figureShaderProgram.UniformLocationModel, 1, false, ref _figureModelMatrix);

            if (_figure.UseIndices)
            {
                Gl.DrawElements(PrimitiveType.Triangles, _figure.VertexesCount, DrawElementsType.UnsignedInt, null);
            }
            else
            {
                Gl.DrawArrays(PrimitiveType.Triangles, 0, _figure.VertexesCount);
            }

            _figureShaderProgram.UnbindVertexArrayObject();
        }

        public void Dispose()
        {
            _figureVbo?.Dispose();
            _figureEbo?.Dispose();
        }
    }
}
