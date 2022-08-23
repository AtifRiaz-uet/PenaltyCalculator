using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using PenaltyProject.Models;
using System.Data;

namespace PenaltyProject.DataLayer
{
    public class SqlData : ISqlData
    {
        public List<Country> countriesList = new List<Country>();
        string connection = "";
        public void SqlDataHelper(IConfiguration config)
        {
            connection = config.GetConnectionString("Connection");
        }
        
        public List<Country> GetCountries()
        {
            SqlConnection con = new
            SqlConnection(connection);
            con.Open();
            string query = "SELECT * FROM CountryData";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable countriesTable = new DataTable();
            sda.Fill(countriesTable);

            for (int index = 0; index < countriesTable.Rows.Count; index++)
            {
                Country countries = new Country();
                countries.countryId = Convert.ToInt32(countriesTable.Rows[index]["CountryID"]);
                countries.countryName = countriesTable.Rows[index]["CountryName"].ToString();
                countries.countrycurrency = countriesTable.Rows[index]["CountryCurrency"].ToString();
                countries.penalty = Convert.ToInt32(countriesTable.Rows[index]["PenaltyPerDay"]);
                countries.tax = Convert.ToInt32(countriesTable.Rows[index]["TaxPerDay"]);
                countriesList.Add(countries);
            }

            con.Close();
            return (countriesList);
        }


    }
}
