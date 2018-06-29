using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.Model
{
    public class TaskListLog
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Activity { get; set; }
        public string OriginalValue { get; set; }
        public string NewValue { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
