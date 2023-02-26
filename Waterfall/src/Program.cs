using Waterfall.UI;
using Waterfall.Core;

namespace Waterfall
{
    internal class Program
    {
        static int Main(string[] args)
        {
            Application app = new Application();

            Console.CancelKeyPress += (sender, e) =>
            {
                app.Running = false;
                e.Cancel = true;
            };

            app.AddModule<LoggerModule>();
            app.Logger.SetLogMethod(Console.WriteLine);

            app.AddModule<UIModule>();

            return app.Run();
        }
    }
}