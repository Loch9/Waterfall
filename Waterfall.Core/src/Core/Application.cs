using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Waterfall.Core.Util;
using Waterfall.Core.Events;

namespace Waterfall.Core
{
    public class Application
    {
        public string Name { get; private set; }
        public bool Running { get; set; } = true;
        public int ReturnCode { get; set; } = 0;
        public static Application Instance;

        public List<IModule> Modules = new List<IModule>();
        public LoggerModule Logger = null;
        public IUIModule UI = null;

        public Application(string name = "Waterfall")
        {
            Name = name;
            Instance = this;
        }

        public int Run()
        {
            if (Logger == null)
            {
                LoggerModule tempLogger = new LoggerModule();
                tempLogger.Pattern = "!@[!t] [!b]: !v!*";
                tempLogger.SetLogMethod(Console.WriteLine);
                tempLogger.LogFatal("Application's logger module has not been added. Add it with AddModule<LoggerModule>().");
                return -1;
            }

            Logger.Init("Waterfall");

            foreach (IModule module in Modules)
                module.Init();

            if (UI == null)
            {
                Logger.LogFatal("Please add a UI module which contols the Update() and Shutdown() callbacks.");
                return -1;
            }

            UI.Init();
            UI.Update();

            return 0;
        }

        public void Update()
        {
            // Update modules
            foreach (IModule module in Modules)
                module.Update();
        }

        private bool KeyPressed(KeyPressedEvent @event)
        {
            Logger.LogInfo(@event.ToString());

            return false;
        }

        public int Shutdown()
        {
            Logger.LogInfo("Shutting down...");

            foreach (IModule module in Modules)
                module.Shutdown();

            return ReturnCode;
        }

        public void OnImGuiRender()
        {
            UI.OnImGuiRender();

            foreach (IModule module in Modules)
                module.OnImGuiRender();
        }

        public void AddModule<T>() where T : IModule, new()
        {
            if (typeof(T) != typeof(LoggerModule))
            {
                if (typeof(T).GetInterfaces().Contains(typeof(IUIModule)))
                    UI = (IUIModule)new T();
                else
                    Modules.Add(new T());
            }
            else if (Logger == null)
                Logger = new LoggerModule();
        }

        public void RemoveModule<T>() where T : IModule
        {
            if (typeof(T) != typeof(LoggerModule))
                Modules.RemoveAll(module => module.GetType() == typeof(T));
            else
                Logger.LogWarning("Logger module cannot be removed as it may limit some of the functionality of other modules.");
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static unsafe void LoggerCallback(int* message, int level, int length)
        {
            string str = "";
            for (int i = 0; i < length; i++)
                str += (char)message[i];

            Instance.Logger.Log((LogLevel)level, str);
        }
    }
}
