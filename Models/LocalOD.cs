using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gate_Pass_management.Models
{
    public class LocalOD
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string VisitLocation { get; set; }
        public string PurposeOfVisit { get; set; }
        public string TypeOfLocalOD { get; set; }

        public String OutDateTime { get; set; }
        public String InDateTime { get; set; }
        
        public TypeOfVisit TypeOfVisit { get; set; }
    }

    public enum TypeOfVisit
    {
        Personal,
        Official
    }

    public enum TypeOfLocalOD
    {
        SinceMorning,
        Intermediate,
        UptoEvening
    }


}