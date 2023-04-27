using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

using System.Text;
using System.Threading.Tasks;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0v2.Entities.ConcreteEnemyEntites
{
    public class Spiny : Enemy
    {
        public override EnemyState EnemyState
        {
            get { return _currentState; }
            set
            {
                if(value is SpinyShellState)
                {
                    Sprite.IsAnimated = true;
                    Sprite.CurrentFrame = 0;
                }
                else if(value is SpinyNormalState)
                {
                    Sprite.IsAnimated = false;
                    Sprite.CurrentFrame = 2;
                }
                else if(value is SpinyFlopState)
                {
                    Grid.Instance.RemoveEntity(this);
                    Sprite.IsAnimated = false;
                    Sprite.CurrentFrame = 2;
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
        public Spiny(Vector2 position, Vector2 speed) : base(position, speed)
        {
            Sprite = EnemySpriteFactory.Instance.CreateSprite(Position, (int)EnemySpriteFactory.eEnemySprite.Spiny);
            _currentState = new SpinyShellState(this);
            scale = 0.875f; // adjust the scale factor as desired
            Vector2 boxSize = new Vector2(Sprite.Texture.Width / Sprite.Columns * scale - 1, Sprite.Texture.Height / Sprite.Rows * scale);
            boxOffset = new Vector2(1, 2);
            AABB = new AABB(position + boxOffset, boxSize);
            boundingBox = AABB.GetBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void OnCollision(Entity other)
        {
            switch (other)
            {
                case Block:
                    //If Spiny his the top of the block He should become grounded and his current frame should be set to 2
                    //Then he walks along the block
                    if(other.Position.Y >= Position.Y + 10)
                    {
                        EnemyState.Landing();
                    }
                    else if ((LeftCollision < 2 || RightCollision < 2) && other.Position.Y - 16 < Position.Y)
                    {
                        flipSpeedX = true;
                    }
                    break;
                case Fireball:
                    if(other.Position.X > Position.X)
                    {
                        flopToLeft = true;
                    }
                    EnemyState.Die();
                    break;
                case Enemy:
                    Enemy collidedEntity = (Enemy)other;
                    if (collidedEntity.EnemyState is KoopaZoomState)
                    {
                        if (other.Position.X > Position.X)
                        {
                            flopToLeft = true;
                        }
                        EnemyState.Die();
                    }
                    else
                    {
                        flipSpeedX = true;
                    }
                    break;
            }
        }
    }
}
