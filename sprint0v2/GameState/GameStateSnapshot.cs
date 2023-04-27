using System;
using System.Collections.Generic;
using sprint0v2.Entities;
using sprint0v2.Entities.MarioStates.PowerUpStates;
using sprint0v2.View;
using Microsoft.Xna.Framework;

namespace sprint0v2.GameState
{
    public class GameStateSnapshot
    {
        public static GameStateSnapshot Instance { get; set; }
        private Vector2 marioPosition;
        private Vector2 marioSpeed;
        private ActionState marioAction;
        private IPowerUpState marioPowerUp;
        private int[] HUDSnapshot;
        public GameStateSnapshot() {
            Instance = this;
            UpdateSnapshot();
        }

        public void UpdateSnapshot() {
            HUDSnapshot = HUD.Instance.Snapshot;
            Mario oldMario = EntityManager.Instance.GetPlayer();
            marioPosition = oldMario.Position;
            marioSpeed = oldMario.Speed;
            marioAction = oldMario.ActionState;
            marioPowerUp = oldMario.PowerUpState;
        }

        public void ReturnToSnapshot() {
            HUD.Instance.Snapshot = HUDSnapshot;
            Mario returningMario = EntityManager.Instance.GetPlayer();
            Grid.Instance.AddEntity(returningMario);
            returningMario.Position = marioPosition;
            returningMario.PowerUpState = marioPowerUp;
            returningMario.Speed = marioSpeed;
            switch (marioPowerUp) {
                case StandardMario:
                    returningMario.Standard();
                    break;
                case SuperMario:
                    returningMario.Super();
                    break;
                case FireMario:
                    returningMario.Fire();
                    break;
            }
            switch (marioAction) {
                case IdleState:
                    returningMario.ActionState.IdleTransition();
                    break;
                case JumpingLeftState:
                    returningMario.ActionState.JumpingLeftTransition();
                    break;
                case JumpingRightState:
                    returningMario.ActionState.JumpingRightTransition();
                    break;
                case JumpingState:
                    returningMario.ActionState.JumpingTransition();
                    break;
                case RunningState:
                    returningMario.ActionState.RunningTransition();
                    break;
                case CrouchingState:
                    returningMario.ActionState.CrouchingTransition();
                    break;
                case FallingLeftState:
                    returningMario.ActionState.FallingLeftTransition();
                    break;
                case FallingRightState:
                    returningMario.ActionState.FallingRightTransition();
                    break;
                case FallingState:
                    returningMario.ActionState.FallingTransition();
                    break;
                case BouncingState:
                    returningMario.ActionState.BounceTransition();
                    break;
            }
        }
    }
}
