using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteBlockEntities.BlockStates;
using sprint0v2.Sprites;
using System.Diagnostics;
using System.Collections;

namespace sprint0v2.Entities.ConcreteEnemyEntites
{
    public class FireBar : Enemy
    {
        private List<Entity> fireballs;
        public List<Entity> Fireballs { 
            get { return fireballs; }
            set { }
        }
        public FireBar(Vector2 position, int fireBallCount) : base(position, Vector2.Zero) {
            fireballs = new List<Entity>();
            for (int i = 0; i < fireBallCount; i++) { 
                fireballs.Add(new FireBarBall(position, i));
            }
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].Draw(spriteBatch);
            }
            if (BBoxVisible)
            {
                DrawBoundingBox(spriteBatch);
            }
        }

        public override void DrawBoundingBox(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < fireballs.Count; i++)
            {
                fireballs[i].DrawBoundingBox(spriteBatch);
            }
        }

        public override void OnCollision(Entity other)
        {

        }
    }

    public class FireBarBall : FireBar
    {
        private Vector2 center;
        private int orderFromCenter;
        public FireBarBall(Vector2 position, int fireBallOrder) : base(position, 0) {
            center = position;
            orderFromCenter = fireBallOrder;
            center.X += 8 * orderFromCenter;
            Position = center;
            center.X -= 8 * orderFromCenter;
            Sprite = FireballSpriteFactory.Instance.CreateSprite(Position, (int)FireballSpriteFactory.eFireballSprite.Normal);
            AABB = new AABB(Position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            boundingBox = AABB.GetBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {
            float x = 8 * orderFromCenter * (float)Math.Cos(2 * gameTime.TotalGameTime.TotalSeconds);
            float y = 8 * orderFromCenter * (float)Math.Sin(2 * gameTime.TotalGameTime.TotalSeconds);
            Vector2 offset = new Vector2(x, y);
            Position = center + offset;
            Sprite.SpritePosition = Position;
            AABB.Position = Position;
            BoundingBox = AABB.GetBoundingBox();
            Sprite.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
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

        public override void DrawBoundingBox(SpriteBatch spriteBatch)
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
    }
}
