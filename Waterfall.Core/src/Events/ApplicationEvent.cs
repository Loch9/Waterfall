using System;

namespace Waterfall.Core.Events
{
    public class WindowResizeEvent : Event
    {
        private uint m_Width, m_Height;

        public WindowResizeEvent(uint width, uint height)
        {
            m_Width = width;
            m_Height = height;
        }

        public uint GetWidth() => m_Width;
        public uint GetHeight() => m_Height;

        public override string ToString()
        {
            return $"WindowResizeEvent: {m_Width}, {m_Height}";
        }

        public override EventType GetEventType()
        {
            return EventType.WindowResize;
        }

        public override string GetName()
        {
            return GetEventType().ToString();
        }

        public override int GetCategoryFlags()
        {
            return (int)EventCategory.EventCategoryApplication;
        }
    }

    public class WindowCloseEvent : Event
    {
        public WindowCloseEvent() { }

        public override EventType GetEventType()
        {
            return EventType.WindowClose;
        }

        public override string GetName()
        {
            return GetEventType().ToString();
        }

        public override int GetCategoryFlags()
        {
            return (int)EventCategory.EventCategoryApplication;
        }
    }

    public class AppTickEvent : Event
    {
        public AppTickEvent() { }

        public override EventType GetEventType()
        {
            return EventType.AppTick;
        }

        public override string GetName()
        {
            return GetEventType().ToString();
        }

        public override int GetCategoryFlags()
        {
            return (int)EventCategory.EventCategoryApplication;
        }
    }

    public class AppUpdateEvent : Event
    {
        public AppUpdateEvent() { }

        public override EventType GetEventType()
        {
            return EventType.AppUpdate;
        }

        public override string GetName()
        {
            return GetEventType().ToString();
        }

        public override int GetCategoryFlags()
        {
            return (int)EventCategory.EventCategoryApplication;
        }
    }

    public class AppRenderEvent : Event
    {
        public AppRenderEvent() { }

        public override EventType GetEventType()
        {
            return EventType.AppRender;
        }

        public override string GetName()
        {
            return GetEventType().ToString();
        }

        public override int GetCategoryFlags()
        {
            return (int)EventCategory.EventCategoryApplication;
        }
    }
}
