using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using sprint0v2.View;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteBlockEntities
{
    public class PipeBlock : Block
    {
        private string pipeType;
        public string PipeType
        {
            get => pipeType;
            set => pipeType = value;
        }
        public PipeBlock(Vector2 position, Vector2 speed, string subType, bool ishidden)
            : base(position, speed, ItemType.Nothing, 0, ishidden)
        {
            if (subType == "tallpipe" || subType == "piranhaplant")
            {
                pipeType = subType;
                Sprite = BlockSpriteFactory.Instance.CreateSprite(position, (int)BlockSpriteFactory.eBlockSprite.TallPipeBlock);
                AABB = new AABB(position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
                boundingBox = AABB.GetBoundingBox();
            }
            else if (subType == "warppipe")
            {
                pipeType = subType;
                Sprite = BlockSpriteFactory.Instance.CreateSprite(position, (int)BlockSpriteFactory.eBlockSprite.TallPipeBlock);
                AABB = new AABB(position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
                boundingBox = AABB.GetBoundingBox();
            }
            else if (subType == "sidepipe")
            {
                pipeType = subType;
                Sprite = BlockSpriteFactory.Instance.CreateSprite(position, (int)BlockSpriteFactory.eBlockSprite.SidePipeBlock);
                AABB = new AABB(position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
                boundingBox = AABB.GetBoundingBox();
            }

        }
        public PipeBlock(Vector2 position, Vector2 speed, bool castlePipe) : base(position, speed, ItemType.Nothing, 0, false) {
            pipeType = "castlepipe";
            Sprite = BlockSpriteFactory.Instance.CreateSprite(position, (int)BlockSpriteFactory.eBlockSprite.CastlePipeBlock);
            AABB = new AABB(position, new Vector2(Sprite.Texture.Width / Sprite.Columns, Sprite.Texture.Height / Sprite.Rows));
            boundingBox = AABB.GetBoundingBox();
        }
        public override void OnCollision(Entity other)
        {
            base.OnCollision(other);
            if (other is Mario player)
            {
                if (player.IsCollidingTop(boundingBox) && player.IsCrouching() && pipeType == "warppipe")
                {
                    WarpToSecretLevel(player);

                } else if (player.IsCollidingLeft(boundingBox) & pipeType == "sidepipe")
                {
                    WarpToLevel(player);
                }
            }
        }

        private void WarpToSecretLevel(Mario mario)
        {
            mario.Position = new Vector2(7032, 0);

            Rectangle screenBounds = new Rectangle(7000, 0, 800, 900); // Change the values to match your game's screen size and location
            Camera.Instance.Limits = screenBounds;

        }
        private void WarpToLevel(Mario mario)
        {
            mario.Position = new Vector2(2320, 140);
            Camera.Instance.LookAt(mario.Position);
            Camera.Instance.Limits = null;
        }
    }
}
