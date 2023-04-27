using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using sprint0v2.Sprites;
using System;
using System.Diagnostics;
using sprint0v2.Entities;

namespace sprint0v2.Commands
{
    public class JumpCommand : MarioCommand
    {
        public JumpCommand(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            //////Debug.WriteLine("User hit Player Jump");
           
            receiver.Jump(); 
        }
        public override void Stop()
        {
            receiver.Fall();
        }
    }

    public class Left : MarioCommand
    {
        public Left(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            //////Debug.WriteLine("User hit Player Left");
            receiver.MoveLeft();
        }
        public override void Stop()
        {
            receiver.StopMovingLeft();
        }
    }

    public class Right : MarioCommand
    {
        public Right(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            //////Debug.WriteLine("User hit Player Right");
            receiver.MoveRight();
        }
        public override void Stop()
        {
            receiver.StopMovingRight();
        }
    }

    public class Crouch : MarioCommand
    {
        public Crouch(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            //////Debug.WriteLine("User hit Player Crouch");
            receiver.Crouch();
        }
        public override void Stop()
        {
            receiver.Crouch();
        }
    }

    public class ShootFireball : MarioCommand
    {
        public ShootFireball(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            receiver.ShootFireball();
        }
    }

    public class Standard : MarioCommand
    {
        public Standard(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
          
            receiver.Standard();
        }
    }

    public class Super : MarioCommand
    {
        public Super(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            //////Debug.WriteLine("User hit I for Player Super Powerup State");
            receiver.Super();
        }
    }

    public class Fire : MarioCommand
    {
        public Fire(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            //////Debug.WriteLine("User hit O for Player Fire Powerup State");
            receiver.Fire();
        }
    }
    public class StarMan : MarioCommand
    {
        public StarMan(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            //////Debug.WriteLine("User hit O for Player Fire Powerup State");
            receiver.Starman();
        }
    }

    public class TakeDamage : MarioCommand
    {
        public TakeDamage(Mario receiver) : base(receiver) { }

        public override void Execute()
        {
            receiver.TakeDamage();
        }
    }
}
