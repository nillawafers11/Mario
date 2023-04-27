using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2;

namespace sprint0v2.Entities.MarioStates
{
    public interface IMarioActionState
    {
        IMarioActionState PreviousState { get; }

        void Enter(IMarioActionState previousState);
        void Exit();
        void IdleTransition();
        void CrouchingTransition();
        void RunningTransition();
        void JumpingTransition();
        void FallingTransition();
        void FaceLeftTransition();
        void FaceRightTransition();
        void ClimbingTransition();
    }
}