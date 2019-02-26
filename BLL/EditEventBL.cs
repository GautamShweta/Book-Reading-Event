using Shared;
using DAL;

namespace BL
    {
    public class EditEventBL
        {
        public bool EditEvent(Event evt )
            {
            EditEventDAL editEvent = new EditEventDAL();
            if (editEvent.EditEvent(evt))
                return true;
            else
                return false;
            
            }

        }
    }
