using Microsoft.Xna.Framework;
using sprint0v2.Entities;
using sprint0v2.Entities.MarioStates;
using sprint0v2.Entities.MarioStates.PowerUpStates;
using System;
using System.Diagnostics;

public class CrouchingState : ActionState
{
    private Mario receiver;

    public CrouchingState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        // Code to execute when entering the Crouching state
        CurrentActionState = this;
        receiver.ActionState = CurrentActionState;
        this.previousActionState = previousActionState;
        receiver.Sprite.IsAnimated = false;
        if (receiver.PowerUpState is not StandardMario)
        {
            receiver.Sprite.CurrentFrame = 5;
        }
        else
        {
            receiver.Sprite.CurrentFrame = 0;
        }
        //Debug.WriteLine("Mario has entered the crouch state");
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
    }
}
