using Microsoft.Xna.Framework.Graphics;
using sprint0v2.Sprites;
using System;
using System.Diagnostics;

namespace sprint0v2.Entities.MarioStates.PowerUpStates
{
    public class SuperMario : PowerUpState
    {
        private Mario receiver;

        public SuperMario(Mario mario) : base (mario)
        {
            receiver = mario;
        }

        public override void Enter(IPowerUpState previousPowerupState)
        {
            //////Debug.WriteLine("Mario is now Super Mario");
            CurrentPowerupState = this;
            receiver.PowerUpState = CurrentPowerupState;
            this.previousPowerupState = previousPowerupState;

        }

        public override void Exit()
        {
            //////Debug.WriteLine("Super Mario state exited.");
        }

        public override void FireTransition()
        {
            //////Debug.WriteLine("Super Mario is transitioning to Fire Mario");
            CurrentPowerupState.Exit();
            CurrentPowerupState = new FireMario(receiver);
            CurrentPowerupState.Enter(this);
        }

        public override void StandardTransition()
        {
            //////Debug.WriteLine("Super Mario is transitioning to Standard Mario");
            CurrentPowerupState.Exit();
            CurrentPowerupState = new StandardMario(receiver);
            CurrentPowerupState.Enter(this);
        }
        public override void DeadTransition()
        {
            CurrentPowerupState.Exit();
            CurrentPowerupState = new DeadMario(receiver);
            CurrentPowerupState.Enter(this);
        }
        public override void StarTransition()
        {
            CurrentPowerupState.Exit();
            CurrentPowerupState = new StarMario(receiver);
            CurrentPowerupState.Enter(this);
        }

    }
}