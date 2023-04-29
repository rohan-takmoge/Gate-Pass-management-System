using Gate_Pass_management.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gate_Pass_management.Data;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.Migrate();

                //LocalOD
                if (!context.LocalODs.Any())
                {
                    context.LocalODs.AddRange(new List<LocalOD>()
                    {
                        new LocalOD()
                        {
                            EmployeeNo = "001",
                            EmployeeName = "Rohan Takmoge",
                            VisitLocation = "State Bank Of India",
                            PurposeOfVisit = "Official",
                            TypeOfVisit= 0,
                            TypeOfLocalOD = "SinceMorning",
                            OutDateTime = ("13/4/2023, 09:00:00") ,
                            InDateTime = ("13/4/2023, 13:00:00")
                        },

                        new LocalOD()
                        {
                            EmployeeNo = "004",
                            EmployeeName = "Rahul Takmoge",
                            VisitLocation = "ABL Main Branch",
                            PurposeOfVisit = "Official",
                            TypeOfVisit= 0,
                            TypeOfLocalOD = "UptoEvening",
                            OutDateTime = "14/4/2023, 08:00:00",
                            InDateTime = "14/4/2023, 11:00:00"
                        }
                    }); ;
                    }
                    context.SaveChanges();

                //VisitorEntry
                if (!context.VisitorsEntries.Any())
                {
                    context.VisitorsEntries.AddRange(new List<VisitorsEntry>()
                    {
                        new VisitorsEntry()
                        {
                            EntryDateTime= "13/4/2023, 14:00:00",
                            VisitorMobileNo= " 7276713047",
                            VisitorName = "Rohan Takmoge",
                            CompanyName = "ABL Nashik",
                            EmployeeName = "Kiran Chougule",
                            PurposeOfVisit = "Meeting",
                            VisitDateTime = "13/4/2023, 14:00:00",
                            VisitEndDateTime = "13/4/2023, 16:00:00",
                            VehicleType = "Bike",
                            VehicleNo = "ABC-123"
                        },


                    }) ;
                }
                context.SaveChanges();
            }
                
            }

        }
    }

