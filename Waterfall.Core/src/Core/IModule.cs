using System;
using System.Collections.Generic;

using Waterfall.Core.Events;

namespace Waterfall.Core
{
    public interface IModule
    {
        string PluginName { get; set; }

        void Init();
        void Update();
        void Shutdown();
        void OnEvent(Event @event);
        void OnImGuiRender();
    }

    public interface IUIModule : IModule { }
}
