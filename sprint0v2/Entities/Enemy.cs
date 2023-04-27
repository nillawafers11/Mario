using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using sprint0v2.Entities.ConcreteEnemyEntites.EnemyStates;
using sprint0v2.CollisionDetection;
using sprint0v2.View;
using sprint0v2.Entities.ConcreteEnemyEntites;

namespace sprint0v2.Entities
{
    public abstract class Enemy : Entity
    {
        private bool _isInViewport;
        protected bool flopToLeft;
        protected bool flipSpeedX;
        public bool InViewport
        {
            get { return _isInViewport; }
            set { _isInViewport = value; }
        }
        public bool DirectionXToggle { 
            get { return flipSpeedX; }
            set { flipSpeedX = value; }
        }
        public float scale { get; set; }
        public EnemyState _currentState;
        public virtual EnemyState EnemyState
        {
            set { _currentState = value; }
            get { return _currentState; }
        }
        public Vector2 boxSize { get; set; }
        public virtual Vector2 boxOffset { get; set; }
        public Enemy(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            _isInViewport = false;
            Position = new Vector2(position.X, Position.Y);
            Speed = speed;
            BoundingBoxColor = Color.Red;
        }

        public override void Update(GameTime gameTime)
        {
            
            if (Camera.Instance.Position.X < Position.X && Camera.Instance.Position.X + 256 > Position.X && !_isInViewport) { 
                _isInViewport = true;
                if (EntityManager.Instance.GetPlayer().Position.X > Position.X) { 
                    flipSpeedX = true;
                }
            }
            if (!IsColliding) {
                if (_currentState is not null) _currentState.Falling();
            }
            if (flipSpeedX)
            {
                SpeedX = -Speed.X;
                if(this is not Bowser)
                {
                    ToggleSpriteFlip();
                }
                flipSpeedX = false;
            }
            if (_isInViewport)
            {
                if( _currentState is not null) _currentState.update(gameTime);
                Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Sprite.SpritePosition = Position;
                Sprite.Update(gameTime);
                AABB.Position = Position + boxOffset;
                boundingBox = AABB.GetBoundingBox();
                if (IsRemoved)
                {
                    EntityManager.Instance.RemoveEntity(this);
                }
            }
        }

        protected void ToggleSpriteFlip() {
            if (Sprite.SpriteFlip is SpriteEffects.None)
            {
                Sprite.SpriteFlip = SpriteEffects.FlipHorizontally;
            }
            else { 
                Sprite.SpriteFlip = SpriteEffects.None;
            }
        }
    }
}