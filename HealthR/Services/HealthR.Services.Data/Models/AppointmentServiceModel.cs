namespace HealthR.Services.Data.Models
{
  public  class AppointmentServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string StartTime { get; set; }
        
        public string PatientName { get; set; }

        public string PatientId { get; set; }

        public string CreatorId { get; set; }
    }
}
