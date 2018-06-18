using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.Model
{
    public class VolunteerLog
    {
        public int Id { get; set; }
        public string VolunteerId { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public double VolunteeringHours { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
