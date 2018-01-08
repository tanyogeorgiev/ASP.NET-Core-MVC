namespace HealthR.Services.Data.Models
{
    using AutoMapper;
    using HealthR.Common.Mapping;
    using HealthR.Data.Models.Medical;

    public class MedicalSheetServiceModel : IMapFrom<MedicalSheet>, IHaveCustomMapping
    {
         
     
        public int Id { get; set; }

        public string PatientId { get; set; }

        public string PatientName { get; set; }

        public string DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string ExaminationDescription { get; set; }

        public string ExaminationDateAndTime { get; set; }

        public void ConfigureMapping(Profile mapper)
        => mapper.CreateMap<MedicalSheet, MedicalSheetServiceModel>()
            .ForMember(m => m.PatientName, cfg => cfg.MapFrom(p => p.Patient.Name))
            .ForMember(m => m.DoctorName, cfg => cfg.MapFrom(p => p.Doctor.Name))
            .ForMember(m => m.ExaminationDateAndTime, cfg => cfg.MapFrom(p => p.ExaminationDateAndTime.ToShortDateString()));
        

    }
    
}
