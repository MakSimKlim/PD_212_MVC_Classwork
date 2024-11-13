using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PD_212_MVC_Classwork.Models
{
    public class Discipline
    {
        [Key]
        public required short discipline_id { get; set; }

        [DisplayName("Дисциплина")]
        [Required]
        public required string discipline_name { get; set; }

        [DisplayName("Количество уроков")]
        [Required]
        public required byte number_of_lessons { get; set; } // Поле типа tinyint в базе данных

        //Navigation properties
        //public ICollection<Teacher>? Teachers { get; set; } // То где отображается Discipline
        //public ICollection<Direction>? Directions { get; set; } // То где отображается Discipline
        public ICollection<TeacherDisciplineRelation>? TeachersDisciplinesRelation { get; set; }
        public ICollection<DirectionDisciplineRelation>? DirectionsDisciplinesRelation { get; set; }
        public ICollection<SpecialityDisciplineRelation>? SpecialitiesDisciplinesRelation { get; set; }
        public ICollection<DemandDiscipline>? DemandDisciplines { get; set; }
        public ICollection<CompleteDiscipline>? CompleteDisciplines { get; set; }
        public ICollection<Schedule>? Schedules { get; set; }
    }
}
