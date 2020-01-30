using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Web.Middleware
{
    public class MeasureRequestExecutionTime
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MeasureRequestExecutionTime> _logger;

        public MeasureRequestExecutionTime(RequestDelegate next, ILogger<MeasureRequestExecutionTime> logger
        )
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (await ShouldMeasureRequests(httpContext))
            {
                var requestStartDate = DateTimeOffset.Now;
                httpContext.Items["RequestStart"] = requestStartDate;
                _logger.LogInformation($"Request started at {requestStartDate}");
                // TODO: Should use httpContext.Response.OnStarting to be more precise about when the response was started
                await _next(httpContext);

                var requestEndDate = DateTimeOffset.Now;
                var durationMs = (requestEndDate - requestStartDate).TotalMilliseconds;
                _logger.LogInformation($"Request ended at {requestEndDate} took {durationMs}ms");
            }
            else
            {
                await _next(httpContext);
            }


        }

        private Task<bool> ShouldMeasureRequests(HttpContext httpContext)
        {
            // TODO: Check relevant conditions to assess
            // Request path (eg. httpContext.Request.Path.StartsWithSegments(new PathString("/api"))
            // Request content-type (eg. httpContext.Request.ContentType == "image/png")
            return Task.FromResult(true);
        }
    }
}