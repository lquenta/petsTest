﻿using Pets.DAO;
using Pets.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Logic
{
    public class Manager
    {
        IDAManager DAManager;
        public Manager(string csvFilePath)
        {
            IDAManager dAManager = new Database.DAManager(csvFilePath);
            DAManager = dAManager;

        }

        public bool CreatePet(RequestNewPet request)
        {
            DAManager.Insert(request.AnimalType, request.AnimalName, request.Gender);
            return true;
        }
        public bool DeletePet(RequestDeletePet request)
        {
            DAManager.Delete(request.AnimalType, request.AnimalName);
            return true;
        }
        public ResponseSearchPet SearchPet(RequestSearchPet request)
        {
            ResponseSearchPet response = new ResponseSearchPet();
            request.SearchPetGender = (request.SearchPetGender == null) ? "" : request.SearchPetGender;
            request.SearchPetType = (request.SearchPetType == null) ? "" : request.SearchPetType;
            request.SearchPetName = (request.SearchPetName == null) ? "" : request.SearchPetName;
            response.results= DAManager.Search(request.SearchPetName, request.SearchPetType, request.SearchPetGender);
            return response;
        }
        public ResponseSearchPet ReturnAllPets()
        {
            ResponseSearchPet response = new ResponseSearchPet();
            response.results = DAManager.GetAllPets();
            return response;
        }
        public List<PetType> ReturnAllPetTypes()
        {
            List<PetType> res = new List<PetType>();
            res = DAManager.ReturnAllPetTypes();
            return res;
        }

    }
}
