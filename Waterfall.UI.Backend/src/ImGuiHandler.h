#pragma once

class ImGuiHandler
{
public:
	ImGuiHandler() = default;
	~ImGuiHandler() = default;

	void OnAttach();
	void OnDetach();
	// void OnEvent(Event);

	void Begin();
	void End();

	void SetDarkThemeColours();
};
