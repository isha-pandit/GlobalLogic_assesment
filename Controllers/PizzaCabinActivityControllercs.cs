using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GlobalLogic_PizzaIncSln.Models;
using System.Reflection.PortableExecutable;
using System;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace GlobalLogic_PizzaIncSln.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaCabinActivityControllercs : ControllerBase
    {
        private readonly AppDBContext _appDBContext;
        public PizzaCabinActivityControllercs(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        [HttpGet]
        [Route("/GetAllScheduledData")]
        public async Task<IActionResult> GetAllScheduledData()
        {
            try
            {
                string url = Constant.url;

                HttpClient client = new HttpClient();

                string response = await client.GetStringAsync(url);

                var jsonData = JsonConvert.DeserializeObject<Root>(response);

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpPost]
        [Route("/InsertScheduledData")]
        public async Task<IActionResult> InsertDataToDB([FromBody] ScheduleResultViewModel scheduleResultViewModel)
        {
            try
            {
                Schedule? existingSchedule = new Schedule();
                Projection? existingProjection = new Projection();

                if (scheduleResultViewModel != null && scheduleResultViewModel.Schedules != null)
                {
                    scheduleResultViewModel.Schedules.ForEach(schedule =>
                    {
                        existingSchedule = _appDBContext.Schedules.FirstOrDefault(s => s.PersonId == schedule.PersonId);
                        if (existingSchedule == null)
                        {
                            var dbSchedule = new Schedule
                            {
                                ContractTimeMinutes = schedule.ContractTimeMinutes,
                                Date = schedule.Date,
                                IsFullDayAbsence = schedule.IsFullDayAbsence,
                                Name = schedule.Name,
                                PersonId = schedule.PersonId,
                                Id = schedule.Id,

                                Projection = schedule.Projection.Select(projection => new Projection
                                {
                                    Color = projection.Color,
                                    Description = projection.Description,
                                    Start = projection.Start,
                                    Minutes = projection.Minutes,
                                    ScheduleId = projection.ScheduleId,
                                }).ToList()
                            };
                            schedule.Projection.ForEach(projection =>
                            {
                                existingProjection = _appDBContext.Projections.FirstOrDefault(x => x.Id == projection.Id);
                            });

                            _appDBContext.Schedules.AddAsync(dbSchedule);
                        }
                    });
                    if (existingSchedule != null && existingProjection != null)
                    {
                        return BadRequest($"Schedule already exists.");

                    }
                    await _appDBContext.SaveChangesAsync();
                    return Ok("Schedule data received successfully.");
                }
                else
                    return BadRequest("Schedule data is null.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

    }

}

