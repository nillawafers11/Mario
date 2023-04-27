using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.Sprites;
using System.Collections.Generic;
using sprint0v2.CollisionDetection;

namespace sprint0v2.Entities.ConcreteItemEntites
{
    
    public class Vine : Item
    {
        List<Entity> VineSegments;
        public Vine(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            VineSegments = new List<Entity>();
        }
        public override void Update(GameTime gameTime)
        {
            if (VineSegments.Count == 0)
            {
                Entity top = new VineTop(Position, Speed);
                VineSegments.Add(top);
                Grid.Instance.AddEntity(top);
            }
            else if (Position.Y - VineSegments[VineSegments.Count - 1].Position.Y >= 7) {
                Vector2 segmentPosition = Position;
                Entity segment = new VineSegment(segmentPosition, Speed);
                VineSegments.Add(segment);
                Grid.Instance.AddEntity(segment);
            }
            for (int i = 0; i < VineSegments.Count; i++) {
                VineSegments[i].Update(gameTime);
                if (VineSegments[i].Position.Y < -16)
                {
                    VineSegments.RemoveAt(i);
                    Grid.Instance.RemoveEntity(VineSegments[i]);
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < VineSegments.Count; i++) { 
                VineSegments[i].Draw(spriteBatch);
            }
        }

        public override void OnCollision(Entity other)
        {

        }
    }

    public class VineTop : Vine {
        private Vector2 bBoxOffset;
        public VineTop(Vector2 position, Vector2 speed) : base(position, speed) {
            bBoxOffset = new Vector2(6, 4);
            Sprite = ItemSpriteFactory.Instance.CreateSprite(position, (int)ItemSpriteFactory.eItemSprite.VineTop);
            AABB = new AABB(position + bBoxOffset, new Vector2(2, 10));
            BoundingBox = AABB.GetBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.SpritePosition = Position;
            AABB.Position = Position + bBoxOffset;
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

    public class VineSegment : Vine {
        private Vector2 bBoxOffset;
        public VineSegment(Vector2 position, Vector2 speed) : base(position, speed)
        {
            bBoxOffset = new Vector2(6, 0);
            Sprite = ItemSpriteFactory.Instance.CreateSprite(position, (int)ItemSpriteFactory.eItemSprite.VineStem);
            AABB = new AABB(position + bBoxOffset, new Vector2(2, 14));
            BoundingBox = AABB.GetBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.SpritePosition = Position;
            AABB.Position = Position + bBoxOffset;
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
