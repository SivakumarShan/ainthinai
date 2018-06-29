using System;
using System.Collections.Generic;
using System.Linq;
using Ainthinai.Service.Model;
using System.Threading.Tasks;
using Ainthinai.Service.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<TaskList>> GetTasks(ViewModel.TaskSearch searchParams)
        {
            Expression<Func<TaskList, bool>> where = c => c.EventType == searchParams.EventType;
            if (searchParams != null)
            {
                where = c => c.TaskStatus == searchParams.TaskStatus;

                if (searchParams.EventType != EnumObjects.EventType.All)
                    where = c => c.EventType == searchParams.EventType;
                if (!string.IsNullOrEmpty(searchParams.TaskName))
                    where = c => c.TaskName == searchParams.TaskName;
                if (!string.IsNullOrEmpty(searchParams.CreatedBy))
                    where = c => c.CreatedBy == searchParams.CreatedBy;
            }
            return await _taskRepo.Get(where);
        }

        public async Task<bool> UpdateTask(int taskId, TaskList task)
        {
            if (taskId == task.Id)
            {
                Context.Entry<TaskList>(task).Property(x => x.CreatedOn).IsModified = false;
                Context.Entry<TaskList>(task).Property(x => x.CreatedBy).IsModified = false;
                Context.Entry<TaskList>(task).Property(x => x.Id).IsModified = false;
                await _taskRepo.Update(task);
                return true;
            }
            return false;
        }
    }
}
