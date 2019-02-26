
using Shared;
using DAL;
using System.Collections.Generic;
using System.Linq;
namespace BL
    {
    public class CreateEventBL
        {
        public void CreateEvent(Event evt)
            {

            IEnumerable<Event> events = new AllEventsDAL().GetEvents;
            List<Invitation> invitations = new List<Invitation>();

            if (evt.InviteByEmail != null)
                {
                var invited = evt.InviteByEmail.Split(',');
                Invitation invitation = new Invitation();
                if (invited != null)
                    {


                    foreach (var item in invited)
                        {
                        invitation.Email = item;
                        invitation.EventId = evt.EventId;
                        }

                    invitations.Add(invitation);
                    }
                }
                new CreateEventDAL().CreateEvent(evt, invitations);
                
            
            }
        }
    }

