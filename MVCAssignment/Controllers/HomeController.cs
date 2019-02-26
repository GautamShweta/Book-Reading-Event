
using System.Collections.Generic;
using System.Web.Mvc;
using BL;
using Shared;
using System.Linq;
using System;
using MVCAssignment.Models;
using MVCAssignment.Helper;

namespace MVCAssignment.Controllers
    {

    public class HomeController : Controller
        {

        public ActionResult About()
            {

            IEnumerable<Event> events = new AllEventsBL().GetEvents;
            IEnumerable<Event> missedEvents;
            if (User.Identity.IsAuthenticated)
                {
                missedEvents = events.Where(e => (e.Date < DateTime.Now.Date) || ((e.Date == DateTime.Now.Date) && (e.Date.TimeOfDay < DateTime.Now.TimeOfDay)));
               }
            else
                {
                missedEvents = events.Where(e => (e.Date < DateTime.Now.Date) || ((e.Date == DateTime.Now.Date) && (e.Date.TimeOfDay < DateTime.Now.TimeOfDay)));
                missedEvents = missedEvents.Where(e => e.Type == EventType.PUBLIC);
               
                }



            IEnumerable<Event> upcomingEvents;
            if (User.Identity.IsAuthenticated)
                {
                    upcomingEvents= events.Where(e => (e.Date > DateTime.Now.Date) || ((e.Date == DateTime.Now.Date) && (e.Date.TimeOfDay > DateTime.Now.TimeOfDay)));
                }
            else
                {
                upcomingEvents = events.Where(e => (e.Date > DateTime.Now.Date) || ((e.Date == DateTime.Now.Date) && (e.Date.TimeOfDay > DateTime.Now.TimeOfDay)));
                upcomingEvents = upcomingEvents.Where(e => e.Type == EventType.PUBLIC);
               
                }
            List<IEnumerable<EventModel>> eventModels = new List<IEnumerable<EventModel>>();
            eventModels.Add(new EventToEventModelHelper().GetEventModels(missedEvents));
            eventModels.Add(new EventToEventModelHelper().GetEventModels(upcomingEvents));

            return View(eventModels);
            }


        }
    }