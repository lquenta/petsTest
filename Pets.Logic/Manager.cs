using Pets.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Logic
{
    public class Manager
    {
        public bool CreatePet(RequestNewPet request)
        {
            return true;
        }
        public bool DeletePet(RequestDeletePet request)
        {
            return true;
        }
        public ResponseSearchPet SearchPet(RequestSearchPet request)
        {
            ResponseSearchPet response = new ResponseSearchPet();
            return response;
        }

    }
}
