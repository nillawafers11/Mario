using Microsoft.Xna.Framework;
using sprint0v2.Entities;
using sprint0v2.Entities.MarioStates;
using System;
using System.Diagnostics;

public class IdleState : ActionState
{
    private Mario receiver;
    private ActionState nextState;

    public IdleState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        CurrentActionState = this;
        receiver.ActionState = CurrentActionState;
        this.previousActionState = previousActionState;
        if (receiver.SpeedX == 0)
        {
            receiver.Sprite.IsAnimated = false;
            receiver.Sprite.CurrentFrame = 0;
        }
        //Debug.WriteLine("Mario has entered the idle state");
    }
    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Slow Mario to Zero
        if (receiver.SpeedX > 0)
        {
            receiver.SpeedX = Math.Max(receiver.SpeedX - acceleration * delta, 0f);
        }
        else if (receiver.SpeedX < 0)
        {
            receiver.SpeedX = Math.Min(receiver.SpeedX + acceleration * delta, 0f);
        }

        // Set animation and frame if necessary
        if (receiver.SpeedX == 0)
        {
            receiver.Sprite.IsAnimated = false;
            receiver.Sprite.CurrentFrame = 0;
        }
    }
}
