using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Waterfall.Core;

namespace Waterfall.Core.Events
{
    public enum EventType
    {
        None = 0,
		WindowClose, WindowResize, WindowFocus, WindowLostFocus, WindowMoved,
		AppTick, AppUpdate, AppRender,
		KeyPressed, KeyReleased, KeyTyped,
		MouseButtonPressed, MouseButtonReleased, MouseMoved, MouseScrolled
    }
    
    public enum EventCategory
    {
        None = 0,
        EventCategoryApplication    = 1 << 0,
        EventCategoryInput          = 1 << 1,
        EventCategoryKeyboard       = 1 << 2,
        EventCategoryMouse          = 1 << 3,
        EventCategoryMouseButton    = 1 << 4
    }

    public abstract class Event
    {
        #region EventHandlers
        public static event EventHandler<WindowCloseEvent> OnWindowClose = (sender, e) => { };
        public static event EventHandler<WindowResizeEvent> OnWindowResize = (sender, e) => { };
        public static event EventHandler<KeyPressedEvent> OnKeyPressed = (sender, e) => { };
        public static event EventHandler<KeyReleasedEvent> OnKeyReleased = (sender, e) => { };
        public static event EventHandler<KeyTypedEvent> OnKeyTyped = (sender, e) => { };
        public static event EventHandler<MouseButtonPressedEvent> OnMouseButtonPressed = (sender, e) => { };
        public static event EventHandler<MouseButtonReleasedEvent> OnMouseButtonReleased = (sender, e) => { };
        public static event EventHandler<MouseMovedEvent> OnMouseMoved = (sender, e) => { };
        public static event EventHandler<MouseScrolledEvent> OnMouseScrolled = (sender, e) => { };

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void CreateWindowResizeEvent(int width, int height)
        {
            WindowResizeEvent e = new WindowResizeEvent((uint)width, (uint)height);
            OnWindowResize(null, e);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void CreateWindowCloseEvent()
        {
            WindowCloseEvent e = new WindowCloseEvent();
            OnWindowClose(null, e);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void CreateKeyPressedEvent(int key, int repeatCount)
        {
            KeyPressedEvent e = new KeyPressedEvent((KeyCode)key, repeatCount);
            OnKeyPressed(null, e);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void CreateKeyReleasedEvent(int key)
        {
            KeyReleasedEvent e = new KeyReleasedEvent((KeyCode)key);
            OnKeyReleased(null, e);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void CreateKeyTypedEvent(int key)
        {
            KeyTypedEvent e = new KeyTypedEvent((KeyCode)key);
            OnKeyTyped(null, e);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void CreateMouseButtonPressedEvent(int button)
        {
            MouseButtonPressedEvent e = new MouseButtonPressedEvent((MouseCode)button);
            OnMouseButtonPressed(null, e);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void CreateMouseButtonReleasedEvent(int button)
        {
            MouseButtonReleasedEvent e = new MouseButtonReleasedEvent((MouseCode)button);
            OnMouseButtonReleased(null, e);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void CreateMouseScrolledEvent(float xOffset, float yOffset)
        {
            MouseScrolledEvent e = new MouseScrolledEvent(xOffset, yOffset);
            OnMouseScrolled(null, e);
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
        public static void CreateMouseMovedEvent(float x, float y)
        {
            MouseMovedEvent e = new MouseMovedEvent(x, y);
            OnMouseMoved(null, e);
        }
        #endregion

        public bool Handled = false;

        public abstract EventType GetEventType();
        public abstract string GetName();
        public abstract int GetCategoryFlags();
        public override string ToString() { return GetName(); }

        public bool IsInCategory(EventCategory category)
        {
            return ((EventCategory)GetCategoryFlags() & category) != 0;
        }
    }
}
