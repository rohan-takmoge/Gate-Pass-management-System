using System;
using System.ComponentModel.DataAnnotations;

namespace Gate_Pass_management.Models
{
    public class VisitorsEntry
    {
        public int Id { get; set; }

        public String EntryDateTime { get; set; }
        public string VisitorMobileNo { get; set; }
        public string VisitorName { get; set; }
        public string CompanyName { get; set; }
        public string EmployeeName { get; set; }
        public string PurposeOfVisit { get; set; }
        public String VisitDateTime { get; set; }
        public String VisitEndDateTime { get; set; }
        public string VehicleType { get; set; }
        public string VehicleNo { get; set; }

    }

}

