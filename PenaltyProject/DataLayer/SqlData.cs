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
        //To make connection to database

        string conString = "";
        public SqlData(IConfiguration config)
        {
            conString = config.GetConnectionString("Connection");
        }
        
        //This method will give away the list of countries Data
        public List<Country> GetCountries()
        {

            List<Country> countriesList = new List<Country>();//Initializing
            SqlConnection con = new SqlConnection(conString);//conString is given in appsettings.json file
            con.Open();
            string query = "SELECT * FROM COUNTRYDATA";//sql query to get all data
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable countriesTable = new DataTable();//Initialing new DataTable to store Data
            sda.Fill(countriesTable);


            for (int index = 0; index < countriesTable.Rows.Count; index++)
            {
                Country country = new Country             //Making new Object of Country and Passing Data
                {
                    countryId = Convert.ToInt32(countriesTable.Rows[index]["CountryID"]),
                    countryName = countriesTable.Rows[index]["CountryName"].ToString(),
                    countrycurrency = countriesTable.Rows[index]["CountryCurrency"].ToString(),
                    penalty = Convert.ToInt32(countriesTable.Rows[index]["PenaltyPerDay"]),
                    tax = Convert.ToInt32(countriesTable.Rows[index]["TaxPerDay"])
                };
                countriesList.Add(country);
            }

            con.Close();
            return countriesList;
        }


    }
}
