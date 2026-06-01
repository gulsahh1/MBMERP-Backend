using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs
{
    public class DashboardHub:Hub
    {
        private readonly IDashboardService _dashboardService;

        public DashboardHub(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task GetDashboard()
        {
            var dashboardData = await _dashboardService.GetDashboardDataAsync();
            await Clients.Caller.SendAsync("dashboard:update", dashboardData);

        }
    }
}
