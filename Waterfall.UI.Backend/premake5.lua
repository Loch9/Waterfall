project "Waterfall.UI.Backend"
    kind "SharedLib"
    language "C++"
    cppdialect "C++17"
    
    targetdir ("%{prj.location}/bin/" .. outputdir)
    objdir ("%{prj.location}/obj/" .. outputdir)

    files
    {
        "%{prj.location}/src/**.h",
        "%{prj.location}/src/**.cpp"
    }

    includedirs
    {
        "%{wks.location}/Dependencies/Glad/include",
        "%{wks.location}/Dependencies/GLFW/include",
        "%{wks.location}/Dependencies/imgui",
    }

    links
    {
        "GLFW",
        "Glad",
        "ImGui",
    }

    filter "system:windows"
        systemversion "latest"

        links
        {
            "opengl32.lib"
        }

        postbuildcommands
        {
            ("xcopy %{prj.location}bin\\" .. outputdir .. " %{wks.location}Waterfall\\bin\\" .. outputdir)
        }

    filter "system:macosx"
		systemversion "10.13"

		links
		{
			"OpenGL.framework",
			"Cocoa.framework",
			"IOKit.framework"
		}
