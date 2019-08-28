using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pets.DAO;

namespace Pets.Database
{
    class DAManager : IDAManager
    {
        public bool Delete(string AnimalType, string AnimalName)
        {
            throw new NotImplementedException();
        }

        public bool Insert(string AnimalType, string AnimalName, string Gender)
        {
            throw new NotImplementedException();
        }

        public List<Pet> Search(string SearchPetName, string SearchPetType, string SearchPetGender)
        {
            throw new NotImplementedException();
        }
    }
}
