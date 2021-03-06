using System;
using System.Collections.Generic;
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
                .Include(evt => evt.User)
                .Include(evt => evt.Pledges)
                .First();

            return foundEvent;
        }

        public void SaveEvent(Event eventObj)
        {
            _context.Events.Add(eventObj);
            _context.SaveChanges();
        }

        public ICollection<Pledge> BeginEvent(int eventId)
        {
            Event foundEvent = GetEventById(eventId);
            foundEvent.EventState = EventState.InProgress;
            _context.SaveChanges();

            return foundEvent.Pledges;
        }
        public ICollection<Pledge> FinishEvent(int eventId)
        {
            Event foundEvent = GetEventById(eventId);
            foundEvent.EventState = EventState.Finished;
            _context.SaveChanges();

            return foundEvent.Pledges;
        }

        public IEnumerable<Pledge> GetPledgesOnUserAndEventId(int eventId, string userId, out Event associatedEvent)
        {
            Event foundEvent = GetEventById(eventId);
            associatedEvent = foundEvent;
            return foundEvent.Pledges.Where(p => p.UserId == userId);
        }

        public void DeleteEventById(int eventId)
        {
            Event foundEvent = _context.Events.Where(e => e.EventId == eventId).Include(e => e.Pledges).FirstOrDefault();
            _context.Pledges.RemoveRange(foundEvent.Pledges);
            _context.Events.Remove(foundEvent);
            _context.SaveChanges();
        }

        public void Update()
        {
            _context.SaveChanges();
        }
    }
}