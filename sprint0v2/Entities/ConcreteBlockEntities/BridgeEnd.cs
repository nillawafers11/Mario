using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities.ConcreteBlockEntities
{
    public class BridgeEnd : Block
    {
        public BridgeEnd(Vector2 position, Vector2 speed, ItemType itemType, int itemCount, bool ishidden)
            : base(position, speed, itemType, itemCount, ishidden)
        {
            Sprite = BlockSpriteFactory.Instance.CreateSprite(position, (int)BlockSpriteFactory.eBlockSprite.BridgeEnd);
            AABB = new AABB(position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            boundingBox = AABB.GetBoundingBox();
        }

        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
        }
    }
}
