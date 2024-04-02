using BaseApi.DTO;
using BaseApi.Repositories;
using System.Globalization;
using System.Text.Json;

namespace BaseApi.Services
{
    public class UserActivityServiceDTO
    {
        private readonly UserActivityRepository _repository;
        private readonly string _directoryPath = @"D:\datas";


        public UserActivityServiceDTO(UserActivityRepository repository)
        {
            _repository = repository;
        }

        public async Task GenerateYearlyReports()
        {
            var datas = await _repository.GetAllActivity();
            var yearlyData = datas.SelectMany(d => d.Events).GroupBy(ev => ev.Timestamp.Month);

            foreach (var monthlyData in yearlyData)
            {
                var month = monthlyData.Key;

                var monthReport = new List<UserActivityDTO>();


                foreach (var data in datas)
                {
                    var userActivityDTO = new UserActivityDTO
                    {
                        userId = data.userId,
                        Events = new List<EventDTO>()
                    };

                    foreach (var ev in monthlyData)
                    {
                        var eventDTO = new EventDTO
                        {
                            Username = ev.Username,
                            Timestamp = ev.Timestamp,
                            Action = ev.Action.ToString()
                        };

                        userActivityDTO.Events.Add(eventDTO);
                    }

                    monthReport.Add(userActivityDTO);
                }

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                var json = JsonSerializer.Serialize(monthReport, options);

                var directoryPath = Path.Combine(_directoryPath, $"{month}_report.json");
                await File.WriteAllTextAsync(directoryPath, json);
            }
        }

        public async Task CreateMonthlyReport(int monthNumber)
        {
            var datas = await _repository.GetAllActivity();
            var monthlyData = datas.Where(d => d.Events.Any(ev => ev.Timestamp.Month == monthNumber));
            var monthReport = new List<UserActivityDTO>();

            foreach (var data in monthlyData)
            {
                var userActivityDTO = new UserActivityDTO
                {
                    userId = data.userId,
                    Events = new List<EventDTO>()
                };
                foreach (var ev in data.Events.Where(ev => ev.Timestamp.Month == monthNumber))
                {
                    var eventDTO = new EventDTO
                    {
                        Username = ev.Username,
                        Timestamp = ev.Timestamp,
                        Action = ev.Action.ToString()
                    };

                    userActivityDTO.Events.Add(eventDTO);
                }

                monthReport.Add(userActivityDTO);
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(monthReport, options);
            var directoryPath = Path.Combine(_directoryPath, $"{monthNumber}_report.json");
            await File.WriteAllTextAsync(directoryPath, json);
        }

        public async Task<bool> ValidateMonthlyReport(int monthNumber)
        {
            if (File.Exists(_directoryPath + $"{monthNumber}_report.json"))
            {
                return true;
            }
            else
            {
                await CreateMonthlyReport(monthNumber);
                return false;
            }
        }



        public async Task<bool> Reports()
        {
            if (Directory.Exists(_directoryPath)) 
            {
                return true;
            }
            else
            {
                Directory.CreateDirectory(_directoryPath);
                return false;
            }
        }

    }
}
