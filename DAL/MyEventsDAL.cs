using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace DAL
    {
    public class MyEventsDAL
        {
        public IEnumerable<Event> GetMyEvents(string userEmail)
            {


            using (BookReadingContext db = new BookReadingContext())
                {


                User user = (from u in db.Users
                             where u.EmailId == userEmail
                             select u).ToList().First();

                return user.Events;
                }

            }


        }
    }
