using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PD_212_MVC_Classwork.Models
{
    public class DirectionDisciplineRelation
    {
        [Key]
        //[Column(Order = 1)] 
        public required int direction_id { get; set; } // Внешний ключ на таблицу Direction
        [ForeignKey("DirectionId")] 
        public Direction? Direction { get; set; } 

        [Key] 
        //[Column(Order = 2)] 
        public required int discipline_id { get; set; } // Внешний ключ на таблицу Discipline
        [ForeignKey("DisciplineId")] 
        public Discipline? Discipline { get; set; }
    }
}
