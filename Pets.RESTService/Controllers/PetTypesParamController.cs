using Pets.DAO;
using Pets.Logic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pets.RESTService.Controllers
{
    public class PetTypesParamController : ApiController
    {
        public IEnumerable<PetType> Get()
        {
            List<PetType> res = new List<PetType>();
            string csvFilePath = ConfigurationManager.AppSettings["CSVFilePath"].ToString();
            Manager manager = new Manager(csvFilePath);
            res=manager.ReturnAllPetTypes();
            return res;
        }
    }
}
