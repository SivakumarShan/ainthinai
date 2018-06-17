using System;
using System.Collections.Generic;
using System.Linq;
using Ainthinai.Service.Model;
using System.Threading.Tasks;
using Ainthinai.Service.Repository;
using Microsoft.EntityFrameworkCore;

namespace Ainthinai.Service.DomainRepository
{
    public class TaskListRepository<TContext> : ITaskListRepository<TContext>, ITaskListRepository where TContext : DbContext
    {
        private readonly IRepository<TaskList> _taskRepo;

        public TaskListRepository(TContext context)
        {
            Context = context;
            _taskRepo = new Repository<TaskList>(Context);
        }

        public TContext Context { get; }

        public async Task<TaskList> CreateTask(TaskList task)
        {
            await _taskRepo.Add(task);
            return task;
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var existingTask = Context.Find<TaskList>(taskId);
            if (existingTask != null)
            {
                await _taskRepo.Delete(existingTask);
                return true;
            }
            return false;
        }

        public async Task<TaskList> GetTask(int taskId)
        {
            return await _taskRepo.Get(taskId);
        }

        public async Task<IEnumerable<TaskList>> GetTasks()
        {
            return await _taskRepo.Get();
        }

        public async Task<bool> UpdateTask(int taskId, TaskList task)
        {
            if (taskId == task.Id)
            {
                await _taskRepo.Update(task);
                return true;
            }
            return false;
        }
    }
}
