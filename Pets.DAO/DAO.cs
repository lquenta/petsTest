using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.DAO
{
    public class RequestNewPet
    {
        public string AnimalType { get; set; }
        public string AnimalName { get; set; }
        public string Gender { get; set; }
    }
    public class RequestDeletePet
    {
        public string AnimalType { get; set; }
        public string AnimalName { get; set; }
    }
    public class RequestSearchPet
    {
        public string SearchPetName { get; set; }
        public string SearchPetType { get; set; }
        public string SearchPetGender { get; set; }
    }
    public class ResponseSearchPet
    {
        public List<Pet> results { get; set; }
    }
    public class ResponseGetAllPets
    {
        public List<Pet> results { get; set; }
    }
    public class Pet
    {
        public string AnimalType { get; set; }
        public string AnimalName { get; set; }
        public string Gender { get; set; }
        public string LastUpdate { get; set; }
    }
    public class PetType
    {
        public string ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

}
