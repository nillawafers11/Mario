using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteBlockEntities.BlockStates;
using sprint0v2.Sprites;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteBlockEntities
{
    public class UsedBlock : Block
    {
        public UsedBlock(Vector2 position) : base(position, Vector2.Zero, ItemType.Nothing, 0, false)
        {
            Sprite = BlockSpriteFactory.Instance.CreateSprite(position, (int)BlockSpriteFactory.eBlockSprite.UsedBlock);
            AABB = new AABB(Position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            boundingBox = AABB.GetBoundingBox();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void OnCollision(Entity other)
        {

        }
    }
}
