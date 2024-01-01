using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATMApp.Models
{
    [Table("tbl_atm")]
    public class AtmDataModel
    {
        [Key]
        public int CardId { get; set; }
        public double CardNum { get; set; }
        public int CardPin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Balance { get; set; }
    }
}
