using System.Numerics;

namespace OpenGlDemo.Materials
{
    public static class MaterialBuilder
    {
        public static Material Create(MaterialType type)
        {
            switch (type)
            {
                case MaterialType.Gold:
                    return new Material
                    {
                        Ambient = new Vector3(0.24725f, 0.1995f, 0.0745f),
                        Diffuse = new Vector3(0.75164f, 0.60648f, 0.22648f),
                        Specular = new Vector3(0.628281f, 0.555802f, 0.366065f),
                        Shininess = 128f
                    };
                case MaterialType.Bronze:
                    return new Material
                    {
                        Ambient = new Vector3(0.2f, 0.1f, 0.062f),
                        Diffuse = new Vector3(0.5f, 0.25f, 0.155f),
                        Specular = new Vector3(0.5f, 0.5f, 0.5f),
                        Shininess = 32f
                    };
                default:
                    return new Material
                    {
                        Ambient = new Vector3(0.2f, 0.2f, 0.2f),
                        Diffuse = new Vector3(1f, 1f, 1f),
                        Specular = new Vector3(1f, 1f, 1f),
                        Shininess = 128f
                    };
            }
        }
    }
}
