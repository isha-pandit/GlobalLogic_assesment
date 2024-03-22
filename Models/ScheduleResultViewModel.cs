namespace GlobalLogic_PizzaIncSln.Models
{
   public class ScheduleResultViewModel
    {
        public List<ScheduleViewModel> Schedules { get; set; }
    }
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public int ContractTimeMinutes { get; set; }
        public DateTime Date { get; set; }
        public bool IsFullDayAbsence { get; set; }
        public string Name { get; set; }
        public string PersonId { get; set; }
        public List<ProjectionViewModel> Projection { get; set; }
    }

    public class ProjectionViewModel
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public int Minutes { get; set; }
        public int ScheduleId { get; set; }
        public ScheduleViewModel? Schedule { get; set; }
    }

}
