using System;
using System.Numerics;
using OpenGlDemo.Extensions;
using OpenGlDemo.Motion;

namespace OpenGlDemo.Rendering
{
    public class Camera
    {
        public float Zoom { get; private set; }

        private Vector3 _position;
        private Vector3 _up;
        private Vector3 _front;

        private float _pitch;
        private float _yaw;

        public Camera(Vector3 position)
        {
            _position = position;
            _up = new Vector3(0f, 1f, 0f);
            _front = new Vector3(0f, 0f, -1f);
            _yaw = -90f;
            _pitch = 0f;
            Zoom = 45f;
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

        public void ChangeView(float xOffset, float yOffset, float sensitivity)
        {
            xOffset *= sensitivity;
            yOffset *= sensitivity;
            _yaw += xOffset;
            _pitch += yOffset;
            _pitch = _pitch > 89f ? 89f : _pitch;
            _pitch = _pitch < -89f ? -89f : _pitch;

            _front.X = (float) Math.Cos(_yaw.ToRadians()) * (float) Math.Cos(_pitch.ToRadians());
            _front.Y = (float)Math.Sin(_pitch.ToRadians());
            _front.Z = (float)Math.Sin(_yaw.ToRadians()) * (float)Math.Cos(_pitch.ToRadians());
            _front = Vector3.Normalize(_front);
        }

        public void ChangeZoom(float offset, float sensitivity)
        {
            Zoom -= offset * sensitivity;
            Zoom = Zoom < 1f ? 1f : Zoom;
            Zoom = Zoom > 45f ? 45f : Zoom;
        }
    }
}
