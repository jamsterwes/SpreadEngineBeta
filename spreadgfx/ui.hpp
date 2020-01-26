#pragma once
#include "windowing.hpp"

#ifdef SPREADGFX_EXPORTS
#define SPREAD_API extern "C" __declspec(dllexport)
#else
#define SPREAD_API extern "C" __declspec(dllimport)
#endif

#pragma pack(push, 8)
struct Color
{
	float r, g, b, a;
};
#pragma pack(pop)

SPREAD_API void InitializeUI(WindowContext ctx, const char* versionString, const char* fontPath);
SPREAD_API void EnterUIFrame();
SPREAD_API void ExitUIFrame();

// Windows
SPREAD_API void EnterUIWindow(const char* name);
SPREAD_API void ExitUIWindow();

SPREAD_API void UIText(const char* text);
SPREAD_API void UIColoredText(const char* text, Color color);

SPREAD_API bool UIButton(const char* label);
SPREAD_API void UICheckbox(const char* label, bool* value);

SPREAD_API void UIColorPicker3(const char* label, Color* color);