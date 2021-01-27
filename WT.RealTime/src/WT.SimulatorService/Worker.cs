using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace WT.SimulatorService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly List<Trkpt> trkpts;
        private Timer _timer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            trkpts = new List<Trkpt>();
        }

        //https://localhost:5001/api/v1/Mobile
        private static async Task PostWebApi(string json)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001");
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("/api/v1/Mobile",data);
            }
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested && trkpts.Count>0)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                Location location = new Location
                {
                    Identifier = "1",
                    Latitude = trkpts[0].Lat,
                    Longitude = trkpts[0].Lon
                };
                _logger.LogInformation(JsonSerializer.Serialize(location));
                await PostWebApi(JsonSerializer.Serialize(location));
                trkpts.RemoveAt(0);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(5000, cancellationToken);
            string path = @"C:\My_SourceCode\WT.RealTimeCommunication\WT.RealTime\Resources\Simulation1.xml";
            string readText = await File.ReadAllTextAsync(path,cancellationToken);
            var positions= PositionReader.SerializeFile(readText);
            trkpts.AddRange(positions.Trk.Trkseg.Trkpt);
            await ExecuteAsync(cancellationToken);

        }
    }
}
