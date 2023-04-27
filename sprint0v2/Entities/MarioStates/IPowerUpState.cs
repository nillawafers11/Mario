using Microsoft.Xna.Framework;
using sprint0v2.Entities;
using System.Net.Sockets;

public interface IPowerUpState
{
    IPowerUpState PreviousState { get; }
    
    void Enter(IPowerUpState previousPowerupState);
    void Exit();
    void FireTransition() { }
    void SuperTransition() { }
    void StandardTransition() { }
    void StarTransition() { }
    void DeadTransition() { }
}