using Ainthinai.Service.EnumObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.ViewModel
{
    public class TaskList
    {
        public int Id { get; set; }
        public EventType EventType { get; set; }
        public string TaskName { get; set; }
        public string TaskDetails { get; set; }
        public EnumObjects.TaskStatus TaskStatus { get; set; }
        public string Remarks { get; set; }
    }
}
