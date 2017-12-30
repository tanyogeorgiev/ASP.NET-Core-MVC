using AutoMapper;
using HealthR.Common.Mapping;
using HealthR.Data.Models.Medical;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthR.Services.Data.Models
{
   public class UserMedicalSheetsServiceModel : IMapFrom<MedicalSheet>, IHaveCustomMapping
    {
        public int Id { get; set; }
                
        public string DoctorName { get; set; }

        public string ExaminationDescription { get; set; }

        public int PrescriptionId { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
            .CreateMap<MedicalSheet, UserMedicalSheetsServiceModel>()
            .ForMember(u => u.DoctorName , cfg => cfg.MapFrom(s => s.Doctor.Name));
    }
}
