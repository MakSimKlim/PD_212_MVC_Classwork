using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PD_212_MVC_Classwork.Models
{
    public class Student
    {
        [Key]
        public int stud_id { get; set; }

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

        
        [DisplayName("Группа")]
        [Required]
        //[ForeignKey]
        public required int group { get; set; }

    }
}
