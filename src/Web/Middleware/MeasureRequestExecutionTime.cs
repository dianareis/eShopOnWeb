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
        ) {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            if(await ShouldMeasureRequests(httpContext)) {
                var requestStartDate = DateTimeOffset.Now;
            _logger.LogInformation($"Request started at {requestStartDate}");
            await _next(httpContext);

            var requestEndDate = DateTimeOffset.Now;
            var durationMs = (requestEndDate - requestStartDate).TotalMilliseconds;
            _logger.LogInformation($"Request ended at {requestEndDate} took {durationMs}ms");
            } else {
                await _next(httpContext);
            }
            

        }

        private Task<bool> ShouldMeasureRequests(HttpContext httpContext)
        {
            return Task.FromResult(httpContext.Request.Path.StartsWithSegments(new PathString("/api"))
                || httpContext.Request.ContentType == "image/png"
            );
        }
    }
}