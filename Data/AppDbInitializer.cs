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
                            TypeOfLocalOD = "SinceMorning",
                            OutDateTime = ("2023, 4, 13, 9, 0, 0") ,
                            InDateTime = ("2023, 4, 13, 13, 0, 0")
                        },

                        new LocalOD()
                        {
                            EmployeeNo = "004",
                            EmployeeName = "Rahul Takmoge",
                            VisitLocation = "ABL Main Branch",
                            PurposeOfVisit = "Official",
                            TypeOfLocalOD = "UptoEvening",
                            OutDateTime = "2023, 4, 14, 8, 0, 0",
                            InDateTime = "2023, 4, 14, 11, 0, 0"
                        }
                    });
                    }
                    context.SaveChanges();

                //VisitorEntry
                if (!context.VisitorsEntries.Any())
                {
                    context.VisitorsEntries.AddRange(new List<VisitorsEntry>()
                    {
                        new VisitorsEntry()
                        {
                            EntryDateTime= "2023, 4, 13, 14, 0, 0",
                            VisitorMobileNo= " 7276713047",
                            VisitorName = "Rohan Takmoge",
                            CompanyName = "ABL Nashik",
                            EmployeeName = "Kiran Chougule",
                            PurposeOfVisit = "Meeting",
                            VisitDateTime = "2023, 4, 13, 14, 0, 0",
                            VisitEndDateTime = "2023, 4, 13, 16, 0, 0",
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

