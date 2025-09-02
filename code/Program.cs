using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WDL2CS
{
    class Program
    {
        static bool showTokens = false;
        static bool excludeProperties = false;
        static bool generatePropertyList = false;
        static bool showHelp = false;

        static string inputFilename = "";
        static string outputFilename = "";
        static string scriptname = "";
        static string selectedFormat = "cs";

        static List<string> availableFormats = new List<string>() { "cs" };
        static List<Transformer> transformers = new List<Transformer>();

        static int Main(string[] args)
        {
            int result = ProcessInput(args);

            if (result == 0)
            {
                //this is the spice
                SetTransformers();
                WDLParser parser = new WDLParser()
                {
                    ShowTokens = showTokens,
                    Transformers = transformers,
                };
                result = parser.Parse(inputFilename);
                //this was the spice

                if (result == 0)
                {
                    ProcessOutput();
                }
            }
            return result;
        }

        static int ProcessInput(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "-h":
                    case "--help":
                        showHelp = true;
                        break;

                    case "-t":
                        showTokens = true;
                        break;

                    case "-p":
                        generatePropertyList = true;
                        excludeProperties = true;
                        break;

                    case "-f":
                        if (++i < args.Length)
                        {
                            selectedFormat = args[i].ToLower();
                        }
                        else
                        {
                            Console.WriteLine("-f Missing format identifier");
                            return 1;
                        }
                        break;

                    default:
                        if (inputFilename == "") inputFilename = args[i];
                        else if (outputFilename == "") outputFilename = args[i];
                        else if (scriptname == "") scriptname = args[i];
                        else
                        {
                            Console.WriteLine("Too many arguments");
                            return 1;
                        }
                        break;
                }
            }

            if (showHelp)
            {
                Console.WriteLine($"Usage: {AppDomain.CurrentDomain.FriendlyName} [options] inputfile [outputfile] [Class Identifier]");
                Console.WriteLine($"Options:");
                Console.WriteLine($"-h, --help   Show help");
                Console.WriteLine($"-f <format>  Enable specific output file format. Default: 'cs'. Available formats: {string.Join(", ", availableFormats)}");
                Console.WriteLine($"-p           Generate a list of properties (and remove from output file");
                Console.WriteLine($"-t           List parsed tokens (for debugging purposes)");
                return 0;
            }

            if (inputFilename == "")
            {
                Console.WriteLine($"Usage: {AppDomain.CurrentDomain.FriendlyName} [options] inputfile [outputfile] [Class Identifier]");
                Console.WriteLine($"Specify -h option for more information");
                return 1;
            }

            if (scriptname == "")
            {
                scriptname = Path.GetFileNameWithoutExtension(inputFilename);
            }

            return 0;
        }

        static void SetTransformers()
        {
            switch (selectedFormat)
            {
                case "cs":
                default:
                    transformers.Add(new CSTransformer(scriptname, excludeProperties));
                    Console.WriteLine("Selected format: C#");
                    break;
            }

            if (generatePropertyList)
            {
                transformers.Add(new ListTransformer());
                Console.WriteLine("Selected format: Property List");
            }
        }

        static void ProcessOutput()
        {
            if (!string.IsNullOrEmpty(transformers[0].Result))
            {
                StreamWriter output;
                if (outputFilename != "")
                {
                    File.Delete(outputFilename);
                    Stream os = File.OpenWrite(outputFilename);
                    output = new StreamWriter(os, new UTF8Encoding(false));
                }
                else
                {
                    output = new StreamWriter(Console.OpenStandardOutput(), new UTF8Encoding(false));
                }
                if (output != null)
                {
                    output.WriteLine(transformers[0].Result);
                    output.Close();
                }
            }

            if (generatePropertyList)
            {
                PrintProperties((ListTransformer)transformers[1]);
            }

        }

        static void PrintProperties(ListTransformer transformer)
        {
            /* Generic types only for avoiding any type dependencies on transpiler code
             *  Object/Asset type
             *      Object/Asset name
             *          Object/Asset property ID
             *              property values (multiple sets)
             */
            foreach (var objects in transformer.List)
            {
                Console.WriteLine("[" + objects.Key + "]");
                foreach (var type in objects.Value)
                {
                    Console.WriteLine("\t" + type.Key);
                    foreach (var property in type.Value)
                    {
                        Console.WriteLine("\t\t[" + property.Key + "]");
                        foreach (var values in property.Value)
                        {
                            Console.WriteLine("\t\t\t" + string.Join(" ", values));
                        }
                    }
                }
            }
        }

    }
}
