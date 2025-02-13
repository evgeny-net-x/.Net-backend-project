﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend5.Models
{
    public class Hospital
    {
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(200)]
        public String Name { get; set; }

        public String Address { get; set; }

        public ICollection<HospitalPhone> Phones { get; set; }

        public ICollection<Ward> Wards { get; set; }

        public ICollection<HospitalDoctor> Doctors { get; set; }

        public ICollection<HospitalLab> Labs { get; set; }
    }
}
