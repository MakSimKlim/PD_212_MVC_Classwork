using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PD_212_MVC_Classwork.Models
{
    //PJT - Pure Join Table
    [PrimaryKey(nameof(teacher), nameof(discipline))]
    public class TeachersDisciplinesRelation
    {
        //[Key]
        [ForeignKey("Teacher")]
        public int teacher { get; set; }

        //[Key]
        [ForeignKey("Discipline")]
        public short discipline { get; set; }

        //Navigation properties
        public required Teacher Teacher { get; set; }
        public required Discipline Discipline { get; set; }

    }
}
