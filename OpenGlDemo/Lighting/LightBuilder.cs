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

                Ambient = new Vector3(1f, 1f, 1f),
                Diffuse = new Vector3(1f, 1f, 1f),
                Specular = new Vector3(1f, 1f, 1f),

                ConstantCoefficient = 1f,
                LinearCoefficient = 0.09f,
                QuadraticCoefficient = 0.032f,

                InnerCutOff = 12.5f,
                OuterCutOff = 17.5f
            };
        }
    }
}
