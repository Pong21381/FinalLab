using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace FinalLab.Models
{
    public class employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Non auto Increment
        public string empId { get; set; }

        public string empName { get; set; }

        public string Email { get; set; }

        public string phoneNumber { get; set; }

        public DateTime  HireDate { get; set;}

        public string positionId { get; set; }
    }
}
