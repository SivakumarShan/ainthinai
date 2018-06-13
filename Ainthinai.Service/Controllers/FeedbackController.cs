using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ainthinai.Service.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ainthinai.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/Feedback")]
    public class FeedbackController : Controller
    {
        private readonly ILogger<FeedbackController> _logger;
        public FeedbackController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FeedbackController>();
        }

        // POST: api/Feedback
        [HttpPost]
        public async Task<bool> PostFeedback([FromBody]Feedback feedback)
        {

            return await Task.FromResult<bool>(false);
        }

    }
}