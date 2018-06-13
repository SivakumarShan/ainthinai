﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ainthinai.Service.Model;
using Microsoft.Extensions.Logging;
using Ainthinai.Service.DomainRepository;

namespace Ainthinai.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Events")]
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EventController>();
            _eventRepository = eventRepository;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<IEnumerable<Event>> GetEvent()
        {
            try
            {
                _logger.LogInformation("EventController: GetEvents() method is being invoked");
                var response = await _eventRepository.GetEvents();
                _logger.LogInformation("EventController: GetEvents() method Successfully invoked");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("EventController: Error Processing GetEvent() method. Exception : {0}", ex.Message);
                return null;
            }
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("EventController: GetEvent() by ID is being invoked");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("EventController: Invalid request is passed");
                    return BadRequest(ModelState);
                }

                var @event = await _eventRepository.GetEvent(id);

                if (@event == null)
                {
                    _logger.LogInformation("EventController: Event ID doesn't exist in the records");
                    return NotFound();
                }
                _logger.LogInformation("EventController: GetEvent() by ID is successfully invoked");

                return Ok(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError("EventController: Error Processing GetEvent() by ID method. Exception : {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] Event @event)
        {
            try
            {
                _logger.LogInformation("EventController: PutEvent() by ID is being invoked");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("EventController: Invalid request is passed");
                    return BadRequest(ModelState);
                }

                if (id != @event.Id)
                {
                    _logger.LogWarning("EventController: EventID is not correct");
                    return BadRequest();
                }

                var result = await _eventRepository.UpdateEvent(id, @event);
                _logger.LogInformation("EventController: PutEvent() by ID is successfully invoked");
                return result == true ? Ok(@event) : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("EventController: Erro processing PutEvent() method. Exception: {0}", ex.Message);
                return null;
            }
        }

        // POST: api/Events
        [HttpPost]
        public async Task<IActionResult> PostEvent([FromBody] Event @event)
        {
            try
            {
                _logger.LogInformation("EventController.PostEvent(): Method is being invoked");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("EventController.PostEvent(): Invalid request is passed.");
                    return BadRequest(ModelState);
                }
                @event = await _eventRepository.CreateEvent(@event);
                _logger.LogInformation("EventController.PostEvent(): Method is successfully invoked");
                return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
            }
            catch (Exception ex)
            {
                _logger.LogError("EventController.PostEvent(): Error processing the method. Exception: {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            try
            {
                _logger.LogInformation("EventController.DeleteEvent(): Method is being invoked");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("EventController.DeleteEvent(): Invalid request is passed.");
                    return BadRequest(ModelState);
                }

                var @event = await _eventRepository.DeleteEvent(id);
                _logger.LogInformation("EventController.DeleteEvent(): Method is successfully invoked");
                return Ok(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError("EventController.DeleteEvent(): Error processing the method. Exception: {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}