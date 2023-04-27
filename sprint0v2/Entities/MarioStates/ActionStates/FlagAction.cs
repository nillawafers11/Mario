using sprint0v2.Entities;
using sprint0v2.Entities.MarioStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using sprint0v2;

public class FlagFallState : ActionState
{
    protected Mario receiver;
    protected readonly float fallSpeed = 2f; // set your desired fall speed
    protected readonly float gravity = 1.5f; // set your desired gravity value
    public FlagFallState(Mario mario) : base(mario)
    {
        receiver = mario;
    }

    public override void Enter(IMarioActionState previousActionState)
    {
        CurrentActionState = this;
        receiver.Sprite.IsAnimated = false;
        receiver.Sprite.CurrentFrame = 4;
        receiver.ActionState = CurrentActionState;
        this.previousActionState = previousActionState;

        receiver.Speed = new Vector2(0, 1); // Change the value to adjust fall speed
        receiver.Position = new Vector2(2890, receiver.Position.Y);
        receiver.ActionState.FaceLeftTransition();
         receiver.IsFinished= true;
    }

    public override void Update(GameTime gameTime)
    {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Debug.WriteLine(receiver.Position.Y + receiver.BoundingBox.Height);
        if (receiver.Position.Y + receiver.BoundingBox.Height > 205)
        {
            Debug.WriteLine(receiver.Position.Y);

            receiver.Speed = new Vector2(3, 0);
            receiver.Sprite.IsAnimated = true;
        }
        else
        {
            Grid.Instance.AddEntity(receiver);

        }
    }
}