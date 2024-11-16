using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PD_212_MVC_Classwork.Models
{
    //PJT - Pure Join Table
    [PrimaryKey(nameof(direction), nameof(discipline))]
    public class DirectionsDisciplinesRelation
    {
        //[Key]
        [ForeignKey("Direction")]
        public byte direction { get; set; }

        //[Key]
        [ForeignKey("Discipline")]
        public short discipline { get; set; }

        //Navigation properties
        public required Direction Direction { get; set; }
        public required Discipline Discipline { get; set; }

    }
}
