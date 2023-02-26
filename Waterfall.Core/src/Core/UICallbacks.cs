using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Waterfall.Core
{
    public class UICallbacks
    {
        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void OnUpdate()
        {
            Application.Instance.Update();
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void OnShutdown()
        {
            Application.Instance.Shutdown();
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void OnImGuiRender()
        {
            Application.Instance.OnImGuiRender();
        }
    }
}
