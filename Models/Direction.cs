using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PD_212_MVC_Classwork.Models
{
    public class Direction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte direction_id { get; set; }
        [Required]
        public required string direction_name { get; set; }

        //Navigation properties
        public ICollection<Group>? Groups { get; set; }
        public ICollection<DirectionsDisciplinesRelation>? Disciplines { get; set; }
    }
}
