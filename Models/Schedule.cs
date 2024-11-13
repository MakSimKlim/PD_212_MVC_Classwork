using System.ComponentModel.DataAnnotations;

namespace PD_212_MVC_Classwork.Models
{
    public class Schedule
    {
        [Key]
        public long lesson_id { get; set; } // Первичный ключ

        [Required]
        public required short discipline { get; set; } // Дисциплина

        [Required]
        public required int teacher { get; set; } // Учитель

        [Required]
        public required DateTime date { get; set; } // Дата

        [Required]
        public required TimeSpan time { get; set; } // Время

        public bool spent { get; set; } // Было ли проведено

        [Required]
        public required int group { get; set; } // Группа
    }

}
