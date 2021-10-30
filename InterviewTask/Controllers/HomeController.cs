using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using InterviewTask.Models;
using InterviewTask.Services;
using System.Linq;

namespace InterviewTask.Controllers
{
    public class HomeController : Controller
    {
        /*
         * Prepare your opening times here using the provided HelperServiceRepository class.       
         */

        public ActionResult Index()
        {

            IList<HelperServiceModel> wrapperModel = new List<HelperServiceModel>();

            wrapperModel= HelperServiceFactory.Create();
            //check time
            DateTime startDateTime;
            DateTime endDateTime;
            DateTime currentdatetime =  DateTime.Now;
            DayOfWeek weekday = currentdatetime.DayOfWeek;
            int weekdaynum = (int)weekday;
            foreach (var item in wrapperModel)
            {
                var currentOpeningTimes = new List<int> { };

                List<List<int>> TimeList = new List<List<int>>();
                TimeList.Add(item.SundayOpeningHours);
                TimeList.Add(item.MondayOpeningHours);
                TimeList.Add(item.TuesdayOpeningHours);
                TimeList.Add(item.WednesdayOpeningHours);
                TimeList.Add(item.ThursdayOpeningHours);
                TimeList.Add(item.FridayOpeningHours);
                TimeList.Add(item.SaturdayOpeningHours);
                
                string[]  weekdays = new string[]{ "Sunday", "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"};
                currentOpeningTimes = TimeList[weekdaynum];
                
                string openhour = currentOpeningTimes.ElementAt(0) + ":00";
                string closehour = currentOpeningTimes.ElementAt(1) + ":00";

                startDateTime = DateTime.Parse(openhour);
                endDateTime = DateTime.Parse(closehour);

                if (TimeSpan.Compare(currentdatetime.TimeOfDay, startDateTime.TimeOfDay) == 1 && TimeSpan.Compare(endDateTime.TimeOfDay, currentdatetime.TimeOfDay) == 1)
                {
                    item.CurrentOpening = true;
                    item.OpeningInfo = string.Format("OPEN - OPEN TODAY UNTIL {0}", closehour); 

                }
                else
                {
                    item.CurrentOpening = false;
                    string nextopenweekday = "";
                    string nextopenhour = "";
                    int startweekcheck = 0;
                    bool checkdone = false;
                    if (weekdaynum <= 6 && weekdaynum >= 0)
                        startweekcheck = weekdaynum;
                    for (int i = startweekcheck; i < 7; i++)
                    {
                        if(TimeList[i].ElementAt(0) > 0)
                        {
                            nextopenweekday = weekdays[i].ToString();
                            nextopenhour = TimeList[i].ElementAt(0) + ":00";
                            checkdone = true;
                            break;
                        }
                    }
                    if (checkdone == false)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            if (TimeList[i].ElementAt(0) > 0)
                            {
                                nextopenweekday = weekdays[i].ToString();
                                nextopenhour = TimeList[i].ElementAt(0) + ":00";
                                checkdone = true;
                                break;
                            }
                        }
                    }
                    item.OpeningInfo = string.Format("CLOSED - REOPENS {0} at {1}", nextopenweekday, nextopenhour);
                }

            }

            return View(wrapperModel);
            //return View();
        }

        public ActionResult Weather(string id)
        {
            ViewBag.Getcity = id;
            ViewBag.Getapikey = "9e5fa282f840f83726583af5d54be316";

            return View();
        }
    }
}