using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace sprint0v2.CollisionDetection
{
    public class AABB
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public AABB(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public Rectangle GetBoundingBox()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        }

        public bool Intersects(AABB other)
        {
            Rectangle thisBox = GetBoundingBox();
            Rectangle otherBox = other.GetBoundingBox();

            return thisBox.Intersects(otherBox);
        }
    }
}

