using Microsoft.Xna.Framework;
using sprint0v2.Entities;
using sprint0v2.Entities.MarioStates.PowerUpStates;

public abstract class PowerUpState : IPowerUpState
{
    protected IPowerUpState previousPowerupState;
    public Mario Mario { get; protected set; }
    protected IPowerUpState CurrentPowerupState { get { return Mario.PowerUpState; } set { Mario.PowerUpState = value; } }
    IPowerUpState IPowerUpState.PreviousState { get { return previousPowerupState; } }

    protected PowerUpState(Mario mario)
    {
        Mario = mario;
    }

    public virtual void Enter(IPowerUpState previousPowerupState)
    {
        CurrentPowerupState = this;
        Mario.PowerUpState = CurrentPowerupState;
        this.previousPowerupState = previousPowerupState;
    }

    public virtual void Exit() { }
    public virtual void FireTransition()
    {
        CurrentPowerupState.Exit();
        CurrentPowerupState = new FireMario(Mario);
        CurrentPowerupState.Enter(this);
    }
    public virtual void SuperTransition() { }
    public virtual void StandardTransition() { }
    public virtual void StarTransition() { }
    public virtual void DeadTransition() { }
}
