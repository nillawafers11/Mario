using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace sprint0v2.Entities.ConcreteItemEntites.ItemStates
{
    public abstract class ItemState
    {
        public Item itemEntity { get; set; }
        public virtual void Update(GameTime gameTime) { }
        public virtual void EnterFallingState() { }
        public virtual void EnterNormalState() { }
    }

    public class AppearFromBlockState : ItemState {

        private Vector2 destination;
        public Vector2 oldSpeed;
        public AppearFromBlockState(Item item)
        {
            itemEntity = item;
            oldSpeed = item.Speed;
            item.Speed = new Vector2(0, -10);
            destination = new Vector2(item.Position.X, item.Position.Y - 8);
        }
        public override void Update(GameTime gameTime) {
            if (itemEntity.Position.Y <= destination.Y) {
                EnterFallingState();
            }
        }
        public override void EnterFallingState()
        {
            //Debug.WriteLine("Entered item falling state");
            itemEntity.Speed = oldSpeed;
            Grid.Instance.AddEntity(itemEntity);
            itemEntity.ItemState = new FallingItemState(itemEntity);
        }
    }

    public class FallingItemState : ItemState {

        private Vector2 acceleration = new Vector2(0, 200);
        public FallingItemState(Item item) { 
            itemEntity = item;
            itemEntity.SpeedY = 0;
        }

        public override void Update(GameTime gameTime)
        {
            itemEntity.SpeedY += itemEntity.Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        public override void EnterNormalState()
        {
            //Debug.WriteLine("Entered item normal state");
            itemEntity.SpeedY = 0;
            itemEntity.ItemState = new NormalState(itemEntity);
        }
    }

    public class NormalState : ItemState {
        public NormalState(Item item) {
            itemEntity = item;
        }

        public override void EnterFallingState()
        {
            //Debug.WriteLine("Entered item falling state");
            itemEntity.ItemState = new FallingItemState(itemEntity);
        }
    }
}
