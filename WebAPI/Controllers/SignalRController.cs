using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Hubs;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {
        private readonly IHubContext<DashboardHub> _hubContext;
        private readonly IDashboardHubService _dashboardHubService;

        public SignalRController(IHubContext<DashboardHub> hubContext , IDashboardHubService dashboardHubService)
        {
            _hubContext = hubContext;
            _dashboardHubService = dashboardHubService;
        }

        [HttpGet("push")]
        public async Task<IActionResult>  Push()
        {
            Console.WriteLine("CONTROLLER ÇALIŞTI");
         

            var data = new
            {
                 message = "SignalR çalışıyor",
                 time = DateTime.Now,
            };

            Console.WriteLine("SENDASYNC BİTTİ");

            await _hubContext.Clients.All.SendAsync("dashboard:update", data);
            return Ok(data);
        }


    }
}
