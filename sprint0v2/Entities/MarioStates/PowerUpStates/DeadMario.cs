using Microsoft.Xna.Framework;
using sprint0v2.CollisionDetection;
using sprint0v2.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities.MarioStates.PowerUpStates
{
    public class DeadMario : PowerUpState
    {
        private Mario receiver;

        public DeadMario(Mario mario) : base(mario)
        {
            receiver = mario;
        }
        public override void Enter(IPowerUpState previousPowerupState)
        {
            CurrentPowerupState = this;
            receiver.PowerUpState = CurrentPowerupState;
            this.previousPowerupState = previousPowerupState;
        }
    }
}
