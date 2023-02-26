using System;

namespace Waterfall.Core.Events
{
    public class MouseMovedEvent : Event
    {
        private float m_MouseX, m_MouseY;

        public MouseMovedEvent(float mouseX, float mouseY)
        {
            m_MouseX = mouseX;
            m_MouseY = mouseY;
        }

        public float GetX() => m_MouseX;
        public float GetY() => m_MouseY;

        public override string ToString() => $"MouseMovedEvent: {m_MouseX}, {m_MouseY}";
        public override EventType GetEventType() => EventType.MouseMoved;
        public override string GetName() => GetEventType().ToString();
        public override int GetCategoryFlags()
        {
            return (int)(EventCategory.EventCategoryMouse | EventCategory.EventCategoryInput);
        }
    }

    public class MouseScrolledEvent : Event
    {
        float m_XOffset, m_YOffset;

        public MouseScrolledEvent(float xOffset, float yOffset)
        {
            m_XOffset = xOffset;
            m_YOffset = yOffset;
        }

        public override string ToString() => $"MouseScrolledEvent: {m_XOffset}, {m_YOffset}";
        public override EventType GetEventType() => EventType.MouseScrolled;
        public override string GetName() => GetEventType().ToString();
        public override int GetCategoryFlags()
        {
            return (int)(EventCategory.EventCategoryMouse | EventCategory.EventCategoryInput);
        }
    }

    public abstract class MouseButtonEvent : Event
    {
        protected MouseCode m_Button;

        protected MouseButtonEvent(MouseCode button)
            => m_Button = button;

        public MouseCode GetMouseButton() => m_Button;

        public override int GetCategoryFlags()
        {
            return (int)(EventCategory.EventCategoryMouse | EventCategory.EventCategoryInput);
        }

        public abstract override EventType GetEventType();
        public abstract override string GetName();
    }

    public class MouseButtonPressedEvent : MouseButtonEvent
    {
        public MouseButtonPressedEvent(MouseCode button)
            : base(button) { }

        public override string ToString() => $"MouseButtonPressedEvent: {m_Button}";
        public override EventType GetEventType() => EventType.MouseButtonPressed;
        public override string GetName() => GetEventType().ToString();
    }

    public class MouseButtonReleasedEvent : MouseButtonEvent
    {
        public MouseButtonReleasedEvent(MouseCode button)
            : base(button) { }

        public override string ToString() => $"MouseButtonReleasedEvent: {m_Button}";
        public override EventType GetEventType() => EventType.MouseButtonReleased;
        public override string GetName() => GetEventType().ToString();
    }
}
