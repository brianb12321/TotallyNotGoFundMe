using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TotallyNotGuFundMe.Models
{
    public class Pledge
    {
        [Key, DisplayName("Pledge ID")]
        public int PledgeId { get; set; }
        [Required, DataType(DataType.Currency), DisplayName("Pledge Amount")]
        public decimal PledgeAmount { get; set; }
        [DataType(DataType.Currency), DisplayName("Amount Paid")]
        public decimal AmountPaid { get; set; }
        [ForeignKey("UserId")]
        public DonationUser User { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
    }
}