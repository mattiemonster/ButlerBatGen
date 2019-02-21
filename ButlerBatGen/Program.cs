using System;
using System.IO;

namespace ButlerBatGen
{
    class Program
    {
        static string username, projectName, path;

        static void Main(string[] args)
        {
            // Console colour
            Console.ForegroundColor = ConsoleColor.White;

            // Introduction
            Console.WriteLine("Butler Bat Generator");
            Console.WriteLine("Created by Mattie");
            Console.WriteLine("--------------------");

            // Get username
            Console.Write("Enter your username: ");
            username = Console.ReadLine();
            Console.WriteLine("Your username is: " + username);

            // Get project name
            Console.WriteLine("--------------------");
            Console.Write("Enter your projects name: ");
            projectName = Console.ReadLine();
            Console.WriteLine("Your projects name is: " + projectName);

            // Get output path
            Console.WriteLine("--------------------");
            Console.Write("Enter the output path (leave empty for exe path, include trailing slash): ");
            path = Console.ReadLine();
            
            // Check path
            if (string.IsNullOrEmpty(path))
            {
                path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            } else
            {
                if (!Directory.Exists(path))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Directory does not exist. Will be created.");
                    Directory.CreateDirectory(path);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            
            Console.WriteLine("Output path is: " + path);
            
            Console.WriteLine("--------------------");
            Console.WriteLine("Press any key to begin generation.");
            Console.ReadKey();

            Generate("win");
            Generate("linux");
            Generate("osx");
            Generate("webgl");
            Generate("android");

            ExitWaitForKey();
        }

        static void ExitWaitForKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static void Generate(string platform)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Generating generator for " + platform + " builds.");
            Console.Write("Input path (including file) for " + platform + " builds (leave empty to skip): ");
            string pathForPlatform = Console.ReadLine();

            if (string.IsNullOrEmpty(pathForPlatform))
                return;
            else
            {
                // Generate
                StreamWriter file = File.CreateText(path + "push_" + platform + ".bat");
                file.WriteLine("@echo off");
                file.WriteLine("butler push \"" + pathForPlatform + "\" " + username + "/" + projectName + ":" + platform);
                file.WriteLine("pause");
                file.Flush();
                file.Close();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Finished generation.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
