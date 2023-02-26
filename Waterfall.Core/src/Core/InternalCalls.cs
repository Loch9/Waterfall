using System.Runtime.InteropServices;

namespace Waterfall.Core
{
    internal static class InternalCalls
    {
        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int i_glfwGetKey(int keycode);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int i_glfwGetMouseButton(int button);

        [DllImport("Waterfall.UI.Backend.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void i_glfwGetCursorPos(float* outxpos, float* outypos);
    }
}