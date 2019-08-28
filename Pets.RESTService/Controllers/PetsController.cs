using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pets.Logic;
using Pets.DAO;
using System.Configuration;

namespace Pets.RESTService.Controllers
{
    public class PetsController : ApiController
    {
        // GET api/values/5
        public ResponseSearchPet Get(string name="",string typeSearch="",string gender="")
        {
            ResponseSearchPet res = new ResponseSearchPet();
            string csvFilePath= ConfigurationManager.AppSettings["CSVFilePath"].ToString();
            Manager manager = new Manager(csvFilePath);
            RequestSearchPet request = new RequestSearchPet();
            if (request.SearchPetGender != "") {
                switch (request.SearchPetGender.ToUpper())
                {
                    case "MALE":
                        request.SearchPetGender="M";
                        break;
                    case "FEMALE":
                        request.SearchPetGender = "F";
                        break;
                    default:
                        throw new Exception("Only allowed values are MALE or FEMALE");
                        
                }
            }
            if(request.SearchPetName != "")
            {
                request.SearchPetName = name.Trim();
            }
            if(request.SearchPetType != "")
            {
                request.SearchPetType = typeSearch.Trim().ToUpper();
            }

            res = manager.SearchPet(request);

            return res;
        }

        // POST api/values
        public bool Post(RequestNewPet request ) //Add new Pet
        {
            string csvFilePath = ConfigurationManager.AppSettings["CSVFilePath"].ToString();
            Manager manager = new Manager(csvFilePath);
            return manager.CreatePet(request);
             
        }

        

        // DELETE api/values/5
        public bool Delete(RequestDeletePet request)
        {
            string csvFilePath = ConfigurationManager.AppSettings["CSVFilePath"].ToString();
            Manager manager = new Manager(csvFilePath);
            return manager.DeletePet(request);
        }
    }
}
