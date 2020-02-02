#include "ui.hpp"
#include "imgui.h"
#include "IMGUI/imgui_impl_glfw.hpp"
#include "IMGUI/imgui_impl_opengl3.hpp"
#include "IMGUI/imgui_fonts.hpp"

void InitializeUI(WindowContext ctx, const char* versionString, const char* fontPath)
{
	ImGui::CreateContext();
	ImGui_ImplOpenGL3_Init(versionString);
	ImGui_ImplGlfw_InitForOpenGL(ctx.windowRef->window, false);

	fonts::font_handler handler = fonts::font_handler({
		{"Default", fontPath, {14.f}}
	});
}

void EnterUIFrame()
{
	ImGui_ImplOpenGL3_NewFrame();
	ImGui_ImplGlfw_NewFrame();
	ImGui::NewFrame();
}

void ExitUIFrame()
{
	ImGui::Render();
	ImGui_ImplOpenGL3_RenderDrawData(ImGui::GetDrawData());
}

void EnterUIWindow(const char* name)
{
	ImGui::Begin(name);
}

void ExitUIWindow()
{
	ImGui::End();
}

void UIText(const char* text)
{
	ImGui::Text(text);
}

void UIColoredText(const char* text, Color color)
{
	ImGui::TextColored(ImVec4(color.r, color.g, color.b, color.a), text);
}

bool UIButton(const char* label)
{
	return ImGui::Button(label);
}

void UICheckbox(const char* label, bool* value)
{
	ImGui::Checkbox(label, value);
}

void UIColorPicker3(const char* label, Color* color)
{
	ImGui::ColorEdit3(label, (float*)color);
}

void UIVector2(const char* label, ImVec2* vec, float speed, float min, float max)
{
	ImGui::DragFloat2(label, (float*)vec, speed, min, max);
}

void UISeparator()
{
	ImGui::Separator();
}