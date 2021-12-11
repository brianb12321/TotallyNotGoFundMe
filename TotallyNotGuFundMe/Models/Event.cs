using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TotallyNotGuFundMe.Models
{
    public enum EventState
    {
        Created, InProgress, AwaitingDonations, Finished
    }

    public class Event
    {
        [Key, DisplayName("Event ID")]
        public int EventId { get; set; }
        [Required, DisplayName("Name"), MaxLength(100)]
        public string Name { get; set; }
        [Required, DataType(DataType.Html)]
        public string Description { get; set; }
        [Required, EnumDataType(typeof(EventState))]
        public EventState EventState { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal ExpectedAmount { get; set; }
        public virtual ICollection<Pledge> Pledges { get; set; }
    }
}