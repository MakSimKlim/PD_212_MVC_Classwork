using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PD_212_MVC_Classwork.Models;
using PD_212_MVC_Data;

namespace PD_212_MVC_Classwork.Views.Students
{
    public class StudentsController : Controller
    {
        private readonly AcademyContext _context;

        public StudentsController(AcademyContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["LastNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "last_name_desk" : "";
            ViewData["FirstNameSortParam"] = sortOrder == "FirstName" ? "first_name_desk" : "FirstName";
            ViewData["MiddleNameSortParam"] = sortOrder == "MiddleName" ? "middle_name_desk" : "MiddleName";
            ViewData["BirthDateSortParam"] = sortOrder == "BirthDate" ? "birth_date_desk" : "BirthDate";
            ViewData["GroupParam"] = sortOrder == "Group" ? "group_desk" : "Group";
            IQueryable<Student> students = from s in _context.Students select s;

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
                students = students.Where(
                    s => s.last_name.Contains(searchString) ||
                    s.first_name.Contains(searchString) ||
                    s.middle_name!.Contains(searchString)
                    );

            switch (sortOrder)
            {
                default:                students = students.OrderBy(s => s.last_name); break;
                case "last_name_desk":  students = students.OrderByDescending(s => s.last_name); break;
                case "FirstName":       students = students.OrderBy(s => s.first_name); break;
                case "first_name_desk": students = students.OrderByDescending(s => s.first_name); break;
                case "MiddleName":      students = students.OrderBy(s => s.middle_name); break;
                case "middle_name_desk":students = students.OrderByDescending(s => s.middle_name); break;
                case "BirthDate":       students = students.OrderBy(s => s.birth_date); break;
                case "birth_date_desk": students = students.OrderByDescending(s => s.birth_date); break;
                case "Group":           students = students.OrderBy(s => s.group); break;
                case "group_desk":      students = students.OrderByDescending(s => s.group); break;

            }

            return View(await students.AsNoTracking().ToListAsync());
            //return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.stud_id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("stud_id,last_name,first_name,middle_name,birth_date,group")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("stud_id,last_name,first_name,middle_name,birth_date,group")] Student student)
        {
            if (id != student.stud_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.stud_id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.stud_id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.stud_id == id);
        }
    }
}
