using Waterfall.Core;
using System.Runtime.InteropServices;

using Waterfall.Core.Events;
using Waterfall.UI.Lang;

namespace Waterfall.UI.Module
{
    public class UIModule : IUIModule
    {
        public string PluginName { get; set; } = "UI";

        #region Imports
        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetUpdateCallback(delegate* unmanaged[Cdecl]<void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetShutdownCallback(delegate* unmanaged[Cdecl]<void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetImGuiRenderCallback(delegate* unmanaged[Cdecl]<void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetLogCallback(delegate* unmanaged[Cdecl]<short*, int, int, void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetCreateWindowResizeEventCallback(delegate* unmanaged[Cdecl]<int, int, void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetCreateWindowCloseEventCallback(delegate* unmanaged[Cdecl]<void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetCreateKeyPressedEventCallback(delegate* unmanaged[Cdecl]<int, int, void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetCreateKeyReleasedEventCallback(delegate* unmanaged[Cdecl]<int, void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetCreateKeyTypedEventCallback(delegate* unmanaged[Cdecl]<int, void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetCreateMouseButtonPressedEventCallback(delegate* unmanaged[Cdecl]<int, void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetCreateMouseButtonReleasedEventCallback(delegate* unmanaged[Cdecl]<int, void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetCreateMouseScrolledEventCallback(delegate* unmanaged[Cdecl]<float, float, void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetCreateMouseMovedEventCallback(delegate* unmanaged[Cdecl]<float, float, void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ModuleInit();

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ModuleUpdate();
        #endregion Imports

        public unsafe void Init()
        {
            // UI / Application callbacks
            SetUpdateCallback(&UICallbacks.OnUpdate);
            SetShutdownCallback(&UICallbacks.OnShutdown);
            SetImGuiRenderCallback(&UICallbacks.OnImGuiRender);

            // Logger callback
            SetLogCallback(&Application.LoggerCallback);

            // Event callbacks
            SetCreateWindowResizeEventCallback(&Event.CreateWindowResizeEvent);
            SetCreateWindowCloseEventCallback(&Event.CreateWindowCloseEvent);
            SetCreateKeyPressedEventCallback(&Event.CreateKeyPressedEvent);
            SetCreateKeyReleasedEventCallback(&Event.CreateKeyReleasedEvent);
            SetCreateKeyTypedEventCallback(&Event.CreateKeyTypedEvent);
            SetCreateMouseButtonPressedEventCallback(&Event.CreateMouseButtonPressedEvent);
            SetCreateMouseButtonReleasedEventCallback(&Event.CreateMouseButtonReleasedEvent);
            SetCreateMouseScrolledEventCallback(&Event.CreateMouseScrolledEvent);
            SetCreateMouseMovedEventCallback(&Event.CreateMouseMovedEvent);

            ModuleInit();

            UIWindow window = new UIWindow
            {
                Children = {
                    new Label
                    {
                        Text = "Hello!"
                    },
                    new Label
                    {
                        Text = "Hello part two!",

                        Children = {
                            new Label
                            {
                                Text = "Hello part three!"
                            }
                        }
                    }
                }
            };
        }

        public void Update() => ModuleUpdate();
        public void Shutdown() { }
        public void OnEvent(Event @event) { }

        public void OnImGuiRender()
        {
            ImGui.Begin("Dockspace");

            uint dockspaceId = ImGui.GetID("WaterfallDockspace");
            ImGui.DockSpace(dockspaceId);

            // Setup dockspace
            ImGui.Begin("Hello!");
            ImGui.Text("Hello world!");
            ImGui.End();

            ImGui.End();
        }
    }
}