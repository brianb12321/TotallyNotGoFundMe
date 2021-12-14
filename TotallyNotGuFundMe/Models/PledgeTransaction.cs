using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TotallyNotGuFundMe.Models
{
    public class PledgeTransaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int PledgeId { get; set; }
        [ForeignKey("PledgeId")]
        public Pledge Pledge { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal TransactionAmount { get; set; }

        //Add additional properties if implementing a real transaction system.
    }
}