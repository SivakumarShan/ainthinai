using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ainthinai.Service.Model;
using Ainthinai.Service.Repository;

namespace Ainthinai.Service.DomainRepository
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly IRepository<TaskList> _taskRepo;
        public TaskListRepository(IRepository<TaskList> repo)
        {
            _taskRepo = repo;
        }
        public TaskList CreateEvent(TaskList task)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEvent(int taskId)
        {
            throw new NotImplementedException();
        }

        public TaskList GetEvent(int taskId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskList> GetEvents()
        {
            throw new NotImplementedException();
        }

        public bool UpdateEvent(int taskId, TaskList task)
        {
            throw new NotImplementedException();
        }
    }
}
