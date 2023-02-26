#include <imgui.h>
#include <memory>

#ifdef _WIN32
#define wfexport extern "C" __declspec(dllexport)
#endif

wfexport bool ImGui_Begin(const char* name, int flags = 0)
{
	return ImGui::Begin(name, (bool*)0, flags);
}

wfexport void ImGui_End()
{
	ImGui::End();
}

wfexport void ImGui_Text(const char* text)
{
	ImGui::Text(text);
}

wfexport uint32_t ImGui_GetID(const char* ptr_id)
{
	return ImGui::GetID(ptr_id);
}

wfexport void ImGui_DockSpace(uint32_t id, float size[2] = new float[2] { 0, 0 })
{
	ImGui::DockSpace(id, ImVec2{ size[0], size[1] });
}
