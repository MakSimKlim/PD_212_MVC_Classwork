﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PD_212_MVC_Classwork.Models;
using PD_212_MVC_Data;

namespace PD_212_MVC_Classwork.Views.Teachers
{
    public class TeachersController : Controller
    {
        private readonly AcademyContext _context;

        public TeachersController(AcademyContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["LastNameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "last_name_desk" :"";
            ViewData["FirstNameSortParam"] = sortOrder == "FirstName" ? "first_name_desk" : "FirstName";
            ViewData["MiddleNameSortParam"] = sortOrder == "MiddleName" ? "middle_name_desk" : "MiddleName";
            ViewData["BirthDateSortParam"] = sortOrder == "BirthDate" ? "birth_date_desk" : "BirthDate";
            ViewData["WorkSinceSortParam"] = sortOrder == "WorkSince" ? "work_since_desk" : "WorkSince";
            IQueryable<Teacher> teachers = from t in _context.Teachers select t;

            ViewData["CurrentFilter"] = searchString;

            if(!String.IsNullOrEmpty(searchString))
                teachers = teachers.Where(
                    t => t.last_name.Contains(searchString) || 
                    t.first_name.Contains(searchString) ||
                    //t.middle_name == null ? "" : t.middle_name.Contains(searchString)
                    t.middle_name!.Contains(searchString)
                    );

            switch (sortOrder)
            {
                default:                teachers = teachers.OrderBy(t => t.last_name); break;
                case "last_name_desk":  teachers = teachers.OrderByDescending(t => t.last_name); break; 
                case "FirstName":       teachers = teachers.OrderBy(t => t.first_name); break; 
                case "first_name_desk": teachers = teachers.OrderByDescending(t => t.first_name); break; 
                case "MiddleName":      teachers = teachers.OrderBy(t => t.middle_name); break; 
                case "middle_name_desk":teachers = teachers.OrderByDescending(t => t.middle_name); break; 
                case "BirthDate":       teachers = teachers.OrderBy(t => t.birth_date); break;
                case "birth_date_desk": teachers = teachers.OrderByDescending(t => t.birth_date); break;
                case "WorkSince": teachers = teachers.OrderBy(t => t.work_since); break;
                case "work_since_desk": teachers = teachers.OrderByDescending(t => t.work_since); break;

            }

            return View(await teachers.AsNoTracking().ToListAsync());

            //return View(await _context.Teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.teacher_id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("teacher_id,last_name,first_name,middle_name,birth_date,work_since")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("teacher_id,last_name,first_name,middle_name,birth_date,work_since")] Teacher teacher)
        {
            if (id != teacher.teacher_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.teacher_id))
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
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.teacher_id == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.teacher_id == id);
        }
    }
}
