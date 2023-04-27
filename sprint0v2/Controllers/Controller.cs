using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sprint0v2.Commands;

namespace sprint0v2.Controllers
{
    public abstract class Controller
    {
        protected Dictionary<int, ICommand> downCommands;

        protected Controller()
        {
            downCommands = new Dictionary<int, ICommand>();
        }
        public void Command(int key, ICommand value)
        {
            downCommands.Add(key, value);
        }

        public void RemoveAllCommands()
        {
            downCommands.Clear();
        }

        public abstract void UpdateInput();
    }
}