using sprint0v2.Sprites;
using System;
using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;

namespace sprint0v2.Entities.ConcreteEnemyEntites
{
    public class BulletBill : Enemy
    {
        public override EnemyState EnemyState
        {
            set
            {
                if (value is BulletFlopState)
                {
                    Grid.Instance.RemoveEntity(this);
                    Sprite.SpriteFlip = SpriteEffects.FlipVertically;

                    float xVelocity = 32;
                    if (flopToLeft)
                    {
                        xVelocity = -xVelocity;
                    }
                    float yVelocity = -160;
                    Speed = new Vector2(xVelocity, yVelocity);
                }
                _currentState = value;
            }
        }
        public BulletBill(Vector2 position, Vector2 speed) : base(position, speed)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSprite(Position, (int)EnemySpriteFactory.eEnemySprite.BulletBill);
            EnemyState = new BulletNormalState(this);
            Sprite.SpriteFlip = SpriteEffects.FlipHorizontally;
            scale = 0.875f; // adjust the scale factor as desired
            Vector2 boxSize = new Vector2(Sprite.Texture.Width / Sprite.Columns * scale - 1, Sprite.Texture.Height / Sprite.Rows * scale);
            boxOffset = new Vector2(1, 2);
            AABB = new AABB(position + boxOffset, boxSize);
            boundingBox = AABB.GetBoundingBox();
            DrawOrder = 1;
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            switch (other)
            {
                case Mario:
                    double xDifference = Math.Abs(other.Position.X - Position.X);
                    if (xDifference < 16 && other.Position.Y + 14 <= Position.Y)
                    {
                        EnemyState.Die();
                    }
                    break;
                case Fireball:
                    if (other.Position.X > Position.X)
                    {
                        flopToLeft = true;
                    }
                    EnemyState.Die();
                    break;
            }
        }
    }
}

