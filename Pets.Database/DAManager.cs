using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CsvHelper;
using Pets.DAO;

namespace Pets.Database
{
    public class CsvLine
    {
        public string AnimalType { get; set; }
        public string AnimalName { get; set; }
        public string Gender { get; set; }
        public string LastUpdate { get; set; }
    }
    class DAManager : IDAManager
    {
        public string CsvFilePath = "";
        List<CsvLine> CsvFileContent = new List<CsvLine>();
        public DAManager(string csvFilePath)
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader))
            {
                CsvFileContent = csv.GetRecords<CsvLine>().ToList();
            }

        }
        public bool Delete(string AnimalType, string AnimalName)
        {
            var match = CsvFileContent.Where(x => x.AnimalType == AnimalType && x.AnimalName == AnimalName).First();
            if (match != null)
            {
                CsvFileContent.Remove(match);
                using (var writer = new StreamWriter(CsvFilePath))
                using (var csv = new CsvWriter(writer))
                {
                    csv.WriteRecords(CsvFileContent);
                }
                return true;
            }
            return false;
        }

        public bool Insert(string AnimalType, string AnimalName, string Gender)
        {
            CsvLine newCsvLine = new CsvLine();
            newCsvLine.AnimalName = AnimalName;
            newCsvLine.AnimalType = AnimalType;
            newCsvLine.Gender = Gender;
            CsvFileContent.Add(newCsvLine);
            using (var writer = new StreamWriter(CsvFilePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(CsvFileContent);
            }
            return true;
        }

        public List<Pet> Search(string searchPetName, string searchPetType, string searchPetGender)
        {
            List<Pet> res = new List<Pet>();
            var partialResult=new List<CsvLine>();
            if (CsvFileContent.Count == 0)
            {
                return res;
            }
            if (searchPetName != String.Empty)
            {
                partialResult.AddRange(CsvFileContent.Where(
                    x => x.AnimalName.ToUpper().Contains(searchPetName.ToUpper())).ToList());
            }
            if (searchPetType != String.Empty)
            {
                partialResult.AddRange(CsvFileContent.Where(
                    x => x.AnimalType.ToUpper().Contains(searchPetType.ToUpper())).ToList());
            }
            if (searchPetType != String.Empty)
            {
                partialResult.AddRange(CsvFileContent.Where(
                    x => x.Gender.ToUpper().Contains(searchPetGender.ToUpper())).ToList());
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pet, CsvLine>());
            var mapper = new Mapper(config);
            res = mapper.Map<List<Pet>>(partialResult);

            return res;
        }
    }


}
