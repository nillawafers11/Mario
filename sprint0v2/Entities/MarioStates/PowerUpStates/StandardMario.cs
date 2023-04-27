using System;
using System.Diagnostics;

namespace sprint0v2.Entities.MarioStates.PowerUpStates
{
    public class StandardMario : PowerUpState
    {
        private Mario receiver;

        public StandardMario(Mario mario) : base(mario)
        {
            receiver = mario;
        }

        public override void Enter(IPowerUpState previousPowerupState)
        {
            //////Debug.WriteLine("Mario is now Standard Mario");
            CurrentPowerupState = this;
            receiver.PowerUpState = CurrentPowerupState;
            this.previousPowerupState = previousPowerupState;
        }

        public override void Exit()
        {
            //////Debug.WriteLine("Standard Mario state exited.");
        }

        public override void SuperTransition()
        {
            //////Debug.WriteLine("Standard Mario is transitioning to Super Mario");
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
        public override void StarTransition()
        {
            CurrentPowerupState.Exit();
            CurrentPowerupState = new StarMario(receiver);
            CurrentPowerupState.Enter(this);
        }
        public override void FireTransition()
        {
            CurrentPowerupState.Exit();
            CurrentPowerupState = new FireMario(receiver);
            CurrentPowerupState.Enter(this);
        }
    }
}