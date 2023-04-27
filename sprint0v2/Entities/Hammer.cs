using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities;
using sprint0v2.Entities.ConcreteBlockEntities;
using sprint0v2.Sprites;
using System;
using System.Threading.Tasks;

namespace sprint0v2
{
    public class Hammer : Entity
    {
        private new Vector2 acceleration = new Vector2(0, 512);
        private float rotation;
        private float angle;
        public Hammer(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            Position = position;
            Speed = speed;
            Sprite = ItemSpriteFactory.Instance.CreateSprite(Position, (int)ItemSpriteFactory.eItemSprite.Hammer);
            AABB = new AABB(position, new Vector2(Sprite.Texture.Width, Sprite.Texture.Height));
            BoundingBox = AABB.GetBoundingBox();
            angle = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.SpritePosition = Position;
            AABB = new AABB(Position, new Vector2(Sprite.Texture.Width, Sprite.Texture.Height));
            BoundingBox = AABB.GetBoundingBox();
            RotateHammer();
        }

        private void RotateHammer()
        {
            angle += 20;
            rotation = MathHelper.ToRadians(angle);
        }

        public override void OnCollision(Entity other)
        {
            if(other is GroundBlock)
            {
                EntityManager.Instance.RemoveEntity(this);
                Grid.Instance.RemoveEntity(this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle frameRectangle = new Rectangle(0, 0, Sprite.Texture.Width, Sprite.Texture.Height);
            spriteBatch.Draw(Sprite.Texture, Position, frameRectangle, Color.White, rotation, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}