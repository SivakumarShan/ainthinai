using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.ViewModel
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
        public int VolunteersCount { get; set; }
        public string Organizers { get; set; }
        public string ContactDetails { get; set; }
        // TODO: Add Event Photo
        public string TicketUrl { get; set; }
        public string GoogleMap { get; set; }
    }
}
