using System;
using Goudkoorts.Controllers;

namespace Goudkoorts
{
    class Program
    {
        private static void Main(string[] args)
        {
            var intervalMilliseconds = -1;
            
            if (args.Length > 0)
            {
                int.TryParse(args[0], out intervalMilliseconds);
            }
            
            var controller = new Controller(intervalMilliseconds);
            controller.Start();
        }
    }
}
