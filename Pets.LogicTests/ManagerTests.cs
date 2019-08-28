using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pets.DAO;
using Pets.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.Logic.Tests
{
    [TestClass()]
    public class ManagerTests
    {
        [TestMethod()]
        public void CreatePetTest()
        {
            Pets.Logic.Manager manager = new Manager(@"c:\pets\test.csv");
            RequestNewPet request = new RequestNewPet();
            request.AnimalName = "DANTE";
            request.AnimalType = "CAT";
            request.Gender = "M";
            bool success=manager.CreatePet(request);
            Assert.IsTrue(success);
        }

        [TestMethod()]
        public void SearchPetTest()
        {
            Pets.Logic.Manager manager = new Manager(@"c:\pets\test.csv");
            RequestNewPet requestNewPet = new RequestNewPet();
            requestNewPet.AnimalName = "DANTELION";
            requestNewPet.AnimalType = "DOG";
            requestNewPet.Gender = "M";
            bool success = manager.CreatePet(requestNewPet);

            RequestSearchPet request = new RequestSearchPet();
            //request.SearchPetName = "DANTE";
            //request.SearchPetType = "CAT";
            request.SearchPetGender = "M";
            var result = manager.SearchPet(request);
            Assert.IsNotNull(result);

        }
        [TestMethod()]
        public void DeletePetTest()
        {
            Pets.Logic.Manager manager = new Manager(@"c:\pets\test.csv");
            RequestDeletePet request = new RequestDeletePet();
            request.AnimalName = "DANTE";
            request.AnimalType = "CAT";
            bool success = manager.DeletePet(request);
            Assert.IsTrue(success);
        }

    }
}