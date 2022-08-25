using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PenaltyProject.DataLayer;
using PenaltyProject.Models;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;

namespace PenaltyProject.BusinessLayer
{
    public class PenaltyCalculator :IPenaltyCalculator
    {
        ISqlData _content;

        public PenaltyCalculator(ISqlData content)
        {
            _content = content;
        }

        //Calling the GetCountries() from DataLayer/SqlData
        public List<Country> GetCountries()
        {
            return _content.GetCountries();
        }
        //This function will return list of double one will be data and other is penalty Price
        public List<string> GetDays(datesModel dates)
        {
            string CalendarID;
            List<string> dayPenaltyList;

            if (dates.countryName == "UAE")
            {
                CalendarID = "en-gb.ae.official#holiday@group.v.calendar.google.com";  //Google API to get Holidays for UAE Country
            }
            else
            {
                CalendarID = "en-gb.pk#holiday@group.v.calendar.google.com";  //Google API to get Holidays for Pakistan Country
            }
            List<SpecialDays> specialDates = GetSpecialDays(CalendarID);//Calling the function GetSpeicalDays() to get Holidays Dates for selected dates and storing in the list specialDates.
            int day = 1;

            while ((dates.checkIn).Date != (dates.checkOut).Date)
            {
                SpecialDays newdays = new SpecialDays();
                newdays.date = dates.checkIn;

                if ((dates.checkIn).DayOfWeek.ToString() == "Saturday" || (dates.checkIn).DayOfWeek.ToString() == "Sunday" || specialDates.Contains(newdays))
                    //Checking if Day of Week is Saturday or Sunday if it is day will not increase and also checking the dates if it is in specialdays dates list. 
                {
                    // Do Nothing
                }
                else
                {
                    Console.WriteLine($"{(dates.checkIn).Date.ToString("yyyy-MM-dd")}:{(dates.checkIn).DayOfWeek.ToString()}");//Just Checking
                    day++;
                }
                (dates.checkIn) = (dates.checkIn).AddDays(1);
            }
            if (day>10)
                // If total business days is greater than 10 then calling function to Calulate Penalty if not assigning 0 as Penalty.
            {
                string penalty = CalculatePenalty(day-10, dates.countryName);
                dayPenaltyList = new List<string> { $"{day}", penalty };
            }
            else
            {
                string penalty = "NIL";
                dayPenaltyList = new List<string> { $"{day}", penalty };
            }
            
            return dayPenaltyList;
        }

        private List<SpecialDays> GetSpecialDays(string CalendarID)//This function will return the list of dates of specildays by calling Google Calendar API
        {
            List<SpecialDays> specialDaysList = new List<SpecialDays>();
            const string apiKey = "AIzaSyCkHEq9efc73mgl0k3Ib7wwI54Gle5hX3M"; //This is self generated key to get dates for Holidays of Countries.
            string calendar = CalendarID;


            static async Task Main(string[] args, List<SpecialDays> specialDaysList,string  CalendarID)
            {
                Console.WriteLine("Just Checking");

                var service = new CalendarService(new BaseClientService.Initializer()//Initialing new object to make the request
                {
                    ApiKey = apiKey,
                    ApplicationName = "Penalty Calculator"
                });
                var request = service.Events.List(CalendarID); //Here Request to get Dates is passing
                request.Fields = "items(summary,start,end)";
                var response = await request.ExecuteAsync();
                foreach (var item in response.Items)
                {
                    Console.WriteLine($"Holiday: {item.Summary} start: {item.Start.Date} End: {item.End.Date}");

                    SpecialDays days = new SpecialDays();
                    days.date = DateTime.Parse(item.Start.Date); //Storing SpecialDay dates
                    specialDaysList.Add(days);

                }
                Console.ReadLine();


            }
            return specialDaysList;//Returning List of SpecialDays Dates
        }

        private string CalculatePenalty(int days,string country)
        {
            List<Country> countries = _content.GetCountries();
            
            if (country =="UAE")
            {
                double Penalty = (countries[1].penalty);//Getting Penalty from DataBase for country UAE
                string cur = (countries[1].countrycurrency);
                double totalPenalty = days * Penalty;
                double tax = ((countries[1].tax)/100) * totalPenalty;
                return $"{ totalPenalty} {cur} ";
            }
            else
            {
                double Penalty = (countries[0].penalty); //Getting Penalty from DataBase for country Pakistan
                string cur = (countries[0].countrycurrency);
                double totalPenalty = days * Penalty;
                return $"{ totalPenalty} {cur} ";
            }
        }
    }


}
