using System.ComponentModel.DataAnnotations;

namespace UniversityMVC.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "The student name cannot be blank")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a student name between 3 and 50 characters in length")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Please enter a student name beginning with a capital letter and enter only letters and spaces.")]
        [Display(Name = "Student Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The address cannot be blank")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Please enter an address between 5 and 100 characters in length")]
        [RegularExpression(@"^[a-zA-Z0-9,''-'\s]*$", ErrorMessage = "Please enter an address made up of only letters, numbers, commas and spaces.")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public int? CampusID { get; set; }
        public virtual Campus Campus { get; set; }
    }
}
