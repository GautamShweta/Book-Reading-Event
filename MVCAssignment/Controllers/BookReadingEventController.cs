using System.Web.Mvc;
using BL;
using MVCAssignment.Models;
using Shared;
using MVCAssignment.Helper;
using System.Collections.Generic;
using System;
using System.Linq;
namespace MVCAssignment.Controllers
    {
    public class BookReadingEventController : Controller
    {

        string UserEmail;
        public string GetUserEmail
            {
            get
                {
                if (User.Identity.IsAuthenticated)
                    {
                    UserEmail = new UserEmailBL().GetUserEmail(User.Identity.Name);
                    }
                return UserEmail;
                }
               
            }
       
        public ActionResult AllEvents()
            {
            AllEventsBL allEvents = new AllEventsBL();
            IEnumerable<Event> events = allEvents.GetEvents;
             
              return View(new EventToEventModelHelper().GetEventModels(events));
              
            }
        

        
        public ActionResult MyEvents()
            {
            string UserEmail=GetUserEmail;
            MyEventsBL getMyEventsBL = new MyEventsBL();
            var myEvents = getMyEventsBL.GetMyEvents(UserEmail);
            return View(new EventToEventModelHelper().GetEventModels(myEvents));
            }


       public ActionResult EventsInvitedTo()
            {
            string UserEmail = GetUserEmail;
            InvitedToBL invitedToBL = new InvitedToBL();
            var invitedEvents = invitedToBL.GetInvitedTo(UserEmail);
            return View(new EventToEventModelHelper().GetEventModels(invitedEvents));
            }
       

      
        [HttpGet]
        public ActionResult CreateEvent()
            {
           
            return View();
            }
        
        [ActionName("CreateEvent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEventPost([Bind(Include = "Title, Date, StartTime,Duration,OtherDetails,InviteByEmail,Location,Description")]EventModel model)
            {
                if(ModelState.IsValid)
                {
                model.UserId =User.Identity.Name;
                
                Event evt = new EventModelToEventHelper().EventModelToEventMapping(model);

                new CreateEventBL().CreateEvent(evt);

                return RedirectToAction("About","Home");
                
                }
            return View(model);

            }
        
        
       
        public ActionResult ViewEvent(int eventId)
            {
             EventBL evt = new EventBL();
            EventModel eventModel = new EventToEventModelHelper().EventToEventModelMapping(evt.GetEvent(eventId));
            if (eventModel.InviteByEmail != null)
                {
                eventModel.Count = eventModel.InviteByEmail.Split(',').Length;
                }
            else
                {
                eventModel.Count = 0;
                }
            ViewBag.DisplayDescription = (eventModel.Description != null) ? true : false;
            ViewBag.DisplayOtherDetails = (eventModel.OtherDetails != null) ? true : false;
            ViewBag.DisplayDuration = (eventModel.Duration != null) ? true : false;
            ViewBag.DisplayCount = (eventModel.Count != 0) ? true : false;
            ViewBag.DisplayEditLink = ((eventModel.Date < DateTime.Now.Date)||(eventModel.StartTime.TimeOfDay <= DateTime.Now.TimeOfDay)) ? false : true;
            
            return View(eventModel);
            }

        [HttpGet]
      
        public ActionResult EditEvent(int eventId)
            {
            EventBL eventBL = new EventBL();
            EventModel model = new EventToEventModelHelper().EventToEventModelMapping(eventBL.GetEvent(eventId));
            return View(model);
            }
        
        
        [HttpPost]
        [ActionName("EditEvent")]
        
        public ActionResult EditEventPost(EventModel eventModel)
            {
            if (ModelState.IsValid)
                {
                EditEventBL editEvent = new EditEventBL();
                editEvent.EditEvent(new EventModelToEventHelper().EventModelToEventMapping(eventModel));
                return View(eventModel);
                }
            else
                return View(RedirectToAction("About", "Home"));
            
            }

        public ActionResult DeleteEvent(int eventId)
            {
            DeleteEventBL deleteEventBL = new DeleteEventBL();
            deleteEventBL.DeleteEvent(eventId);
            return RedirectToAction("About","Home");
            }

        [HttpGet]
        public ActionResult Comments(int eventId)
            {
            CommentsBL commentsBL = new CommentsBL();
            IEnumerable<Comment> comments= commentsBL.GetComments(eventId);
            return PartialView(new CommentToCommentModelHelper().GetCommentModels(comments));
            }

     
        
        [HttpPost]
        public ActionResult AddCommentsPost([Bind(Include ="EventId,CommentAdded")]CommentModel commentModel)
            {
            commentModel.Date = DateTime.Now;
                if(ModelState.IsValid)
                {
                new AddCommentsBL().AddComment(new CommentModelToCommentHelper().CommentModelToCommentMapping(commentModel));
                }
            return RedirectToAction("ViewEvent",new { commentModel.EventId});
            }
            
    }
}