using Waterfall.Core;
using System.Runtime.InteropServices;
using Waterfall.Core.Events;

namespace Waterfall.UI
{
    public class UIModule : IUIModule
    {
        public string PluginName { get; set; } = "UI";

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetUpdateCallback(delegate* unmanaged[Cdecl]<void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetShutdownCallback(delegate* unmanaged[Cdecl]<void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetImGuiRenderCallback(delegate* unmanaged[Cdecl]<void> callback);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void SetLogCallback(delegate* unmanaged[Cdecl]<int*, int, int, void> callback);

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

        public unsafe void Init()
        {
            SetUpdateCallback(&UICallbacks.OnUpdate);
            SetShutdownCallback(&UICallbacks.OnShutdown);
            SetImGuiRenderCallback(&UICallbacks.OnImGuiRender);

            SetLogCallback(&Application.LoggerCallback);

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