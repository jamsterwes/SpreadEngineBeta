#include <GL/glew.h>
#include "windowing.hpp"

#include <algorithm>

Window::Window()
{
	const char* windowTitle = "No Properties for this Window!";
	Initialize({
		800, 600, 4, 3, false, const_cast<char*>(windowTitle), false
	});
}

Window::Window(WindowOptions cfg)
{
	Initialize(cfg);
}

Window::~Window()
{
	glfwDestroyWindow(window);
	glfwTerminate();
}

void key_callback(GLFWwindow* window, int key, int scancode, int action, int mods)
{
	Window* ctx = (Window*)glfwGetWindowUserPointer(window);
	if (key >= 0 && key <= GLFW_KEY_LAST)
	{
		if (action == GLFW_PRESS) ctx->keyState[key] = (int)KeyActions::KEY_PRESS;
		else if (action == GLFW_RELEASE) ctx->keyState[key] = (int)KeyActions::KEY_RELEASE;
	}
}

void Window::ClearInput()
{
	for (int i = 0; i <= GLFW_KEY_LAST; i++)
	{
		int key = keyState[i];
		if (key == (int)KeyActions::KEY_PRESS) keyState[i] = (int)KeyActions::KEY_DOWN;
		else if (key == (int)KeyActions::KEY_RELEASE) keyState[i] = (int)KeyActions::KEY_NONE;
	}
}

void Window::Initialize(WindowOptions cfg)
{
	if (!glfwInit())
	{
		// ...
	}

	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, cfg.glMajor);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, cfg.glMinor);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);

	glfwWindowHint(GLFW_RESIZABLE, cfg.resizable ? GL_TRUE : GL_FALSE);  // Resizable
	glfwWindowHint(GLFW_SAMPLES, 4);  // 4x MSAA

	window = glfwCreateWindow(cfg.width, cfg.height, cfg.windowTitle, nullptr, nullptr);
	glfwSetWindowUserPointer(window, this);
	glfwSetInputMode(window, GLFW_LOCK_KEY_MODS, GLFW_FALSE);

	glfwSetKeyCallback(window, key_callback);

	glfwMakeContextCurrent(window);
	glfwSwapInterval(cfg.vsync ? 1 : 0);  // V-Sync

	glEnable(GL_MULTISAMPLE);
	// -- OLD MINECRAFT2 SETTINGS, MAKE CONFIGURABLE LATER
	//glEnable(GL_CULL_FACE);
	//glCullFace(GL_CW);
	//glEnable(GL_BLEND);
	//glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	glewExperimental = GL_TRUE;
	glewInit();
}

// Exposed DLL Functions

WindowContext CreateWindowContext(WindowOptions cfg)
{
	WindowContext ctx;
	ctx.windowRef = new Window(cfg);
	return ctx;
}

void ClearColor(float r, float g, float b)
{
	glClearColor(r, g, b, 1.0f);
}

bool ShouldRender(WindowContext ctx)
{
	return !glfwWindowShouldClose(ctx.windowRef->window);
}

void EnterRenderLoop(WindowContext ctx)
{
	glEnable(GL_DEPTH_TEST);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
}

void ExitRenderLoop(WindowContext ctx)
{
	glfwSwapBuffers(ctx.windowRef->window);

	ctx.windowRef->ClearInput();
	glfwPollEvents();
}

bool GetKey(WindowContext ctx, int key)
{
	return (ctx.windowRef->keyState[key] == (int)KeyActions::KEY_DOWN) || (ctx.windowRef->keyState[key] == (int)KeyActions::KEY_PRESS);
}

bool GetKeyDown(WindowContext ctx, int key)
{
	return ctx.windowRef->keyState[key] == (int)KeyActions::KEY_PRESS;
}