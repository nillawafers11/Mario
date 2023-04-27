using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteEnemyEntites;
using sprint0v2.Sprites;
using System;

namespace sprint0v2.Entities.ConcreteBlockEntities
{
    public class BulletBillLauncher : Block
    {
        private double fireInterval = 5000; // 5 seconds
        private double fireTimer = 0;

        public BulletBillLauncher(Vector2 position)
            : base(position, Vector2.Zero, ItemType.Nothing, 0, true)
        {
            Sprite = BlockSpriteFactory.Instance.CreateSprite(position, (int)BlockSpriteFactory.eBlockSprite.BulletBillLauncher);
            DrawOrder = 5;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Update the timer
            fireTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check if Mario has crossed the center Y coordinate and if the timer has passed the fire interval
            if (EntityManager.Instance.GetPlayer().Position.Y <= Position.Y + Sprite.Texture.Height / 2 && fireTimer >= fireInterval)
            {
                FireBulletBill();
                fireTimer = 0;
            }
        }

        private void FireBulletBill()
        {
            // Create a new Bullet Bill entity with the appropriate position and speed
            Vector2 bulletBillPosition = new Vector2(
        Position.X + Sprite.Texture.Width / 2 - Sprite.Texture.Width / 2,
        Position.Y + Sprite.Texture.Height / 2 - Sprite.Texture.Height / 2
    );
            Entity bullet = EntityManager.Instance.AddEnemyEntity(bulletBillPosition, "bulletbill");
            Grid.Instance.AddEntity(bullet);

        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
        }
    }
}
