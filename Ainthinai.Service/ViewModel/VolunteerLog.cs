using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.ViewModel
{
    public class VolunteerLog
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public double VolunteeringHours { get; set; }

    }
}
