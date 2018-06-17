using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.Model
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
