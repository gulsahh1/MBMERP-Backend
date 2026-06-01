using Application.DTOS;
using Application.Interfaces;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Hubs;

namespace WebAPI.Services
{
    public class DashboardHubService : IDashboardHubService
    {
        private readonly IHubContext<DashboardHub> _hubContext;
        private readonly IDashboardService _dashboardService;

        public DashboardHubService(IHubContext<DashboardHub> hubContext, IDashboardService dashboardService)
        {
            _hubContext = hubContext;
            _dashboardService = dashboardService;
        }

        public async Task RefreshDashboard()
        {
           var data = await _dashboardService.GetDashboardDataAsync();
            await SendDashboard(data);
        }

        public async Task SendDashboard(DashboardDto data)
        {
            Console.WriteLine("🔥 SIGNALR DATA GÖNDERİLDİ");
            //var dashboardData = await _dashboardService.GetDashboardDataAsync();
            //await _hubContext.Clients.All.SendAsync("dashboard:update", dashboardData);

            await _hubContext.Clients.All.SendAsync("dashboard:update", data);


           

        }
    }
}
