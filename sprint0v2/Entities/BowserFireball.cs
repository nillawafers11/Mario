using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using sprint0v2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities
{
    public class BowserFireball : Entity
    {
        private new Vector2 acceleration = new Vector2(0, 512);
        public BowserFireball(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            Position = position;
            Speed = speed;
            Sprite = FireballSpriteFactory.Instance.CreateSprite(Position, (int)FireballSpriteFactory.eFireballSprite.Bowser);
            AABB = new AABB(position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            BoundingBox = AABB.GetBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.SpritePosition = Position;
            AABB = new AABB(Position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            BoundingBox = AABB.GetBoundingBox();
        }
    }
}
