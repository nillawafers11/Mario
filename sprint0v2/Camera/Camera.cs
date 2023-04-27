using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2;

namespace sprint0v2.View
{
    public class Camera
    {
        public static Camera Instance { get; internal set; }

        public Camera(Viewport viewport)
        {
            Instance = this;
            _viewport = viewport;
            Origin = new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
            Zoom = 1.0f;
        }
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position.X = value.X;

                // Set a fixed value for the Y position
                _position.Y = Limits.HasValue ? Limits.Value.Y : 0;

                // If there's a limit set and there's no zoom or rotation clamp the X position
                if (Limits != null && Zoom == 1.0f && Rotation == 0.0f)
                {
                    _position.X = MathHelper.Clamp(_position.X, Limits.Value.X, Limits.Value.X + Limits.Value.Width - _viewport.Width);
                }
            }
        }


        public Vector2 Origin { get; set; }

        public float Zoom { get; set; }

        public float Rotation { get; set; }


        public Rectangle? Limits
        {
            get
            {
                return _limits;
            }
            set
            {
                if (value != null)
                {
                    // Assign limit but make sure it's always bigger than the viewport
                    _limits = new Rectangle
                    {
                        X = value.Value.X,
                        Y = value.Value.Y,
                        Width = System.Math.Max(_viewport.Width, value.Value.Width),
                        Height = System.Math.Max(_viewport.Height, value.Value.Height)
                    };

                    // Validate camera position with new limit
                    Position = Position;
                }
                else
                {
                    _limits = null;
                }
            }
        }

        public Matrix GetViewMatrix(Vector2 parallax)
        {
            return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
                   Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(Zoom, Zoom, 1.0f) *
                   Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
        }

        public void LookAt(Vector2 position)
        {
            Position = position - new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
        }

        public void Move(Vector2 displacement, bool respectRotation = false)
        {
            if (respectRotation)
            {
                displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(-Rotation));
            }

            Position += displacement;
        }

        private readonly Viewport _viewport;
        private Vector2 _position;
        private Rectangle? _limits;
    }
}
