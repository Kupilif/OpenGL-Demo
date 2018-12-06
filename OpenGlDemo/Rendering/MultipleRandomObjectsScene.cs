using System;
using System.Collections.Generic;
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
    public class MultipleRandomObjectsScene : Scene
    {
        private Model _figure;
        private List<(Vector3 position, MaterialType material)> _figureParams;

        public MultipleRandomObjectsScene(Args args) : base(args.CameraPosition)
        {
            InitializeFigures(args);
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

            var lightDirection = light.Type == LightType.Directional ? light.Direction : _camera.Front;
            shaderProgram.SetLight(light, _camera.Position, lightDirection);

            Gl.Uniform3(shaderProgram.UCameraPosition, _camera.Position.X, _camera.Position.Y, _camera.Position.Z);

            Matrix4x4 matrix = _camera.GetViewMatrix();
            Gl.UniformMatrix4f(shaderProgram.UView, 1, false, ref matrix);
            matrix = Matrix4x4.CreatePerspectiveFieldOfView(_camera.Zoom.ToRadians(), (float)width / (float)height, 0.1f, 100f);
            Gl.UniformMatrix4f(shaderProgram.UProjection, 1, false, ref matrix);

            foreach (var figureParam in _figureParams)
            {
                shaderProgram.SetMaterial(MaterialBuilder.Create(figureParam.material));

                matrix = _figure.Matrix * Matrix4x4.CreateTranslation(figureParam.position);
                Gl.UniformMatrix4f(shaderProgram.UModel, 1, false, ref matrix);

                _figure.Draw();
            }
        }

        private void InitializeFigures(Args args)
        {
            _figureParams = new List<(Vector3 position, MaterialType material)>();
            var random = new Random();
            Array materials = Enum.GetValues(typeof(MaterialType));

            for (int i = 0; i < args.ObjectsCount; i++)
            {
                var position = new Vector3(
                    random.Next((int)args.MinObjectPostion.X, (int)args.MaxObjectPosition.X),
                    random.Next((int)args.MinObjectPostion.Y, (int)args.MaxObjectPosition.Y),
                    random.Next((int)args.MinObjectPostion.Z, (int)args.MaxObjectPosition.Z)
                );
                var material = (MaterialType)materials.GetValue(random.Next(materials.Length));

                _figureParams.Add((position, material));
            }
        }

        public class Args
        {
            public Vector3 CameraPosition;
            public Vector3 MinObjectPostion;
            public Vector3 MaxObjectPosition;
            public int ObjectsCount;
        }
    }
}
