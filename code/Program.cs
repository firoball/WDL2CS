using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WDL2CS
{
    class Program
    {
        static int Main(string[] args)
        {
            bool showTokens = false;
            bool generatePropertyList = false;
            string inputFilename = "";
            string outputFilename = "";
            string scriptname = "";

            foreach (string s in args)
            {
                if (s.ToLower() == "-t")
                {
                    showTokens = true;
                }
                else if (s.ToLower() == "-p")
                {
                    generatePropertyList = true;
                }
                else
                {
                    if (inputFilename == "") inputFilename = s;
                    else
                    if (outputFilename == "") outputFilename = s;
                    else
                    if (scriptname == "") scriptname = s;
                    else
                    {
                        Console.WriteLine("Too many arguments!");
                        return 1;
                    }
                }
            }
            if (inputFilename == "")
            {
                System.Console.WriteLine("You need to specify input and (optional) outputfile: compiler.exe input.txt [output.txt] [C# Class Identifier]");
                return 1;
            }
            if (scriptname == "")
            {
                scriptname = Path.GetFileNameWithoutExtension(inputFilename);
            }

            WDLParser parser = new WDLParser()
            {
                ScriptName = scriptname,
                ShowTokens = showTokens,
                GeneratePropertyList = generatePropertyList
            };

            int result = parser.Parse(inputFilename, out string code);
            if (result == 0)
            {
                StreamWriter output;
                if (outputFilename != "")
                {
                    File.Delete(outputFilename);
                    Stream os = File.OpenWrite(outputFilename);
                    output = new StreamWriter(os, new System.Text.UTF8Encoding(false));
                }
                else
                {
                    output = new StreamWriter(Console.OpenStandardOutput(), new System.Text.UTF8Encoding(false));
                }
                if (output != null)
                {
                    output.WriteLine(code);
                    output.Close();
                }

                PrintProperties(parser);
            }
            return result;
        }

        static void PrintProperties(WDLParser parser)
        {
            /* Generic types only for avoiding any type dependencies on transpiler code
             *  Object/Asset type
             *      Object/Asset name
             *          Object/Asset property ID
             *              property values (multiple sets)
             */
            if (parser.GeneratePropertyList)
            {
                foreach (var objects in parser.PropertyList)
                {
                    Console.WriteLine(objects.Key);
                    foreach (var type in objects.Value)
                    {
                        Console.WriteLine("\t" + type.Key);
                        foreach (var property in type.Value)
                        {
                            Console.WriteLine("\t\t" + property.Key);
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
}
