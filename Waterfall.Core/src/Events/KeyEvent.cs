using System;

namespace Waterfall.Core.Events
{
    public abstract class KeyEvent : Event
    {
        protected KeyCode m_KeyCode;

        protected KeyEvent(KeyCode keyCode)
            => m_KeyCode = keyCode;

        public KeyCode GetKeyCode() => m_KeyCode;

        public override int GetCategoryFlags()
        {
            return (int)(EventCategory.EventCategoryKeyboard | EventCategory.EventCategoryInput);
        }

        public abstract override EventType GetEventType();
        public abstract override string GetName();
    }

    public class KeyPressedEvent : KeyEvent
    {
        private int m_RepeatCount;

        public KeyPressedEvent(KeyCode keycode, int repeatCount)
            : base(keycode) => m_RepeatCount = repeatCount;

        public int GetRepeatCount() => m_RepeatCount;
        public override string ToString() => $"KeyPressedEvent: {m_KeyCode} ({m_RepeatCount} repeats)";
        public override EventType GetEventType() => EventType.KeyPressed;
        public override string GetName() => GetEventType().ToString();
    }

    public class KeyReleasedEvent : KeyEvent
    {
        public KeyReleasedEvent(KeyCode keycode)
            : base(keycode) { }

        public override string ToString() => $"KeyReleasedEvent: {m_KeyCode}";
        public override EventType GetEventType() => EventType.KeyReleased;
        public override string GetName() => GetEventType().ToString();
    }

    public class KeyTypedEvent : KeyEvent
    {
        public KeyTypedEvent(KeyCode keycode)
            : base(keycode) { }

        public override string ToString() => $"KeyTypedEvent: {m_KeyCode}";
        public override EventType GetEventType() => EventType.KeyTyped;
        public override string GetName() => GetEventType().ToString();
    }
}
