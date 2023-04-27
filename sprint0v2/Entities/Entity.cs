using sprint0v2.Sprites;
using sprint0v2.CollisionDetection;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace sprint0v2
{
    public abstract class Entity
    {
        public ISprite Sprite { get; set; }
        public Vector2 Position { get; set; }
        public bool IsRemoved { get; internal set; }
        

        #region Physics
        protected Vector2 speed;

        public Vector2 Speed 
        { 
            get => speed; 
            set => speed = value; 
        }

        public float SpeedY
        {
            get => speed.Y; 
            set => speed.Y = value;
        }

        public float SpeedX
        {
            get => speed.X;
            set => speed.X = value;
        }

        protected float acceleration = 10;
        public float Acceleration
        {
            get => acceleration;
            set => acceleration = value;
        }

        protected float gravity = 200; // in m/s^2
        public float Gravity
        {
            get => gravity; 
            set => gravity = value;        
        }

        public bool isGrounded;
        #endregion

        #region Collision
        public AABB AABB { get; set; }
        public bool IsColliding { get; internal set; }
        protected bool bBoxVisible;
        public bool BBoxVisible
        {
            get => bBoxVisible;
            set => bBoxVisible = value;
        }

        protected Color BoundingBoxColor;
        protected Texture2D BBoxPixel;
        protected Rectangle boundingBox;
        public Rectangle BoundingBox
        {
            get => boundingBox;
            set => boundingBox = value;
        }
        public Vector2 Center
        {
            get { return new Vector2(BoundingBox.Center.X, BoundingBox.Center.Y); }
        }

        public float LeftCollision;
        public float RightCollision;
        public float TopCollision;
        public float BottomCollision;

        public float OverLapX;
        public float OverLapY;
        public int DrawOrder { get; set; }

        #endregion

        public Entity(Vector2 position, Vector2 speed)
        {
            isGrounded = false;
            bBoxVisible= false;
            Position = position;
            this.speed = speed;
            AABB = new AABB(Position, new Vector2(16, 16));
            boundingBox = AABB.GetBoundingBox();
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Sprite != null)
            {
                Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Sprite.SpritePosition += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Sprite.Update(gameTime);
               
                
                AABB.Position = Sprite.SpritePosition;
                boundingBox = AABB.GetBoundingBox();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Sprite != null)
            {
                Sprite.Draw(spriteBatch);
                if (bBoxVisible)
                {
                    DrawBoundingBox(spriteBatch);
                }
            }
        }

        public virtual void DrawBoundingBox(SpriteBatch spriteBatch)
        {
            if (BBoxPixel == null)
            {
                BBoxPixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                BBoxPixel.SetData(new[] { BoundingBoxColor });
            }

            int borderWidth = 2; // Set the width of the border

            spriteBatch.Draw(BBoxPixel, new Rectangle(boundingBox.Left, boundingBox.Top, 1, boundingBox.Height), BoundingBoxColor);
            spriteBatch.Draw(BBoxPixel, new Rectangle(boundingBox.Right, boundingBox.Top, 1, boundingBox.Height), BoundingBoxColor);
            spriteBatch.Draw(BBoxPixel, new Rectangle(boundingBox.Left, boundingBox.Top, boundingBox.Width, 1), BoundingBoxColor);
            spriteBatch.Draw(BBoxPixel, new Rectangle(boundingBox.Left, boundingBox.Bottom, boundingBox.Width, 1), BoundingBoxColor);
        }

        public virtual void OnCollision(Entity other)
        {
            // Get the vertical and horizontal overlap of the AABBs
            LeftCollision = BoundingBox.Right - other.BoundingBox.Left;
            RightCollision = other.BoundingBox.Right - BoundingBox.Left;
            TopCollision = BoundingBox.Bottom - other.BoundingBox.Top;
            BottomCollision = other.BoundingBox.Bottom - BoundingBox.Top;

            OverLapX = Math.Min(LeftCollision, RightCollision);
            OverLapY = Math.Min(TopCollision, BottomCollision);
        }
    }
}