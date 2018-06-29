using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ainthinai.Service.ViewModel;
using Ainthinai.Service.DomainRepository;
using Microsoft.Extensions.Logging;
using Ainthinai.Service.EnumObjects;

namespace Ainthinai.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/TaskLists")]
    public class TaskListsController : Controller
    {
        private readonly ITaskListRepository _taskRepo;
        private readonly ILogger<TaskListsController> _logger;

        public TaskListsController(ITaskListRepository repo, ILoggerFactory loggerFactory)
        {
            _taskRepo = repo;
            _logger = loggerFactory.CreateLogger<TaskListsController>();
        }

        // GET: api/TaskLists
        [HttpGet]
        public async Task<IActionResult> GetTaskList()
        {
            try
            {
                _logger.LogInformation("TaskController: GetTasks() method is being invoked");
                var taskListResponse = await _taskRepo.GetTasks();
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Model.TaskList, TaskList>(); });
                var mapper = config.CreateMapper();
                var vmTaskList = mapper.Map<IEnumerable<Model.TaskList>, IEnumerable<TaskList>>(taskListResponse);
                return Ok(vmTaskList);
            }
            catch (Exception ex)
            {
                _logger.LogError("TaskController: Error Processing GetTaskList() method. Exception : {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //return _context.TaskList;
        }

        // GET: api/TaskLists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskList([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("TaskController: GetTaskList() method is being invoked");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("TaskController: GetTaskList() - Invalid input parameters are passed.");
                    return BadRequest(ModelState);
                }
                var responseTask = await _taskRepo.GetTask(id);
                if (responseTask == null)
                {
                    _logger.LogWarning("TaskController: GetTaskList() - No task is found with the given input value.");
                    return NotFound();
                }
                _logger.LogInformation("TaskController: GetTaskList() method is successfully invoked");
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Model.TaskList, TaskList>(); });
                var mapper = config.CreateMapper();
                var vmTaskList = mapper.Map<Model.TaskList, TaskList>(responseTask);
                return Ok(vmTaskList);
            }
            catch (Exception ex)
            {
                _logger.LogError("TaskController: Error Processing GetTaskList() method. Exception : {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/TaskLists
        [HttpGet]
        [ActionName("GetTaskListSearch")]
        public async Task<IActionResult> GetTaskList([FromBody]TaskSearch searchParams)
        {
            _logger.LogInformation("TaskController: GetTaskListSearch() method is being invoked");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("TaskController: GetTaskListSearch() - Invalid input parameters are passed.");
                return BadRequest(ModelState);
            }
            try
            {
                var responseTask = await _taskRepo.GetTasks(searchParams);
                if (responseTask == null)
                {
                    _logger.LogWarning("TaskController: GetTaskListSearch() - No task is found with the given input value.");
                    return NotFound();
                }
                _logger.LogInformation("TaskController: GetTaskListSearch() method is successfully invoked");
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Model.TaskList, TaskList>(); });
                var mapper = config.CreateMapper();
                var vmTaskList = mapper.Map<IEnumerable<Model.TaskList>, IEnumerable<TaskList>>(responseTask);
                return Ok(vmTaskList);
            }
            catch (Exception ex)
            {
                _logger.LogError("TaskController: Error Processing PutTaskList() method. Exception : {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        // PUT: api/TaskLists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskList([FromRoute] int id, [FromBody] ViewModel.TaskList taskList)
        {
            try
            {
                _logger.LogInformation("TaskController: PutTaskList() method is being invoked");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("TaskController: PutTaskList() - Model State in invalid. Please check the input parameters.");
                    return BadRequest(ModelState);
                }
                if (id != taskList.Id)
                {
                    _logger.LogWarning("TaskController: PutTaskList() - Incorrect ID is passed");
                    return BadRequest();
                }

                var config = new MapperConfiguration(cfg => { cfg.CreateMap<TaskList, Model.TaskList>(); });
                var mapper = config.CreateMapper();
                var modelTask = mapper.Map<TaskList, Model.TaskList>(taskList);

                _logger.LogInformation("TaskController: PutTaskList() method is successfully invoked");
                return Ok(await _taskRepo.UpdateTask(id, modelTask));
            }
            catch (Exception ex)
            {
                _logger.LogError("TaskController: Error Processing PutTaskList() method. Exception : {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            

            //_context.Entry(taskList).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TaskListExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
        }

        // POST: api/TaskLists
        [HttpPost]
        public async Task<IActionResult> PostTaskList([FromBody] ViewModel.TaskList taskList)
        {
            try
            {
                _logger.LogInformation("TaskController: PostTaskList() method is being invoked");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("TaskController: PostTaskList() - Invalid input parameters are passed.");
                    return BadRequest(ModelState);
                }
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<TaskList, Model.TaskList>(); });
                var mapper = config.CreateMapper();
                var modelTask = mapper.Map<TaskList, Model.TaskList>(taskList);
                var postResponse = await _taskRepo.CreateTask(modelTask);
                _logger.LogInformation("TaskController: PostTaskList() method is successfully invoked.");
                return Ok(postResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError("TaskController: Error Processing PutTaskList() method. Exception : {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //_context.TaskList.Add(taskList);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTaskList", new { id = taskList.Id }, taskList);
        }

        // DELETE: api/TaskLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskList([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("TaskController: DeleteTaskList() method is being invoked");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("TaskController: DeleteTaskList() - Invalid input parameters are passed.");
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("TaskController: DeleteTaskList() method is successfully invoked");
                return Ok(await _taskRepo.DeleteTask(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("TaskController: Error Processing PutTaskList() method. Exception : {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

           

            //var taskList = await _context.TaskList.SingleOrDefaultAsync(m => m.Id == id);
            //if (taskList == null)
            //{
            //    return NotFound();
            //}

            //_context.TaskList.Remove(taskList);
            //await _context.SaveChangesAsync();

            //return Ok(taskList);
        }

        //private bool TaskListExists(int id)
        //{
        //    return _context.TaskList.Any(e => e.Id == id);
        //}
    }
}