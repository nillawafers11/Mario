using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using sprint0v2.Sprites;
using sprint0v2.Entities.MarioStates.PowerUpStates;
using System;

namespace sprint0v2.Entities.ConcreteEnemyEntites
{
    public class PiranhaPlant : Enemy
    {
        public override EnemyState EnemyState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
            }
        }

        public PiranhaPlant(Vector2 position, Vector2 speed) : base(position, speed) {
            Sprite = EnemySpriteFactory.Instance.CreateSprite(position, (int)EnemySpriteFactory.eEnemySprite.piranha);
            scale = 0.875f;
            boxSize = new Vector2(Sprite.Texture.Width / Sprite.Columns * scale - 1, Sprite.Texture.Height / Sprite.Rows * scale);
            boxOffset = new Vector2(1, 3);
            AABB = new AABB(position + boxOffset, boxSize);
            boundingBox = AABB.GetBoundingBox();
            EnemyState = new PiranhaDownState(this, position);
        }

        public override void Update(GameTime gameTime)
        {
            if (Math.Abs(EntityManager.Instance.GetPlayer().Position.X - Position.X) > 128) {
                InViewport = false;
                _currentState.MoveDown();
            }
            if (InViewport) {
                if (Math.Abs(EntityManager.Instance.GetPlayer().Position.X - Position.X) > 25)
                {
                    _currentState.MoveUp();
                }
            }
            base.Update(gameTime);
        }

        public override void OnCollision(Entity other)
        {
            if (other is Fireball) {
                _currentState.Die();
            }
        }
    }
}
