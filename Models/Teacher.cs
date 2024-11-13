﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PD_212_MVC_Classwork.Models
{
    public class Teacher
    {
        [Key]
        public int teacher_id { get; set; }

        [DisplayName("Фамилия")]
        [Required]
        public required string last_name { get; set; }

        [DisplayName("Имя")]
        [Required]
        public required string first_name { get; set; }

        [DisplayName("Отчество")]
        public string? middle_name { get; set; }

        [DisplayName("Дата рождения")]
        [Required]
        [DataType(DataType.Date)]
        public required DateTime birth_date { get; set; }

        [DisplayName("Дата трудоустройства")]
        [Required]
        [DataType(DataType.Date)]
        public required DateTime work_since { get; set; }

        //Navigation properties
        //public required Discipline Discipline { get; set; } // То что отображается в Teachers

        public ICollection<TeacherDisciplineRelation>? TeachersDisciplinesRelation { get; set; }
        public ICollection<SpecialityTeacherRelation>? SpecialitiesTeachersRelation { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }
}
