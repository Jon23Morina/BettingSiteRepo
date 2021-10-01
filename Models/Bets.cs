using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingSite.Models {
    public class Bets {

        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime? LastUpdatedBet { get; set; }
    }
}
