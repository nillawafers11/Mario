using Microsoft.Xna.Framework;
using sprint0v2.Entities.MarioStates;
using sprint0v2.Entities;
using System.Diagnostics;

public class BouncingState : ActionState
{
    private readonly Mario receiver;
    private readonly float maxBounceHeight = 40f; // set your desired maximum bounce height
    private readonly float bounceSpeed = -10f; // set your desired bounce speed
    private float initialBounceHeight; // variable to store the height at the start of the bounce

    public BouncingState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        // Implement the logic to enter the bouncing state
        // This might include setting the sprite to the bouncing animation, adjusting velocity, etc.
        // For example:
        Mario.Sprite.CurrentFrame = 4;
        receiver.Speed = new Vector2(receiver.Speed.X, bounceSpeed); // Set initial upward velocity
        initialBounceHeight = receiver.Position.Y; // Store the starting height of the bounce
        CurrentActionState = this;
        receiver.ActionState = CurrentActionState;
        //Debug.WriteLine("Mario has entered the bounce state");
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Apply gravity to Mario's velocity
        receiver.Speed = new Vector2(receiver.Speed.X, receiver.Speed.Y + receiver.Gravity * delta);

        // Check if Mario has reached the peak of the bounce
        if (receiver.Position.Y <= initialBounceHeight - maxBounceHeight || receiver.Speed.Y >= 0)
        {
            // Transition to falling state
            ActionState fallingState = new FallingState(receiver);
            fallingState.Enter(this);
        }
    }
}
