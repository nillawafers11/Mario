using sprint0v2.Commands;
namespace sprint0v2.Controllers;

    interface IController
    {
        void Command(int key, ICommand value);
        void UpdateInput();
    }

