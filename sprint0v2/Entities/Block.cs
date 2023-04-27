using Microsoft.Xna.Framework;
using sprint0v2.Entities.ConcreteBlockEntities.BlockStates;

namespace sprint0v2.Entities
{

	public abstract class Block : Entity
    {
		protected BlockState _currentState;
        protected ItemType itemType;
		protected int _currentItemCount;
		protected bool isHidden;

        public bool IsHidden
        {
            get { return isHidden; }
        }

		public virtual void CreateBrickPieces() { }

		public virtual ItemType ItemHeld{ 
			set { }
			get { return itemType; }
		}
		public virtual int ItemCount
        {
			set { }
			get { return _currentItemCount;  }
		}

		public virtual BlockState BlockState { 
			set { _currentState = value; }
			get { return _currentState; }
		}

		public Block(Vector2 position, Vector2 speed, ItemType itemType, int itemCount, bool isHidden) 
			: base(position, speed)
        {
            gravity = 0;
            this.itemType = itemType;
			ItemCount= itemCount;
			this.isHidden= isHidden;
            BoundingBoxColor = Color.Yellow;
        }
    }
}
