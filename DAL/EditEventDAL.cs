using System;
using Shared;
using DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;

namespace DAL
    {
    public class EditEventDAL
        {
        public bool EditEvent(Event evt)
            {
            using (BookReadingContext context = new BookReadingContext())
                {

                
                context.Entry(evt).State = EntityState.Modified;

                context.SaveChanges();
                 return true;
                    
               
                }
            }
        }
    }
