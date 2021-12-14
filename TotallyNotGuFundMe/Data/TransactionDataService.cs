using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TotallyNotGuFundMe.Models;

namespace TotallyNotGuFundMe.Data
{
    public class TransactionDataService : ITransactionDataService
    {
        private readonly ApplicationDbContext _context;

        public TransactionDataService(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddTransaction(PledgeTransaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
        public IEnumerable<Pledge> GetUnfulfilledPledgesByUser(Event eventObj, string userId)
        {
            var unfulfilledPledges = eventObj.Pledges
                .Where(p => p.Transactions.Sum(t => t.TransactionAmount) < p.PledgeAmount);

            return unfulfilledPledges;
        }
    }
}