using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Team3Assig.Controllers
{
    public class DiplomataControllerSettings : IDiplomataControllerSettings
    {   

        public DiplomataControllerSettings(IConfiguration conf)
        {
            ApiKey = conf["CoreApi:ApiKey"];

        }

        public string ApiKey { get; }
    }
}
