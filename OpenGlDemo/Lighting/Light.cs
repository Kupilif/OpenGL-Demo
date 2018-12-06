using System.Numerics;

namespace OpenGlDemo.Lighting
{
    public class Light
    {
        public LightType Type { get; set; }

        public Vector3 Direction { get; set; }

        public Vector3 Ambient { get; set; }
        public Vector3 Diffuse { get; set; }
        public Vector3 Specular { get; set; }

        public float ConstantCoefficient { get; set; }
        public float LinearCoefficient { get; set; }
        public float QuadraticCoefficient { get; set; }

        public float InnerCutOff { get; set; }
        public float OuterCutOff { get; set; }
    }
}
