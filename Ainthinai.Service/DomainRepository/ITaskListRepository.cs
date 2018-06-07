using Ainthinai.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.DomainRepository
{
    interface ITaskListRepository
    {
        IEnumerable<TaskList> GetEvents();
        TaskList GetEvent(int taskId);
        TaskList CreateEvent(TaskList task);
        bool UpdateEvent(int taskId, TaskList task);
        bool DeleteEvent(int taskId);
    }
}
