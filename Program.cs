using HarmonyLib;
using System;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Title = "AntiInvokePatcher";
        string totalArgs = "";

        if (args.Length > 0)
        {
            foreach (string arg in args)
            {
                if (totalArgs == "")
                {
                    totalArgs = arg;
                }
                else
                {
                    totalArgs += " " + arg;
                }
            }

            ClearArguments(ref totalArgs);
        }

        while (!System.IO.File.Exists(totalArgs) || !System.IO.Path.GetExtension(totalArgs).ToLower().Equals(".exe"))
        {
            Console.WriteLine("Please, insert a valid .NET executable file here: ");
            totalArgs = Console.ReadLine();
            ClearArguments(ref totalArgs);

            if (!System.IO.File.Exists(totalArgs))
            {
                Console.WriteLine("The specified file does not exist.");
            }
            else if (!System.IO.Path.GetExtension(totalArgs).ToLower().Equals(".exe"))
            {
                Console.WriteLine("The specified file is not a executable (*.exe) file.");
            }
        }
        
        try
        {
            GlobalVariables.LoadedAssembly = Assembly.LoadFile(totalArgs);
            ParameterInfo[] parameterInfo = GlobalVariables.LoadedAssembly.EntryPoint.GetParameters();
            object[] parameters = new object[parameterInfo.Length];
            Harmony harmonyPatch = new Harmony("AntiInvokePatcher");
            harmonyPatch.PatchAll(Assembly.GetExecutingAssembly());
            GlobalVariables.LoadedAssembly.EntryPoint.Invoke(null, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load the file with the patches.\r\n{ex.Message}\r\n{ex.StackTrace}\r\n{ex.Source}");
        }

        Console.ReadLine();
    }

    public static void ClearArguments(ref string totalArgs)
    {
        while (totalArgs.StartsWith("\"") || totalArgs.StartsWith(" ") || totalArgs.StartsWith("\t"))
        {
            totalArgs = totalArgs.Substring(1);
        }

        while (totalArgs.EndsWith("\"") || totalArgs.EndsWith(" ") || totalArgs.EndsWith("\t"))
        {
            totalArgs = totalArgs.Substring(0, totalArgs.Length - 1);
        }
    }
}