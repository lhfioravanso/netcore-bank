using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Interfaces.Services;

namespace Domain.Services.Jobs
{
    public class DailyIncomeServiceJob: IHostedService
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DailyIncomeServiceJob(IServiceScopeFactory serviceScopeFactory) {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Para fins de testes/exemplo foi colocado para rodar a cada 10mins,
            // mas o ideal seria colocar para rodar apenas 1x no dia em um horario fixo como por ex: 00:01
            _timer = new Timer(ProcessDailyIncomes, null, 0, 600000); 
            return Task.CompletedTask;
        }

        private void ProcessDailyIncomes(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope()) {
				var service = scope.ServiceProvider.GetService<IDailyIncomeService>();
                service.ProcessDailyIncome();
			}
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}