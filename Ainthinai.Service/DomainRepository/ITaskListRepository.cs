using Ainthinai.Service.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ainthinai.Service.DomainRepository
{
    public interface ITaskListRepository
    {
        Task<IEnumerable<TaskList>> GetTasks();
        Task<TaskList> GetTask(int taskId);
        Task<TaskList> CreateTask(TaskList task);
        Task<bool> UpdateTask(int taskId, TaskList task);
        Task<bool> DeleteTask(int taskId);
    }

    public interface ITaskListRepository<TContext> : ITaskListRepository where TContext : DbContext
    {
        TContext Context { get; }
    }
}
