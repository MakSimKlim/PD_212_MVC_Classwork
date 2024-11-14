using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PD_212_MVC_Classwork.Models
{
    public class CompleteDiscipline
    {
        //[Key]
        //[Column(Order = 1)]
        public int group { get; set; } // Внешний ключ на таблицу Group
        //[ForeignKey("Group")]
        public required Group Group { get; set; } //То что отображается в CompleteDiscipline
        //[Key]
        //[Column(Order = 2)]
        public int discipline { get; set; } // Внешний ключ на таблицу Discipline
        //[ForeignKey("Discipline")]
        public required Discipline Discipline { get; set; } //То что отображается в CompleteDiscipline
    }
}
