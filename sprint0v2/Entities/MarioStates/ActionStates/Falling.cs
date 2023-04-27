using sprint0v2.Entities;
using sprint0v2.Entities.MarioStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;

public class FallingState : ActionState
{
    protected Mario receiver;
    protected readonly float fallSpeed = 2f; // set your desired fall speed
    protected readonly float gravity = 1.5f; // set your desired gravity value
    protected readonly float maxSpeed = 3f; // set your desired maximum speed
    public FallingState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        // Code to execute when entering the Falling state
        CurrentActionState = this;
        receiver.Sprite.IsAnimated = false;
        receiver.Sprite.CurrentFrame = 4;
        receiver.ActionState = CurrentActionState;
        this.previousActionState = previousActionState;
        //Debug.WriteLine("Mario has entered the falling state");

        // Apply downward velocity to Mario
        receiver.Speed = new Vector2(receiver.Speed.X, fallSpeed); // Change the value to adjust fall speed
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Apply gravity to Mario's velocity
        receiver.Speed = new Vector2(receiver.Speed.X, receiver.Speed.Y + gravity * delta); // Change the value to adjust gravity
        
        //if (receiver.SpeedX > 0)
        //{
        //    receiver.SpeedX = Math.Max(receiver.SpeedX - acceleration * delta, 0f);
        //}
        //else if (receiver.SpeedX < 0)
        //{
        //    receiver.SpeedX = Math.Min(receiver.SpeedX + acceleration * delta, 0f);
        //}
        Slow(gameTime);
        Exit();
    }

    public void Slow(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        bool leftKeyPressed = keyboardState.IsKeyDown(Keys.Left);
        bool rightKeyPressed = keyboardState.IsKeyDown(Keys.Right);

        if (!leftKeyPressed)
        {
            if (receiver.SpeedX < 0)
            {
                receiver.SpeedX = Math.Min(receiver.SpeedX + acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds, 0f);
            }
        }
        else if (!rightKeyPressed)
        {
            if (receiver.SpeedX > 0)
            {
                receiver.SpeedX = Math.Max(receiver.SpeedX - acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds, 0f);
            }
        }
    }

    public override void Exit()
    {
        KeyboardState keyboardState = Keyboard.GetState();
        bool leftKeyPressed = keyboardState.IsKeyDown(Keys.Left);
        bool rightKeyPressed = keyboardState.IsKeyDown(Keys.Right);
        // Check for collisions with the ground
        if (receiver.isGrounded)
        {
            // Set Mario's horizontal speed to 0 if no keys are pressed and Mario has landed
            if (!leftKeyPressed && !rightKeyPressed)
            {
                if (receiver.ActionState is not IdleState)
                {
                    IdleTransition();
                }
            }
            // Transition to walking state if Mario is moving horizontally
            else
            {
                RunningTransition();
            }
        }
    }
}

public class FallingLeftState : FallingState
{
    public FallingLeftState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        // Code to execute when entering the Falling state
        CurrentActionState = this;
        receiver.ActionState = CurrentActionState;
        this.previousActionState = previousActionState;
        receiver.SpeedX -= 1;
        //Debug.WriteLine("Mario has entered the falling left state");
    }

    public override void Update(GameTime gameTime)
    {
        if (receiver.SpeedX < 0)
        {
            receiver.SpeedX -= acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            receiver.SpeedX = MathHelper.Clamp(receiver.SpeedX, -maxSpeed, 0);
        }
        base.Update(gameTime);
    }
}
public class FallingRightState : FallingState
{
    public FallingRightState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        // Code to execute when entering the Falling state
        CurrentActionState = this;
        receiver.ActionState = CurrentActionState;
        this.previousActionState = previousActionState;
        receiver.SpeedX += 1;
        //Debug.WriteLine("Mario has entered the falling right state");
    }

    public override void Update(GameTime gameTime)
    {
        if (receiver.SpeedX > 0)
        {
            receiver.SpeedX += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            receiver.SpeedX = MathHelper.Clamp(receiver.SpeedX, 0, maxSpeed);
        }
        base.Update(gameTime);
    }
}
