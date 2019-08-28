using Pets.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Database
{
    public interface IDAManager
    {
        bool Insert(string AnimalType, string AnimalName, string Gender);
        List<Pet> Search(string SearchPetName, string SearchPetType, string SearchPetGender);
        bool Delete (string AnimalType, string AnimalName);
                                     
    }
}
