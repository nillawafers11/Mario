using sprint0v2.Entities.MarioStates;
using sprint0v2.Entities;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0v2.Entities.MarioStates.ActionStates;

public abstract class ActionState : IMarioActionState
{
    protected Mario Mario;
    protected readonly float acceleration = 3f;
    protected IMarioActionState previousActionState;
    protected ActionState CurrentActionState 
    { 
        get { return Mario.ActionState; } 
        set { Mario.ActionState = value; } 
    }
    IMarioActionState IMarioActionState.PreviousState 
    {
        get { return previousActionState; } 
    }
    public ActionState(Mario mario) => Mario = mario;

    public virtual void Enter(IMarioActionState previousActionState)
    {
        CurrentActionState = this;
        Mario.ActionState = CurrentActionState;
        this.previousActionState = previousActionState;
    }
    public virtual void Exit() { }
    public virtual void IdleTransition()
    {
        ActionState idle = new IdleState(Mario);
        idle.Enter(Mario.ActionState);
    }
    public virtual void CrouchingTransition()
    {
        ActionState crouch = new CrouchingState(Mario);
        crouch.Enter(Mario.ActionState);
    }
    public virtual void RunningTransition()
    {
       ActionState running = new RunningState(Mario);
       running.Enter(Mario.ActionState);
    }
    public virtual void JumpingTransition()
    {
       ActionState jumping = new JumpingState(Mario);
       jumping.Enter(Mario.ActionState);
    }
    public virtual void JumpingLeftTransition()
    {
        ActionState jumping = new JumpingLeftState(Mario);
        jumping.Enter(Mario.ActionState);
    }
    public virtual void JumpingRightTransition()
    {
        ActionState jumping = new JumpingRightState(Mario);
        jumping.Enter(Mario.ActionState);
    }
    public virtual void FallingTransition()
    {
        ActionState falling = new FallingState(Mario);
        falling.Enter(Mario.ActionState);
    }
    public virtual void FallingLeftTransition()
    {
        ActionState falling = new FallingLeftState(Mario);
        falling.Enter(Mario.ActionState);
    }
    public virtual void FallingRightTransition()
    {
        ActionState falling = new FallingRightState(Mario);
        falling.Enter(Mario.ActionState);
    }
    public virtual void BounceTransition()
    {
        ActionState bounce = new BouncingState(Mario);
        bounce.Enter(Mario.ActionState);
    }
    public virtual void FlagFallTransition()
    {
        ActionState flagFall = new FlagFallState(Mario);
        flagFall.Enter(Mario.ActionState);
    }
    public virtual void FaceLeftTransition()
    {
        Mario.Sprite.SpriteFlip = SpriteEffects.FlipHorizontally;
    }
    public virtual void FaceRightTransition()
    {
        Mario.Sprite.SpriteFlip = SpriteEffects.None;
    }
    public virtual void ClimbingTransition()
    {
        ActionState climbing = new ClimbState(Mario);
        climbing.Enter(Mario.ActionState);
    }
    public virtual void Update(GameTime gameime) { }
}
