using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace ManageStaff.Domain.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(256)]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        [MaxLength(25)]
        public string PhoneNumber { get; set; }

        public byte[]? Logo { get; set; }

        public int PositionId { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("PositionId")]
        public virtual Position? Position { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
    }
}
