#pragma once
#include <GLFW/glfw3.h>

#ifdef SPREADGFX_EXPORTS
#define SPREAD_API extern "C" __declspec(dllexport)
#else
#define SPREAD_API extern "C" __declspec(dllimport)
#endif

struct WindowOptions;

class Window
{
public:
	Window();
	Window(WindowOptions cfg);
	~Window();

	GLFWwindow* window;
	int keyState[GLFW_KEY_LAST + 1];
	void ClearInput();
private:
	void Initialize(WindowOptions cfg);
};

// Exposed DLL Functions

#pragma pack(push, 8)
struct WindowOptions
{
	int width, height;
	int glMajor, glMinor;
	bool resizable;
	char* windowTitle;
	bool vsync;
};
#pragma pack(pop)

#pragma pack(push, 8)
struct WindowContext
{
	Window* windowRef;
};
#pragma pack(pop)

enum class KeyActions : int
{
	KEY_NONE = 0, KEY_PRESS, KEY_DOWN, KEY_RELEASE
};

SPREAD_API WindowContext CreateWindowContext(WindowOptions cfg);
SPREAD_API void ClearColor(float r, float g, float b);
SPREAD_API bool ShouldRender(WindowContext ctx);
SPREAD_API void EnterRenderLoop(WindowContext ctx);
SPREAD_API void ExitRenderLoop(WindowContext ctx);

// Input
SPREAD_API bool GetKeyDown(WindowContext ctx, int key);