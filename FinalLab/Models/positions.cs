using Pomelo.EntityFrameworkCore.MySql.Query.ExpressionTranslators.Internal;
using System.ComponentModel.DataAnnotations; //key
using System.ComponentModel.DataAnnotations.Schema; //Auto Increment

namespace FinalLab.Models
{
    public class positions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Non auto Increment
        public string positionId { get; set; }

        public string positionName { get; set; }

        public float  baseSalary { get; set; }

        public float  salaryIncreaseRate { get; set; }
    }
}
