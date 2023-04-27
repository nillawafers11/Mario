using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteBlockEntities.BlockStates;
using sprint0v2.Sprites;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteBlockEntities
{
    public class QuestionBlock : Block
    {
        private bool _hidden;

        public new bool IsHidden { 
            set { _hidden = value; }
            get { return _hidden; }
        }

        public override int ItemCount
        {
            set { _currentItemCount = value; }
        }
        public override BlockState BlockState
        {
            set
            {
                _currentState = value;
                if (value is QuestionBlockBumpState) {
                    //Debug.WriteLine("entered QuestionBlockBumpState");
                    IsHidden = false;
                    _currentItemCount--;
                }else if (value is UsedBlockState) {
                    //Debug.WriteLine("entered QBlockUsed state");
                    Sprite.IsAnimated = false;
                    Sprite.CurrentFrame = 3;
                }
                
            }
            get { return _currentState; }
        }

        public QuestionBlock(Vector2 position, Vector2 speed, ItemType itemHeld, int itemCount, bool isHidden) 
            : base(position, speed, itemHeld, itemCount, isHidden)
        {
            
            if (isHidden) {
                Sprite = BlockSpriteFactory.Instance.CreateSprite(position, (int)BlockSpriteFactory.eBlockSprite.HiddenBlock);
                _currentState = new HiddenQuestionBlockState(this);
            }
            else {
                Sprite = BlockSpriteFactory.Instance.CreateSprite(position, (int)BlockSpriteFactory.eBlockSprite.QuestionBlock);
                _currentState = new QuestionBlockState(this);
            }
            itemType = itemHeld;
            ItemCount = itemCount;
            IsHidden = isHidden;
            AABB = new AABB(Position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            boundingBox = AABB.GetBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {
            _currentState.Update(gameTime);
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.SpritePosition = Position;
            Sprite.Update(gameTime);
            AABB = new AABB(Position, new Vector2(16, 16));
            boundingBox = AABB.GetBoundingBox();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsHidden)
            {
                base.Draw(spriteBatch);
            }
            if (BBoxVisible)
            {
                DrawBoundingBox(spriteBatch);
            }
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            if (other is Mario)
            {
                if (other.Position.Y >= Position.Y + 16)
                {
                    isHidden = false;
                    _currentState.QuestionBump(other);
                }
            }
        }
    }
}
