﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend5.Models
{
    public class Doctor
    {
        public Int32 Id { get; set; }

        public ICollection<HospitalDoctor> Hospitals { get; set; }

        public ICollection<DoctorPatient> Patients { get; set; }
        
        [Required]
        [MaxLength(200)]
        public String Name { get; set; }

        public String Speciality { get; set; }
    }
}
