using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        public byte? direction { get; set; } // Поле типа tinyint в базе данных

    }
}
