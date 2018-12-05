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
                        Shininess = 64f
                    };
                case MaterialType.Silver:
                    return new Material
                    {
                        Ambient = new Vector3(0.19225f, 0.19225f, 0.19225f),
                        Diffuse = new Vector3(0.50754f, 0.50754f, 0.50754f),
                        Specular = new Vector3(0.508273f, 0.508273f, 0.508273f),
                        Shininess = 64f
                    };
                case MaterialType.Bronze:
                    return new Material
                    {
                        Ambient = new Vector3(0.2f, 0.1f, 0.062f),
                        Diffuse = new Vector3(0.5f, 0.25f, 0.155f),
                        Specular = new Vector3(0.5f, 0.5f, 0.5f),
                        Shininess = 32f
                    };
                case MaterialType.GreenRubber:
                    return new Material
                    {
                        Ambient = new Vector3(0f, 0.05f, 0f),
                        Diffuse = new Vector3(0.4f, 0.5f, 0.4f),
                        Specular = new Vector3(0.04f, 0.7f, 0.04f),
                        Shininess = 13f
                    };
                case MaterialType.RedPlastic:
                    return new Material
                    {
                        Ambient = new Vector3(0f, 0f, 0f),
                        Diffuse = new Vector3(0.5f, 0f, 0f),
                        Specular = new Vector3(0.7f, 0.6f, 0.6f),
                        Shininess = 40f
                    };
                case MaterialType.Obsidian:
                    return new Material
                    {
                        Ambient = new Vector3(0.05375f, 0.05f, 0.06625f),
                        Diffuse = new Vector3(0.18275f, 0.17f, 0.22525f),
                        Specular = new Vector3(0.332741f, 0.328634f, 0.346435f),
                        Shininess = 48f
                    };
                case MaterialType.Emerald:
                    return new Material
                    {
                        Ambient = new Vector3(0.0215f, 0.1745f, 0.0215f),
                        Diffuse = new Vector3(0.07568f, 0.61424f, 0.07568f),
                        Specular = new Vector3(0.633f, 0.727811f, 0.633f),
                        Shininess = 96f
                    };
                case MaterialType.Ruby:
                    return new Material
                    {
                        Ambient = new Vector3(0.1745f, 0.01175f, 0.01175f),
                        Diffuse = new Vector3(0.61424f, 0.041364f, 0.04136f),
                        Specular = new Vector3(0.727811f, 0.626959f, 0.626959f),
                        Shininess = 96f
                    };
                case MaterialType.Brass:
                    return new Material
                    {
                        Ambient = new Vector3(0.329412f, 0.223529f, 0.027451f),
                        Diffuse = new Vector3(0.780392f, 0.568627f, 0.113725f),
                        Specular = new Vector3(0.992157f, 0.941176f, 0.807843f),
                        Shininess = 35f
                    };
                case MaterialType.Chrome:
                    return new Material
                    {
                        Ambient = new Vector3(0.25f, 0.25f, 0.25f),
                        Diffuse = new Vector3(0.4f, 0.4f, 0.4f),
                        Specular = new Vector3(0.774597f, 0.774597f, 0.774597f),
                        Shininess = 96f
                    };
                case MaterialType.Copper:
                    return new Material
                    {
                        Ambient = new Vector3(0.19125f, 0.0735f, 0.0225f),
                        Diffuse = new Vector3(0.7038f, 0.27048f, 0.0828f),
                        Specular = new Vector3(0.256777f, 0.137622f, 0.086014f),
                        Shininess = 16f
                    };
                case MaterialType.Pearl:
                    return new Material
                    {
                        Ambient = new Vector3(0.25f, 0.20725f, 0.20725f),
                        Diffuse = new Vector3(1f, 0.829f, 0.829f),
                        Specular = new Vector3(0.296648f, 0.296648f, 0.296648f),
                        Shininess = 14f
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
