namespace OpenGlDemo.Rendering
{
    public class Model
    {
        public bool UseIndices { get; set; }
        public int VertexesCount { get; set; }
        public float[] Vertexes { get; set; }
        public uint[] Indices { get; set; }
    }
}
