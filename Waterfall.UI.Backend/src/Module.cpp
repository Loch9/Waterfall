#include <glad/glad.h>
#define GLFW_INCLUDE_NONE
#include <GLFW/glfw3.h>

#include <imgui.h>
#include <stdio.h>

#include "ImGuiHandler.h"

#pragma warning(disable: 6386) // Buffer overrun while writing to 'array'

#ifdef _WIN32
#define wfapi extern "C" __declspec(dllexport)
#endif

void (*UpdateCallback)();
void (*ShutdownCallback)();
void (*ImGuiRenderCallback)();

void (*LogCallback)(int*, int, int);

void (*CreateWindowResizeEvent)(int, int);
void (*CreateWindowCloseEvent)();
void (*CreateKeyPressedEvent)(int, int);
void (*CreateKeyReleasedEvent)(int);
void (*CreateKeyTypedEvent)(int);
void (*CreateMouseButtonPressedEvent)(int);
void (*CreateMouseButtonReleasedEvent)(int);
void (*CreateMouseScrolledEvent)(float, float);
void (*CreateMouseMovedEvent)(float, float);

ImGuiHandler Handler;
GLFWwindow* Window;

enum LogLevel
{
	None = 0,
	Debug,
	Info,
	Warning,
	Error,
	Fatal
};

void Log(const char* message, LogLevel level)
{
	int* array = new int[strlen(message)];
	for (int i = 0; i < strlen(message); i++)
		array[i] = message[i];
	LogCallback(array, (int)level, (int)strlen(message));
	delete[] array;
}

void Update()
{
	while (!glfwWindowShouldClose(Window))
	{
		glClear(GL_COLOR_BUFFER_BIT);
		glClearColor(0.23f, 0.23f, 0.26f, 1.0f);
		
		Handler.Begin();
		ImGui::PushStyleVar(ImGuiStyleVar_PopupRounding, 11.25f);
		ImGuiRenderCallback();
		ImGui::PopStyleVar();
		Handler.End();

		/* Swap front and back buffers */
		glfwSwapBuffers(Window);

		/* Poll for and process events */
		glfwPollEvents();

		UpdateCallback();
	}
}

void Shutdown()
{
	ShutdownCallback();
	Handler.OnDetach();
	glfwTerminate();
}

void SetGLFWCallbacks()
{
	glfwSetWindowSizeCallback(Window, [](GLFWwindow* window, int width, int height)
	{
		CreateWindowResizeEvent(width, height);
	});

	glfwSetWindowCloseCallback(Window, [](GLFWwindow* window)
	{
		CreateWindowCloseEvent();
	});

	glfwSetKeyCallback(Window, [](GLFWwindow* window, int key, int scancode, int action, int mods)
	{
		switch (action)
		{
		case GLFW_PRESS:
			CreateKeyPressedEvent(key, 0);
			break;
		case GLFW_RELEASE:
			CreateKeyReleasedEvent(key);
			break;
		case GLFW_REPEAT:
			CreateKeyPressedEvent(key, 1);
			break;
		}
	});

	glfwSetCharCallback(Window, [](GLFWwindow* window, uint32_t keycode)
	{
		CreateKeyTypedEvent((int)keycode);
	});

	glfwSetMouseButtonCallback(Window, [](GLFWwindow* window, int button, int action, int mods)
	{
		switch (action)
		{
		case GLFW_PRESS:
			CreateMouseButtonPressedEvent(button);
			break;
		case GLFW_RELEASE:
			CreateMouseButtonReleasedEvent(button);
			break;
		}
	});

	glfwSetScrollCallback(Window, [](GLFWwindow* window, double xOffset, double yOffset)
	{
		CreateMouseScrolledEvent((float)xOffset, (float)yOffset);
	});

	glfwSetCursorPosCallback(Window, [](GLFWwindow* window, double xPos, double yPos)
	{
		CreateMouseMovedEvent((float)xPos, (float)yPos);
	});
}

wfapi void ModuleInit()
{
	glfwInit();

	Log("Creating window \"Waterfall\" (1600, 900).", Info);
	Window = glfwCreateWindow(1600, 900, "Waterfall", nullptr, nullptr);

	glfwMakeContextCurrent(Window);
	glfwSwapInterval(1);

	if (!gladLoadGLLoader((GLADloadproc)glfwGetProcAddress))
		return;

	SetGLFWCallbacks();

	Handler = ImGuiHandler();
	Handler.OnAttach();
}

wfapi void ModuleUpdate()
{
	Update();
	Shutdown();
}

wfapi void SetUpdateCallback(void (*callback)())
{
	UpdateCallback = callback;
}

wfapi void SetShutdownCallback(void (*callback)())
{
	ShutdownCallback = callback;
}

wfapi void SetImGuiRenderCallback(void (*callback)())
{
	ImGuiRenderCallback = callback;
}

wfapi void SetLogCallback(void (*callback)(int*, int, int))
{
	LogCallback = callback;
}

wfapi void SetCreateWindowResizeEventCallback(void (*callback)(int, int))
{
	CreateWindowResizeEvent = callback;
}

wfapi void SetCreateWindowCloseEventCallback(void (*callback)())
{
	CreateWindowCloseEvent = callback;
}

wfapi void SetCreateKeyPressedEventCallback(void (*callback)(int, int))
{
	CreateKeyPressedEvent = callback;
}

wfapi void SetCreateKeyReleasedEventCallback(void (*callback)(int))
{
	CreateKeyReleasedEvent = callback;
}

wfapi void SetCreateKeyTypedEventCallback(void (*callback)(int))
{
	CreateKeyTypedEvent = callback;
}

wfapi void SetCreateMouseButtonPressedEventCallback(void (*callback)(int))
{
	CreateMouseButtonPressedEvent = callback;
}

wfapi void SetCreateMouseButtonReleasedEventCallback(void (*callback)(int))
{
	CreateMouseButtonReleasedEvent = callback;
}

wfapi void SetCreateMouseScrolledEventCallback(void (*callback)(float, float))
{
	CreateMouseScrolledEvent = callback;
}

wfapi void SetCreateMouseMovedEventCallback(void (*callback)(float, float))
{
	CreateMouseMovedEvent = callback;
}

wfapi int i_glfwGetKey(int keycode)
{
	return glfwGetKey(Window, keycode);
}

wfapi int i_glfwGetMouseButton(int button)
{
	return glfwGetMouseButton(Window, button);
}

wfapi void i_glfwGetCursorPos(float* outxpos, float* outypos)
{
	double xpos, ypos;
	glfwGetCursorPos(Window, &xpos, &ypos);

	*outxpos = (float)xpos;
	*outypos = (float)ypos;
}
