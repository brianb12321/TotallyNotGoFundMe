using System;
using System.Data.Entity;
using System.Linq;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.Data
{
    public class EventDataService : IEventDataService
    {
        private readonly ApplicationDbContext _context;

        public EventDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Event GetEventById(int id)
        {
            Event foundEvent = _context.Events
                .Where(evt => evt.EventId == id)
                .Include(evt => evt.Pledges)
                .First();

            return foundEvent;
        }

        public void SaveEvent(Event eventObj)
        {
            _context.Events.Add(eventObj);
            _context.SaveChanges();
        }

        public void BeginEvent()
        {
            throw new NotImplementedException();
        }
    }
}