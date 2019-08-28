using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommandLine;

namespace Pets.ConsoleProgram
{
    class Program
    {
        public class Options
        {
            [Value(0, MetaName = "CSV File Path,",
            HelpText = "CSV file to be processed,must be the file name of csv file must be relative to the executable of this console program (e.g. test.csv)",
            Required = true)]
            public string FileName { get; set; }

            [Option('n', "name", Required = false, HelpText = "Pet Name argument to be searched.")]
            public string NameSearch { get; set; }

            [Option('t', "type", Required = false, HelpText = "Pet Type argument to be searched.")]
            public string TypeSearch { get; set; }

            [Option('g', "gender", Required = false, HelpText = "Pet Gender argument to be searched.")]
            public string GenderSearch { get; set; }


        }

        static void Main(string[] args)
        {
           

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {

                       Console.Write($"FileName:{o.FileName} ,Enabled!");
                       if (o.NameSearch != "")
                       {
                           Console.WriteLine($"Name Search argument present:{o.NameSearch}");
                       }
                   });

           /* if (args.Length == 0)
            {
                Console.WriteLine("Please insert a valid argument.");
                Console.WriteLine("First argument must be the file name of csv file must be relative to the executable of this console program (e.g. test.csv)");
                Console.WriteLine("Second argument is search type over the csv file (e.g type=cat) (e.g type=dog gender=female) ");

            }
            var filePath = args[0];
            string filePathAbs =Path.GetFullPath(filePath);
            if (!File.Exists(filePath))
            {
                Console.WriteLine(String.Format("{0} File not exists!", filePath));
                return;
            }
             */

        }
    }
}
