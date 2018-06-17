using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Ainthinai.Service.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ainthinai.Service.DomainRepository;

namespace Ainthinai.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Feedback")]
    public class FeedbackController : Controller
    {
        private readonly ILogger<FeedbackController> _logger;
        private readonly IFeedbackRepository _feedbackRepo;
        public FeedbackController(ILoggerFactory loggerFactory, IFeedbackRepository feedbackRepo)
        {
            _feedbackRepo = feedbackRepo;
            _logger = loggerFactory.CreateLogger<FeedbackController>();
        }

        // POST: api/Feedback
        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromBody]Feedback feedback)
        {
            try
            {
                _logger.LogInformation("FeedbackController: PostFeedback() method is being invoked");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("FeedbackController: PostFeedback() - Invalid Request");
                    return BadRequest();
                }
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Feedback, Model.Feedback>(); });
                var mapper = config.CreateMapper();
                var modelFeedback = mapper.Map<Feedback, Model.Feedback>(feedback);
                var response = await _feedbackRepo.PostFeedback(modelFeedback);
                _logger.LogInformation("FeedbackController: PostFeedback() method is successfully invoked");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("FeedbackController: PostFeedback() - Error occured while procesing the request. Error Message : {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Feedback
        [HttpGet]
        public async Task<IActionResult> GetFeedbacks()
        {
            try
            {
                _logger.LogInformation("FeedbackController: GetFeedbacks() method is being invoked");
                var modelFeedback = await _feedbackRepo.GetFeedbacks();
                _logger.LogInformation("FeedbackController: GetFeedbacks() method is successfully invoked");
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Model.Feedback, Feedback>(); });
                var mapper = config.CreateMapper();
                var vmFeedbacks = mapper.Map<IEnumerable<Model.Feedback>, IEnumerable<Feedback>>(modelFeedback);
                return Ok(vmFeedbacks);
            }
            catch (Exception ex)
            {
                _logger.LogError("FeedbackController: GetFeedbacks() - Error processing the request. Message : {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}