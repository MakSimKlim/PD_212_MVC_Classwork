using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PD_212_MVC_Classwork.Models
{
    public class SpecialityDirectionRelation
    {
        //[Key, Column(Order = 1)]
        //public int speciality_id { get; set; } // Внешний ключ на таблицу Speciality
        //[ForeignKey("SpecialityId")]
        //public Specialities Speciality { get; set; }

        //[Key, Column(Order = 2)]
        //public int DirectionId { get; set; } // Внешний ключ на таблицу Direction
        //[ForeignKey("DirectionId")]
        //public Direction Direction { get; set; }
    }

}
