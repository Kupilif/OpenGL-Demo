using System.Numerics;
using OpenGlDemo.Extensions;
using OpenGlDemo.GlObjects.ShaderPrograms;
using OpenGlDemo.Lighting;
using OpenGlDemo.Materials;
using OpenGlDemo.Models;
using OpenGlDemo.Motion;
using OpenGL;

namespace OpenGlDemo.Rendering
{
    public class SingleObjectScene : Scene
    {
        private Model _figure;

        public SingleObjectScene(Vector3 cameraPosition) : base(cameraPosition)
        {
        }

        public override void AddFigure(Model model)
        {
            _figure = model;
        }

        public override void RotateFigures(FigureRotation direction, float angle)
        {
            Matrix4x4 matrix = GetRotationMatrix(direction, angle);
            _figure.Transform(matrix);
        }

        protected override void RenderWithShader(int width, int height, Light light, FigureShaderProgram shaderProgram)
        {
            shaderProgram.Use();

            Matrix4x4 matrix = _camera.GetViewMatrix();
            Gl.UniformMatrix4f(shaderProgram.UView, 1, false, ref matrix);
            matrix = Matrix4x4.CreatePerspectiveFieldOfView(_camera.Zoom.ToRadians(), (float) width / (float) height, 0.1f, 100f);
            Gl.UniformMatrix4f(shaderProgram.UProjection, 1, false, ref matrix);
            matrix = _figure.Matrix;
            Gl.UniformMatrix4f(shaderProgram.UModel, 1, false, ref matrix);

            shaderProgram.SetMaterial(MaterialBuilder.Create(_figure.Material));

            var lightDirection = light.Type == LightType.Directional ? light.Direction : _camera.Front;
            shaderProgram.SetLight(light, _camera.Position, lightDirection);

            Gl.Uniform3(shaderProgram.UCameraPosition, _camera.Position.X, _camera.Position.Y, _camera.Position.Z);

            _figure.Draw();
        }
    }
}
