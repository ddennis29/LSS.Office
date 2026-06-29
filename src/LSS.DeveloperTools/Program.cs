using System;
using System.IO;
using System.Linq;

namespace LSS.DeveloperTools;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("LSS Developer Tools");
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: LSS.DeveloperTools <vba-folder>");
            return;
        }
        var folder = args[0];
        foreach (var file in Directory.EnumerateFiles(folder, "*.bas").OrderBy(Path.GetFileName))
            Console.WriteLine(Path.GetFileName(file));
    }
}
