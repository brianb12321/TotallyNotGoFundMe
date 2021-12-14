using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.Data
{
    public interface ITransactionDataService
    {
        void AddTransaction(PledgeTransaction transaction);
        IEnumerable<Pledge> GetUnfulfilledPledgesByUser(Event eventObj, string userId);
    }
}