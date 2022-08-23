using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PenaltyProject.Models;

namespace PenaltyProject.DataLayer
{
    interface ISqlData
    {
        public void SqlDataHelper(IConfiguration config);
        public List<Country> GetCountries();
    }
}
