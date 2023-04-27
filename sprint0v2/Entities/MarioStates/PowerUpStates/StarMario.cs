using Microsoft.Xna.Framework;
using sprint0v2.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0v2.Entities.MarioStates.PowerUpStates
{
    public class StarMario : PowerUpState
    {
        private Mario receiver;
        private int duration = 12;

        public StarMario(Mario mario) : base(mario)
        {
            receiver = mario;
        }
        public override void Enter(IPowerUpState previousPowerupState)
        {
            CurrentPowerupState = this;
            receiver.PowerUpState = CurrentPowerupState;
            this.previousPowerupState = previousPowerupState;
        }
        public override void SuperTransition()
        {
            CurrentPowerupState.Exit();
            CurrentPowerupState = new SuperMario(receiver);
            CurrentPowerupState.Enter(this);
        }
        public override void DeadTransition()
        {
            CurrentPowerupState.Exit();
            CurrentPowerupState = new DeadMario(receiver);
            CurrentPowerupState.Enter(this);
        }
    }
}
