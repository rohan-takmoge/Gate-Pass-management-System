using Gate_Pass_management.Data;
using Gate_Pass_management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using System.Drawing.Printing;
using System.Globalization;

namespace Gate_Pass_management.Controllers
{
    public class VisitorsEntryController : Controller
    {
        private readonly AppDbContext _context;

        public VisitorsEntryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString , string dateFilter, string purposeFilter)
        {
            ViewData["CurrentFilter"] = searchString;

            var visitorsEntries = from v in _context.VisitorsEntries.ToList()
                                  select v;
            if (!String.IsNullOrEmpty(searchString))
            {
                visitorsEntries = visitorsEntries.Where(v => v.VisitorName.Contains(searchString));
            }
            return View(visitorsEntries);

            /*
            var visitors = _context.VisitorsEntries.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                visitors = visitors.Where(v => v.VisitorName.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(dateFilter) && dateFilter != "All dates")
            {
                DateTime filterDate = DateTime.ParseExact(dateFilter, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                visitors = visitors.Where(v => v.EntryDateTime.Date == filterDate.Date);
            }

            if (!string.IsNullOrEmpty(purposeFilter) && purposeFilter != "All purposes")
            {
                visitors = visitors.Where(v => v.PurposeOfVisit.Contains(purposeFilter));
            }

            visitors = visitors.OrderByDescending(v => v.EntryDateTime);

            ViewBag.SearchString = searchString;
            ViewBag.DateFilterList = new SelectList(_context.VisitorsEntries.Select(v => v.EntryDateTime.Date.ToString("dd/MM/yyyy")).Distinct().OrderByDescending(d => d));
            ViewBag.DateFilter = dateFilter;
            ViewBag.PurposeFilterList = new SelectList(_context.VisitorsEntries.Select(v => v.PurposeOfVisit).Distinct().OrderBy(p => p));
            ViewBag.PurposeFilter = purposeFilter;

            return View(visitors);
            */



        }

        public async Task<IActionResult> Punch(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitorsEntry = await _context.VisitorsEntries.FindAsync(id);
            if (visitorsEntry == null)
            {
                return NotFound();
            }


            visitorsEntry.VisitEndDateTime = DateTime.Now.ToString("dd'/'MM'/'yyyy, HH:mm:ss");

            _context.Update(visitorsEntry);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Create()
        {
            var entry = new VisitorsEntry
            {
                EntryDateTime = DateTime.Now.ToString("dd'/'MM'/'yyyy, HH:mm:ss"),
                VisitDateTime = DateTime.Now.ToString("dd'/'MM'/'yyyy, HH:mm:ss"),
                VisitEndDateTime = " - "
            };
            return View(entry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VisitorsEntry visitorsEntry)
        {
            if (ModelState.IsValid)
            {

                _context.VisitorsEntries.Add(visitorsEntry);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(visitorsEntry);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitorsEntry = _context.VisitorsEntries.Find(id);

            if (visitorsEntry == null)
            {
                return NotFound();
            }

            return View(visitorsEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, VisitorsEntry visitorsEntry)
        {
            if (id != visitorsEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(visitorsEntry);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(visitorsEntry);
        }

        public IActionResult ExportToExcel()
        {
            var visitors = _context.VisitorsEntries.ToList();
            byte[] fileContents;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Visitors");

                // Add header row
                worksheet.Cells[1, 1].Value = "Visitor Mobile No.";
                worksheet.Cells[1, 2].Value = "Visitor Mobile No.";
                worksheet.Cells[1, 3].Value = "Visitor Name";
                worksheet.Cells[1, 4].Value = "Company Name";
                worksheet.Cells[1, 5].Value = "Employee Name (Visitor to)";
                worksheet.Cells[1, 6].Value = "Purpose of Visit";
                worksheet.Cells[1, 7].Value = "Visit Date and Time";
                worksheet.Cells[1, 8].Value = "Visit End Date & Time";
                worksheet.Cells[1, 9].Value = "Vehicle Type";
                worksheet.Cells[1, 9].Value = "Vehicle No.";

                // Add data rows
                for (int row = 2; row <= visitors.Count + 1; row++)
                {
                    var visitor = visitors[row - 2];
                    worksheet.Cells[row, 1].Value = visitor.EntryDateTime ;
                    worksheet.Cells[row, 2].Value = visitor.VisitorMobileNo;
                    worksheet.Cells[row, 3].Value = visitor.VisitorName;
                    worksheet.Cells[row, 4].Value = visitor.CompanyName;
                    worksheet.Cells[row, 5].Value = visitor.EmployeeName;
                    worksheet.Cells[row, 6].Value = visitor.PurposeOfVisit;
                    worksheet.Cells[row, 7].Value = visitor.VisitDateTime ;
                    worksheet.Cells[row, 8].Value = visitor.VisitEndDateTime;
                    worksheet.Cells[row, 9].Value = visitor.VehicleType;
                    worksheet.Cells[row, 10].Value = visitor.VehicleNo;
                }

                fileContents = package.GetAsByteArray();
            }

            return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Visitors.xlsx");
        }







        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visitorsEntry = _context.VisitorsEntries.Find(id);

            if (visitorsEntry == null)
            {
                return NotFound();
            }

            return View(visitorsEntry);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var visitorsEntry = _context.VisitorsEntries.Find(id);
            _context.VisitorsEntries.Remove(visitorsEntry);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }


}
