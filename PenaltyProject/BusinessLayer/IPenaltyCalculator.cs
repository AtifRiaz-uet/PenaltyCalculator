using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PenaltyProject.BusinessLayer;
using PenaltyProject.Models;
using PenaltyProject.DataLayer;

namespace PenaltyProject.BusinessLayer
{
    public interface IPenaltyCalculator
    {
        public List<Country> GetCountries();
    }
}
