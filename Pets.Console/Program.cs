using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommandLine;
using Pets.Logic;
using Pets.DAO;
using ConsoleTables;

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
                       Console.WriteLine($"PROGRAM STARTED. Arguments: FileName:{o.FileName}");
                       Manager manager = new Manager(o.FileName);
                       RequestSearchPet request = new RequestSearchPet();
                       request.SearchPetGender = o.GenderSearch;
                       request.SearchPetName = o.NameSearch;
                       request.SearchPetType = o.TypeSearch;

                       if (request.SearchPetGender != null)
                       {
                           switch (request.SearchPetGender.ToUpper())
                           {
                               case "MALE":
                                   request.SearchPetGender = "M";
                                   break;
                               case "FEMALE":
                                   request.SearchPetGender = "F";
                                   break;
                               default:
                                   throw new Exception("Only allowed values are MALE or FEMALE");

                           }
                       }
                       if (request.SearchPetName != null)
                       {
                           request.SearchPetName = request.SearchPetName.Trim();
                       }
                       if (request.SearchPetType != null)
                       {
                           request.SearchPetType = request.SearchPetType.Trim().ToUpper();
                       }

                       var resultSearch = manager.SearchPet(request);

                       //draw a nice table here :)
                       Console.WriteLine("Search Results:");
                       var table = new ConsoleTable(new string[] { "Animal Type", "Animal Name", "Gender", "Timestamp" }); //Animal Type, Animal Name, Gender, Timestamp
                       foreach (var row in resultSearch.results)
                       {
                           table.AddRow(row.AnimalType, row.AnimalName, row.Gender, DateTime.ParseExact(row.LastUpdate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture));
                       }
                       table.Write();
                       Console.WriteLine("End of Search,That's all");
                   });


        }
    }
}
