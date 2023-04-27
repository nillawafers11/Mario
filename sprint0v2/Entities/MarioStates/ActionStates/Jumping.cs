using Microsoft.Xna.Framework;
using sprint0v2.Entities.MarioStates;
using sprint0v2.Entities;
using System.Diagnostics;

public class JumpingState : ActionState
{
    protected Mario receiver;
    protected readonly float maxJumpHeight = 75f; // set your desired maximum jump height
    protected readonly float jumpSpeed = -6f; // set your desired jump speed
    protected readonly float maxSpeed = 1.5f; // set your desired maximum speed
    protected float initialJumpHeight; // variable to store the height at the start of the jump
    public JumpingState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        // Implement the logic to enter the jumping state
        // This might include setting the sprite to the jumping animation, adjusting velocity, etc.
        // For example:
        receiver.Sprite.IsAnimated = false;
        Mario.Sprite.CurrentFrame = 4;
        receiver.Speed = new Vector2(receiver.Speed.X, jumpSpeed); // Set initial upward velocity
        initialJumpHeight = receiver.Position.Y; // Store the starting height of the jump
        CurrentActionState = this;
        receiver.ActionState = CurrentActionState;
        //Debug.WriteLine("Mario has entered the jump state");
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Apply gravity to Mario's velocity
        receiver.Speed = new Vector2(receiver.Speed.X, receiver.Speed.Y + receiver.Acceleration * delta);
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
        Exit();
    }

    public override void Exit()
    {
        // Check if Mario has reached the peak of the jump
        if (receiver.Position.Y <= initialJumpHeight - maxJumpHeight || receiver.Speed.Y >= 0)
        {
            // Transition to falling state
            ActionState fallingState = new FallingState(receiver);
            fallingState.Enter(this);
        }
    }
}

public class JumpingLeftState : JumpingState
{
    public JumpingLeftState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        CurrentActionState = this;
        receiver.ActionState = CurrentActionState;
        receiver.SpeedX -= 1;
        //Debug.WriteLine("Mario has entered the jumpleftstate");
    }
}

public class JumpingRightState : JumpingState
{
    public JumpingRightState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        CurrentActionState = this;
        receiver.ActionState = CurrentActionState;
        receiver.SpeedX += 1;
        //Debug.WriteLine("Mario has entered the jumpRightstate");
    }
}
