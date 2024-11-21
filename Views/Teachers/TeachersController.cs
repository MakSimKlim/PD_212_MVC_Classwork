using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PD_212_MVC_Classwork.Models;
using PD_212_MVC_Data;

using static System.Net.Mime.MediaTypeNames;

namespace PD_212_MVC_Classwork.Views.Teachers
{
    public class TeachersController : Controller
    {
        private readonly AcademyContext _context;

        public TeachersController(AcademyContext context)
        {
            _context = context;
        }

        public Teacher Teacher { get; set; } = default!;

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
                .Include(t => t.Disciplines!)
                .ThenInclude(d => d.Discipline)
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
        public async Task<IActionResult> Create([Bind("teacher_id,last_name,first_name,middle_name,birth_date,work_since")] Teacher teacher, List<IFormFile> Image)// добавили List<IFormFile> Image для добавления фото в БД
        {
            //==== код для добавления фото в базу данных ===========

            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        teacher.Image = stream.ToArray();
                    }
                }
            }

            //======================================================

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

            //var teacher = await _context.Teachers.FindAsync(id);

            var teacher = await _context.Teachers
                .Include(t => t.Disciplines!)
                .ThenInclude(d => d.Discipline)
                .FirstOrDefaultAsync(m => m.teacher_id == id);

            //чтобы отобразить список всех дисциплин
            var disciplines = await _context.Disciplines.ToListAsync();
            ViewData["Disciplines"] = new SelectList(disciplines, "discipline_id", "discipline_name");

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
        //public async Task<IActionResult> Edit(int id, [Bind("teacher_id,last_name,first_name,middle_name,birth_date,work_since")] Teacher teacher)
        public async Task<IActionResult> Edit(int id, [Bind("teacher_id,last_name,first_name,middle_name,birth_date,work_since")] Teacher teacher, List<IFormFile> Image)// добавили List<IFormFile> Image для добавления фото в БД
        {
            if (id != teacher.teacher_id)
            {
                return NotFound();
            }

            //************ код для добавления фото в базу данных *************

            // Получаем текущего преподавателя из базы, чтобы сохранить старое изображение если оно уже хранится в базе
            var existingTeacher = await _context.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.teacher_id == id);

            if (existingTeacher == null)
            {
                return NotFound();
            }

            // Если новое изображение не выбрано, оставляем старое
            if (Image == null || !Image.Any())
            {
                teacher.Image = existingTeacher.Image;
            }
            else
            {
                // Обновляем изображение, если было передано
                foreach (var item in Image)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            teacher.Image = stream.ToArray();
                        }
                    }
                }
            }

            //******************************************************************


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    //_context.Update(teacher.Disciplines!);
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
                return RedirectToAction(nameof(Edit));
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


        //=============== ВАРИАНТ МЕТОДА AddDiscipline ==================================
        /*public async Task<IActionResult> AddDiscipline(int? teacher_id, short? discipline_id)
        {
            Teacher teacher = await _context.Teachers
                .Include(t => t.Disciplines)
                .ThenInclude(d => d.Discipline)
                .FirstOrDefaultAsync(m => m.teacher_id == teacher_id);
            //List<Discipline> disciplines = _context.Disciplines.ToList();
            if (teacher == null)
                return NotFound($"Teacher with ID {teacher_id} not found.");

            Discipline discipline = await _context.Disciplines
                .FirstOrDefaultAsync(d => d.discipline_id == discipline_id);
            
            teacher.Disciplines.Add(
                new TeachersDisciplinesRelation
                {
                    teacher = teacher.teacher_id,
                    discipline = (short)discipline_id,
                    //Teacher = teacher,
                    //Discipline = discipline
                }
                );

           

            return View(teacher);
        }*/
        //================================================================================

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

        //==============================================================================================
        //Метод для добавления дисциплины преподавателю в лист дисциплин, которые он может вести
        //Принимает ID преподавателя и ID дисциплины.

        /*
        [HttpPost]// Обязательный атрибут для данного метода,
                  // т.к он добавляет новую связь между преподавателем и дисциплиной в базу данных
        [ValidateAntiForgeryToken]// Проверяет наличие токена антифальсификации,
                                  // чтобы защититься от CSRF-атак.
        public async Task<IActionResult> AddDiscipline(int teacherId, short disciplineId) 
        {
            // Загружаем преподавателя вместе с его дисциплинами из базы данных.
            var teacher = await _context.Teachers
                .Include(t => t.Disciplines) // Загружаем связанные дисциплины преподавателя через связь "многие-ко-многим".
                .FirstOrDefaultAsync(t => t.teacher_id == teacherId); // Ищем преподавателя с указанным ID.

            if (teacher == null) // Проверяем, найден ли преподаватель.
                return NotFound($"Teacher with ID {teacherId} not found."); // Если преподаватель не найден, возвращаем ошибку 404.

            // Загружаем дисциплину по ее ID из базы данных.
            var discipline = await _context.Disciplines.FindAsync(disciplineId);
            if (discipline == null) // Проверяем, найдена ли дисциплина.
            {
                return NotFound($"Discipline with ID {disciplineId} not found."); // Если дисциплина не найдена, возвращаем ошибку 404.
            }

            // Проверяем, не была ли дисциплина уже добавлена этому преподавателю.
            if (teacher.Disciplines!.Any(td => td.discipline == disciplineId))
            {
                // Если связь уже существует, добавляем ошибку в состояние модели.
                ModelState.AddModelError("", "This discipline is already assigned to the teacher.");
                // Возвращаем пользователя обратно на страницу редактирования.
                return RedirectToAction(nameof(Edit), new { id = teacherId });
            }

            // Создаем новую связь между преподавателем и дисциплиной.
            var relation = new TeachersDisciplinesRelation
            {
                teacher = teacherId, // Указываем ID преподавателя.
                discipline = disciplineId, // Указываем ID дисциплины.
                Teacher = teacher, // Присваиваем объект преподавателя для навигационного свойства.
                Discipline = discipline // Присваиваем объект дисциплины для навигационного свойства.
            };

            // Добавляем новую связь в контекст базы данных.
            _context.Add(relation);

            // Сохраняем изменения в базе данных.
            await _context.SaveChangesAsync();

            // Перенаправляем пользователя обратно на страницу редактирования преподавателя.
            return RedirectToAction(nameof(Edit), new { id = teacherId });
        }
        */

        //=====================ТОТ ЖЕ КОД БЕЗ КОММЕНТАРИЕВ=======================================
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDiscipline(int teacherId, short disciplineId)
        {           
            var teacher = await _context.Teachers
                .Include(t => t.Disciplines) 
                .FirstOrDefaultAsync(t => t.teacher_id == teacherId);

            if (teacher == null) 
                return NotFound($"Teacher with ID {teacherId} not found."); 
                   
            var discipline = await _context.Disciplines.FindAsync(disciplineId);

            if (discipline == null)
                return NotFound($"Discipline with ID {disciplineId} not found.");
           
            if (teacher.Disciplines!.Any(td => td.discipline == disciplineId))
            {
                ModelState.AddModelError("", "This discipline is already assigned to the teacher.");
                return RedirectToAction(nameof(Edit), new { id = teacherId });
            }

            var relation = new TeachersDisciplinesRelation
            {
                teacher = teacherId,
                discipline = disciplineId,
                Teacher = teacher,
                Discipline = discipline
            };

            _context.Add(relation);

            await _context.SaveChangesAsync();
               
            return RedirectToAction(nameof(Edit), new { id = teacherId });
        }
        

        //=================================================================================================


    }
}
