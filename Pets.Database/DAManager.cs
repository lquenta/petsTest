﻿using System;
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
    public class DAManager : IDAManager
    {
        public string CsvFilePath = "";
        List<CsvLine> CsvFileContent = new List<CsvLine>();
        public DAManager(string csvFilePath)
        {
            CsvFilePath = csvFilePath;
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.Delimiter = ",";
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
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Configuration.Delimiter = ",";
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
            newCsvLine.LastUpdate = DateTime.Now.ToString("yyyyMMddHHmmss");  //<year><month><day><hour><minute><second>
            CsvFileContent.Add(newCsvLine);
            using (var writer = new StreamWriter(CsvFilePath))
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.Delimiter = ",";
                csv.WriteRecords(CsvFileContent);
            }
            return true;
        }

        public List<Pet> Search(string searchPetName="", string searchPetType="", string searchPetGender="")
        {
            List<Pet> res = new List<Pet>();
            var partialResult= CsvFileContent;
            if (CsvFileContent.Count == 0)
            {
                return res;
            }
            if (searchPetName != String.Empty)
            {
                partialResult = partialResult.Where(
                    x => x.AnimalName.ToUpper().Contains(searchPetName.ToUpper())).OrderBy(x => x.AnimalName).ToList(); ;
            }
            if (searchPetType != String.Empty)
            {
                partialResult=(partialResult.Where(
                    x => x.AnimalType.ToUpper().Contains(searchPetType.ToUpper())).OrderByDescending(x=>DateTime.ParseExact(x.LastUpdate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)).ToList());
            }
            if (searchPetGender != String.Empty)
            {
                partialResult=(partialResult.Where(
                    x => x.Gender.ToUpper().Contains(searchPetGender.ToUpper())).OrderByDescending(x => DateTime.ParseExact(x.LastUpdate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)).ToList());
            }
            List<Pet> listPets = new List<Pet>();
            foreach (var row in partialResult)
            {
                listPets.Add(new Pet() { AnimalName = row.AnimalName, Gender = row.Gender, AnimalType = row.AnimalType ,LastUpdate=row.LastUpdate});
            }
            res = listPets;
            return res;
        }

       

        List<Pet> IDAManager.GetAllPets()
        {
            List<Pet> listPets = new List<Pet>();
            foreach (var row in CsvFileContent)
            {
                listPets.Add(new Pet() { AnimalName = row.AnimalName, Gender = row.Gender, AnimalType = row.AnimalType, LastUpdate = row.LastUpdate });
            }
            
            return listPets;
        }

        List<PetType> IDAManager.ReturnAllPetTypes()
        {
            List<PetType> petTypes = new List<PetType>();
            var data = from v in CsvFileContent
                       select new
                       {
                           ID = v.AnimalType,
                           Name = v.AnimalType
                       };

            data = data.Distinct();
            foreach (var typeRow in data)
            {
                petTypes.Add(new PetType() { ID = typeRow.ID, Name = typeRow.Name });

            }
            return petTypes;
        }
    }


}
