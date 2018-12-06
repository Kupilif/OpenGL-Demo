using System.Numerics;

namespace OpenGlDemo.Lighting
{
    public static class LightBuilder
    {
        public static Light CreateWhiteLight()
        {
            return new Light
            {
                Type = LightType.Ambient,

                Direction = new Vector3(-1f, -1f, -1f),

                Ambient = new Vector3(1f, 1f, 1f),
                Diffuse = new Vector3(1f, 1f, 1f),
                Specular = new Vector3(1f, 1f, 1f),

                ConstantCoefficient = 1f,
                LinearCoefficient = 0.09f,
                QuadraticCoefficient = 0.032f,

                InnerCutOff = 50f,
                OuterCutOff = 70f
            };
        }
    }
}
