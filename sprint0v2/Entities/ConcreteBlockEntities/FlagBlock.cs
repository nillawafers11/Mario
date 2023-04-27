using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;

namespace sprint0v2.Entities.ConcreteBlockEntities
{
    public class FlagBlock : Block
    {

        public FlagBlock(Vector2 position, Vector2 speed, ItemType itemType, int itemCount, bool ishidden)
            : base(position, speed, itemType, itemCount, ishidden)
        {
            Sprite = BlockSpriteFactory.Instance.CreateSprite(Position, (int)BlockSpriteFactory.eBlockSprite.FlagBlock);
            Vector2 boxSize = new Vector2((Sprite.Texture.Width / Sprite.Columns) / 3, Sprite.Texture.Height / Sprite.Rows);
            Vector2 bBoxOffset = new Vector2(12, 0);
            AABB = new AABB(position + bBoxOffset, boxSize);
            boundingBox = AABB.GetBoundingBox();

        }
        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
        }

        public override void Update(GameTime gameTime)
        {
            //do nothing
        }

        /*0-17 pixels high: 100 extra points
        18-57 pixels high: 400 extra points
        58-81 pixels high: 800 extra points
        82-127 pixels high: 2000 extra points
        128-153 pixels high: 4000 extra points*/

        public int GetPoints(Mario mario)
        {
            int points = 0;
            if(mario.Position.Y <= 190 && mario.Position.Y >= 173)
            {
                points = 100;
            }
            else if (mario.Position.Y <= 172 && mario.Position.Y >= 133)
            {
                points = 400;
            }
            else if (mario.Position.Y <= 132 && mario.Position.Y >= 109)
            {
                points = 800;
            }
            else if (mario.Position.Y <= 108 && mario.Position.Y >= 63)
            {
                points = 2000;
            }
            else if (mario.Position.Y <= 62 && mario.Position.Y >= 37)
            {
                points = 4000;
            }
            else if (mario.Position.Y >= Position.Y)
            {
                mario.Lives++;
            }
            return points;
        }
    }
}
