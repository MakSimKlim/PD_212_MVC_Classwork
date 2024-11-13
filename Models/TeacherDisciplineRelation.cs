using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PD_212_MVC_Classwork.Models
{
    public class TeacherDisciplineRelation
    {
        [Key, Column(Order = 1)]
        public int teacher_id { get; set; } // Внешний ключ на таблицу Teacher
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        [Key, Column(Order = 2)]
        public int discipline_id { get; set; } // Внешний ключ на таблицу Discipline
        [ForeignKey("DisciplineId")]
        public Discipline Discipline { get; set; }
    }

}
