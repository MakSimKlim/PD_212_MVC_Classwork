using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PD_212_MVC_Classwork.Models
{
    public class Group
    {
        [Key]
        public int group_id { get; set; }

        [DisplayName("Название группы")]
        [Required]
        public required string group_name { get; set; }

        [DisplayName("Направление")]
        [Required]
        [ForeignKey("Direction")]
        public required byte direction { get; set; } // Поле типа tinyint в базе данных

        //Navigation property
        public required Direction Direction { get; set; }
        public ICollection<Student>? Students { get; set; }

        public ICollection<CompleteDiscipline>? CompleteDisciplines { get; set; }
    }
}
