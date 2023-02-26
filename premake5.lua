workspace "Waterfall"
	architecture "x86_64"

	configurations
	{
		"Debug",
		"Release"
	}

outputdir = "%{cfg.buildcfg}"

group "Dependencies"
	include "Dependencies/Glad"
	include "Dependencies/GLFW"
	include "Dependencies/imgui"
group ""

include "Waterfall.UI.Backend"