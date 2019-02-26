using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Shared;

using BL;

namespace MVCAssignment.Validation
    {
    public class EventValidator : ValidationAttribute

        {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
            IEnumerable<Event> events = new AllEventsBL().GetEvents;
            var propertyTitle = validationContext.ObjectType.GetProperty("Title");
           
            string title = (string)propertyTitle.GetValue(validationContext.ObjectInstance);
            
            var queryTitle = (from e in events
                                where e.Title.Equals(title,StringComparison.OrdinalIgnoreCase)
                                select e).ToList().Count;
            return queryTitle!=0? new ValidationResult("" + validationContext.DisplayName + "already exists"):  ValidationResult.Success; 

           
            }
        }
    }