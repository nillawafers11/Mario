using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.CollisionDetection;
using sprint0v2.Entities.ConcreteItemEntites.ItemStates;
using System.Diagnostics;

namespace sprint0v2.Entities
{
    public enum ItemType
    {
        Coin,
        FireFlower,
        OneUpMushroom,
        SuperMushroom,
        Star,
        Axe,
        Vine,
        Nothing,
    }
    
    public abstract class Item : Entity
    {
        public ItemType ItemType { get; set; }
        public virtual ItemState ItemState { get; set; }
        protected bool flipSpeedX;
        protected Vector2 BBoxOffset;

        public Item(Vector2 position, Vector2 speed)
            : base(position, speed)
        {
            Position = position;
            Speed = speed;
            float scale = 1.25f;
            BBoxOffset = new Vector2(-2, -4);
            Vector2 BBoxPosition = Position + BBoxOffset;
            AABB = new AABB(BBoxPosition, new Vector2(16*scale, 16*scale));
            boundingBox = AABB.GetBoundingBox();
            BoundingBoxColor = Color.GreenYellow;
        }

        public override void Update(GameTime gameTime)
        {
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.SpritePosition += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Sprite.Update(gameTime);
            AABB.Position = Position + BBoxOffset;
            boundingBox = AABB.GetBoundingBox();
            if (IsRemoved)
            {
                EntityManager.Instance.RemoveEntity(this);
            }
        }
    }
}