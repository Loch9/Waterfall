using System.Runtime.InteropServices;
using System.Text;

namespace CompileUI
{
    internal class Program
    {
        static readonly string[] ImGuiKeyWords = new[]
        {
            "Begin",
            "End",
            "Text",
            "Dockspace"
        };

        static void Main(string[] args)
        {
            string filepath = args[0];
            string[] lines = File.ReadAllLines(filepath);
            string file = string.Join(Environment.NewLine, lines);

            File.Delete("out.cpp");
            FileStream fs = new FileStream("out.cpp", FileMode.CreateNew, FileAccess.Write);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                fs.Write(Encoding.Default.GetBytes("extern \"C\" __declspec(dllexport) void ImGuiRender()\n{\n\t\n}\n"));
            else
                fs.Write(Encoding.Default.GetBytes("void ImGuiRender()\n{\n\t\n}\n"));


            foreach (string line in lines)
            {
                if (new string(line.Trim()[0], 1) + new string(line.Trim()[1], 1) == "//")
                {
                    continue;
                }

                foreach (string word in line.Split(" "))
                {
                    if (ImGuiKeyWords.Contains(word))
                    {

                    }
                    
                }
            }

            fs.Close();
        }
    }
}