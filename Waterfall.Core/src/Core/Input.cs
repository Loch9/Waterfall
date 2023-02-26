using System;

namespace Waterfall.Core
{
    public static class Input
    {
        public static bool IsKeyDown(KeyCode key)
        {
            return InternalCalls.i_glfwGetKey((int)key) == 1 || InternalCalls.i_glfwGetKey((int)key) == 2;
        }

        public static bool IsMouseButtonPressed(MouseCode button)
        {
            return InternalCalls.i_glfwGetMouseButton((int)button) == 1;
        }

        public static unsafe (float x, float y) GetMousePosition()
        {
            float xpos = 0.0f, ypos = 0.0f;
            InternalCalls.i_glfwGetCursorPos(&xpos, &ypos);
            return (xpos, ypos);
        }
    }
}
