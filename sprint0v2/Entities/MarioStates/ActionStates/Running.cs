using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0v2.Entities;
using sprint0v2.Entities.MarioStates;
using System.Diagnostics;

public class RunningState : ActionState
{
    private readonly Mario receiver;
    private readonly float maxSpeed = 1.5f; // set your desired maximum speed

    public RunningState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        CurrentActionState = this;
        receiver.ActionState = CurrentActionState;
        this.previousActionState = previousActionState;
        receiver.Sprite.IsAnimated = true;
        if (receiver.Sprite.SpriteFlip != SpriteEffects.None)
            receiver.SpeedX -= 1;
        else
            receiver.SpeedX += 1;
        //Debug.WriteLine("Mario has entered the running state");
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Update speed based on current direction
        if (receiver.SpeedX > 0)
        {
            receiver.SpeedX += acceleration * delta;
            receiver.SpeedX = MathHelper.Clamp(receiver.SpeedX, 0, maxSpeed);
        }
        else if (receiver.SpeedX < 0)
        {
            receiver.SpeedX -= acceleration * delta;
            receiver.SpeedX = MathHelper.Clamp(receiver.SpeedX, -maxSpeed, 0);
        }
    }
}
