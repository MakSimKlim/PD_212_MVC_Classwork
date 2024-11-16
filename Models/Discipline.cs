using System.ComponentModel.DataAnnotations;

namespace PD_212_MVC_Classwork.Models
{
    public class Discipline
    {
        [Key]
        public short discipline_id { get; set; }
        [Required]
        public required string discipline_name { get; set; }
        [Required]
        public required byte number_of_lessons { get; set; }  // tinyint в базе ->  в c# это byte

        //Navigation properties
        
    }
}
