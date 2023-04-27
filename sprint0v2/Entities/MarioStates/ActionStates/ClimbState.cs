using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities.MarioStates.ActionStates
{
    public class ClimbState : ActionState
    {
        protected Mario receiver;
        protected float climbSpeed = -4f;
        public ClimbState(Mario mario) : base(mario)
        {
            receiver = mario;
        }
        public override void Enter(IMarioActionState previousActionState)
        {
            receiver.Speed = new Vector2(0, climbSpeed);
            CurrentActionState = this;
            receiver.ActionState = CurrentActionState;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Exit()
        {
            
        }
    }
}
