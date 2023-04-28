using Gate_Pass_management.Data;
using Gate_Pass_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Gate_Pass_management.Controllers
{
    public class LocalODController : Controller
    {
        private readonly AppDbContext _context;

        public LocalODController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var localOD = from v in _context.LocalODs.ToList()
                                  select v;
            if (!String.IsNullOrEmpty(searchString))
            {
                localOD = localOD.Where(v => v.EmployeeName.Contains(searchString));
            }
            return View(localOD);

        }

        
        public async Task<IActionResult> Punch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localOD = await _context.LocalODs.FindAsync(id);
            if (localOD == null)
            {
                return NotFound();
            }

        
            localOD.InDateTime = DateTime.Now.ToString("dd'/'MM'/'yyyy, HH:mm:ss");

            _context.Update(localOD);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Punch(int id, LocalOD localOD)
        {
            if (id != localOD.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(localOD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (id == localOD.Id)
                    {
                        throw;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(new List<LocalOD> { localOD });

        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localOD = _context.LocalODs.Find(id);

            if (localOD == null)
            {
                return NotFound();
            }

            return View(localOD);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LocalOD localOD)
        {
            if (id != localOD.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(localOD);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(localOD);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var localOD = _context.LocalODs.Find(id);
            if (localOD == null)
            {
                return NotFound();
            }

            return View(localOD);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var localOD = _context.LocalODs.Find(id);
            _context.LocalODs.Remove(localOD);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult ExportToExcel()
        {
            var LocalOds = _context.LocalODs.ToList();
            byte[] fileContents;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("LocalOD");

                // Add header row
                worksheet.Cells[1, 1].Value = "Employee No.";
                worksheet.Cells[1, 2].Value = "Employee Name.";
                worksheet.Cells[1, 3].Value = "Visit Location";
                worksheet.Cells[1, 6].Value = "Purpose of Visit";
                worksheet.Cells[1, 7].Value = "Type of Local OD";
                worksheet.Cells[1, 8].Value = "Out Date and Time";
                worksheet.Cells[1, 9].Value = "In Date & Time";


                // Add data rows
                for (int row = 2; row <= LocalOds.Count + 1; row++)
                {
                    var localod = LocalOds[row - 2];
                    worksheet.Cells[row, 1].Value = localod.EmployeeNo;
                    worksheet.Cells[row, 2].Value = localod.EmployeeName;
                    worksheet.Cells[row, 3].Value = localod.VisitLocation;
                    worksheet.Cells[row, 4].Value = localod.PurposeOfVisit;
                    worksheet.Cells[row, 6].Value = localod.TypeOfLocalOD;
                    worksheet.Cells[row, 7].Value = localod.OutDateTime;
                    worksheet.Cells[row, 8].Value = localod.InDateTime;
                }

                fileContents = package.GetAsByteArray();
            }


            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LocalODs.xlsx");
        }
        public IActionResult Create()
        {
            var entry = new LocalOD
            {
                
                InDateTime=" - ",
                OutDateTime=" - "
            };
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LocalOD localOD)
        {
            if (ModelState.IsValid)
            {
                if (localOD.TypeOfLocalOD == TypeOfLocalOD.SinceMorning.ToString())
                {
                    localOD.OutDateTime = DateTime.Now.ToString("dd-MM-yyyy 09:30:00");
                    localOD.InDateTime = DateTime.Now.ToString("G");
                }
                else if (localOD.TypeOfLocalOD == TypeOfLocalOD.UptoEvening.ToString())
                {
                    localOD.OutDateTime = DateTime.Now.ToString("G");
                    localOD.InDateTime = DateTime.Now.ToString("dd-MM-yyyy 18:30:00");
                }
                else if (localOD.TypeOfLocalOD == TypeOfLocalOD.Intermediate.ToString())
                {
                    localOD.OutDateTime = DateTime.Now.ToString("G");
                    localOD.InDateTime = DateTime.Now.ToString("G");
                }

                _context.Add(localOD);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(localOD);
        }

    }
}

/*                  if (localOD.TypeOfLocalOD == TypeOfLocalOD.SinceMorning.ToString())
                    {
                        // Set out date and time to 9:30 AM
                        localOD.OutDateTime = new DateTime(localOD.InDateTime.Year, localOD.InDateTime.Month, localOD.InDateTime.Day, 9, 30, 0);
                        _context.LocalODs.Add(localOD);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));
                    }
                    else if (localOD.TypeOfLocalOD == TypeOfLocalOD.UptoEvening.ToString())
                    {
                        // Set in date and time to 6:30 PM
                        localOD.InDateTime = new DateTime(localOD.OutDateTime.Year, localOD.OutDateTime.Month, localOD.OutDateTime.Day, 18, 30, 0);
                        _context.LocalODs.Add(localOD);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Index));


if (ModelState.IsValid)
    {
        if (localOD.TypeOfLocalOD == TypeOfLocalOD.SinceMorning)
        {
            
            localOD.OutDateTime = DateTime.Today.Add(new TimeSpan(9, 30, 0));
            
        }
        else if (localOD.TypeOfVisit == TypeOfVisit.UptoEvening)
        {
            
            localOD.InDateTime = DateTime.Today.Add(new TimeSpan(18, 30, 0));
        }

        _context.Add(localOD);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    return View(localOD);
}
*/