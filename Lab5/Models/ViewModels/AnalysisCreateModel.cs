using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend5.Models.ViewModels
{
    public class AnalysisCreateModel
    {
        public Int32 LabId { get; set; }
        public String Type { get; set; }
        public DateTime Date { get; set; }
        public String Status { get; set; }
    }
}
