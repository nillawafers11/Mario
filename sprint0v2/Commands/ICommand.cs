using sprint0v2;
using sprint0v2.Entities;
using Microsoft.Xna.Framework;

namespace sprint0v2.Commands;

public interface ICommand
{
    void Execute();
    void Stop();
}
public abstract class CommandImpl<TReceiver> : ICommand
{
    protected TReceiver receiver;
    protected CommandImpl(TReceiver receiver)
    {
        this.receiver = receiver;
    }
    public abstract void Execute();
    public virtual void Stop() { }


}

public abstract class MarioCommand : CommandImpl<Mario>
{
    protected MarioCommand(Mario receiver)
        : base(receiver)
    {
    }
  
}
public abstract class GameCommand : CommandImpl<Game>
{
    protected GameCommand(Game receiver)
        : base(receiver)
    {
    }
}

