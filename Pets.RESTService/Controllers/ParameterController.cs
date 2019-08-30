using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pets.RESTService.Controllers
{
    public class Gender
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

    public class ParameterController : ApiController
    {
        public IEnumerable<Gender> Get()
        {
            List<Gender> res = new List<Gender>();
            res.Add(new Gender() { ID = "M", Name = "MALE" });
            res.Add(new Gender() { ID = "F", Name = "FEMALE" });
            return res;
        }
    }
}
