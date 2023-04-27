using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteBlockEntities.BlockStates;
using sprint0v2.Sprites;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteBlockEntities
{
    public class Platform : Block
    {
        protected readonly Vector2 oringalPosition;

        public override BlockState BlockState
        {
            set
            {
                _currentState = value;

            }
            get { return _currentState; }
        }

        public Platform(Vector2 position, Vector2 speed, ItemType itemHeld, int itemCount, bool isHidden, string type)
            : base(position, speed, itemHeld, itemCount, isHidden)
        {
            Sprite = BlockSpriteFactory.Instance.CreateSprite(Position, (int)BlockSpriteFactory.eBlockSprite.Platform);
            AABB = new AABB(Position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            boundingBox = AABB.GetBoundingBox();
            oringalPosition = position;
            BlockState = new PlatformState(this);
            if (type == "verticalPlatform")
            {
                _currentState.PlatformVerticalState(false);
            }
            else
            {
                _currentState.PlatformHorizontalState(true);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime);
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.SpritePosition = Position;
            Sprite.Update(gameTime);
            AABB = new AABB(Position, new Vector2(Sprite.Texture.Width, Sprite.Texture.Height));
            boundingBox = AABB.GetBoundingBox();
        }
    }
}
