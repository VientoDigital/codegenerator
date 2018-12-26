using System;
using System.IO;
using mGnu;

namespace iCodeGenerator.iCodeGeneratorConsole
{
    /// <summary>
    /// Summary description for MainConsole.
    /// </summary>
    public class MainConsole
    {
        private string inputFolder = String.Empty;
        private string outputFolder = String.Empty;
        private string connectionString = String.Empty;
        private string providerType = String.Empty;
        private bool verbose = false;
        private bool errors = false;

        public MainConsole()
        {
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                PrintUsage();
            }

            new MainConsole().Run(args);
        }

        private void Run(string[] args)
        {
            GetInputValues(args);
            if (!errors)
            {
                GenerateCode();
            }
        }

        private void GetInputValues(string[] args)
        {
            GetOpt opt = new GetOpt(args, "vi:o:c:t:");
            Arg arg = opt.NextArg();
            while (arg != null)
            {
                switch (arg.Flag)
                {
                    case "-v":
                        verbose = true;
                        break;

                    case "-i":
                        inputFolder = GetFolderPath(arg, "Invalid input folder path.");
                        break;

                    case "-o":
                        outputFolder = GetFolderPath(arg, "Invalid output folder path");
                        break;

                    case "-c":
                        connectionString = arg.Parameter.Trim();
                        break;

                    case "-t":
                        providerType = arg.Parameter.Trim();
                        break;

                    default:
                        PrintUsage();
                        return;
                }
                arg = opt.NextArg();
            }
        }

        private string GetFolderPath(Arg arg, string message)
        {
            if (Directory.Exists(arg.Parameter.Trim()))
            {
                return arg.Parameter.Trim();
            }
            else
            {
                Console.Error.WriteLine(message);
                errors = true;
                return String.Empty;
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("iCodeGeneratorConsole [-v] -i <Input Folder> -o <Output Folder> -c <Connection String>");
            Console.WriteLine();
        }

        private void GenerateCode()
        {
        }
    }
}