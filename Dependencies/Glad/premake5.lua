project "Glad"
    kind "StaticLib"
    language "C"
    staticruntime "off"
    
    targetdir ("%{prj.location}/bin/" .. outputdir)
    objdir ("%{prj.location}/obj/" .. outputdir)

    files
    {
        "include/glad/glad.h",
        "include/KHR/khrplatform.h",
        "src/glad.c"
    }

    includedirs
    {
        "include"
    }
    
    filter "system:windows"
        systemversion "latest"

    filter "system:macosx"
        systemversion "10.13"

    filter "configurations:Debug"
        runtime "Debug"
        symbols "on"

    filter "configurations:Release"
        runtime "Release"
        optimize "on"
