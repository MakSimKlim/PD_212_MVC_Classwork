using System.ComponentModel.DataAnnotations;

namespace PD_212_MVC_Classwork.Models
{
    public class Direction
    {
        [Key]
        public byte direction_id { get; set; }
        [Required]
        public required string direction_name { get; set; }

        //Navigation properties
        public ICollection<Group>? Groups { get; set; } // То где отображается Direction

        //public required Discipline Discipline { get; set; }//То что отображается в Direction

        public ICollection<DirectionDisciplineRelation>? DirectionsDisciplinesRelation { get; set; }
        public ICollection<SpecialityDirectionRelation>? SpecialitiesDirectionsRelation { get; set; }
    }
}
