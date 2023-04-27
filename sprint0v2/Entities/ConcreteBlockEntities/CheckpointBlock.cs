using Microsoft.Xna.Framework;
using sprint0v2.GameState;
using sprint0v2.CollisionDetection;

namespace sprint0v2.Entities.ConcreteBlockEntities
{
    public class CheckpointBlock : Block
    {
        private bool captured;
        public CheckpointBlock(Vector2 position) : base(position, Vector2.Zero, ItemType.Nothing, 0, true) {
            captured = false;
            
            Vector2 checkpointSize = new Vector2(16, 16);
            AABB = new AABB(Position, checkpointSize);
        }

        public override void Update(GameTime gameTime)
        {
            if (GameStateSnapshot.Instance is null)
            {
                new GameStateSnapshot();
            }
            if (Vector2.Distance(EntityManager.Instance.GetPlayer().Position, Position) < 32 && !captured)
            {
                Grid.Instance.RemoveEntity(this);
                captured = true;
                GameStateSnapshot.Instance.UpdateSnapshot();
            }
        }

        public void ReturnToSnapshot() {
            GameStateSnapshot.Instance.ReturnToSnapshot();
        }
    }
}
