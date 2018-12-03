using System.Numerics;
using OpenGlDemo.Motion;

namespace OpenGlDemo.Rendering
{
    public class Camera
    {
        private Vector3 _position;
        private Vector3 _up;
        private Vector3 _front;

        public Camera(Vector3 position)
        {
            _position = position;
            _up = new Vector3(0f, 1f, 0f);
            _front = new Vector3(0f, 0f, -1f);
        }

        public Matrix4x4 GetViewMatrix()
        {
            return Matrix4x4.CreateLookAt(_position, _position + _front, _up);
        }

        public void Move(CameraMove diretion, float distance)
        {
            switch (diretion)
            {
                case CameraMove.Forward:
                    _position += _front * distance;
                    break;
                case CameraMove.Backward:
                    _position -= _front * distance;
                    break;
                case CameraMove.Right:
                    _position += Vector3.Normalize(Vector3.Cross(_front, _up)) * distance;
                    break;
                case CameraMove.Left:
                    _position -= Vector3.Normalize(Vector3.Cross(_front, _up)) * distance;
                    break;
            }
        }
    }
}
