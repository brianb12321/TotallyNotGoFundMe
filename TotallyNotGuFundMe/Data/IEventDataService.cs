using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.Data
{
    public interface IEventDataService
    {
        Event GetEventById(int id);
        void SaveEvent(Event eventObj);
        ICollection<Pledge> BeginEvent(int eventId);
        IEnumerable<Pledge> GetPledgesOnUserAndEventId(int eventId, string userId, out Event associatedEvent);
    }
}