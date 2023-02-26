using System;
using System.Runtime.InteropServices;

namespace Waterfall.UI
{
    public static class ImGui
    {
        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool ImGui_Begin(string text, int flags = 0);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ImGui_End();

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ImGui_Text(string text);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint ImGui_GetID(string ptr_id);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void ImGui_DockSpace(uint id, float[] size);

        public static bool Begin(string text, int flags = 0) => ImGui_Begin(text, flags);
        public static void End() => ImGui_End();
        public static void Text(string text) => ImGui_Text(text);
        public static uint GetID(string ptr_id) => ImGui_GetID(ptr_id);
        public static void DockSpace(uint id, float[]? size = null)
        {
            if (size == null)
                size = new float[2] { 0, 0 };
            if (size.Length != 2)
                size = new float[2] { 0, 0 };
            ImGui_DockSpace(id, size);
        }
    }
}
