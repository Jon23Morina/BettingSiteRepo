using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BettingSite.Data;
using BettingSite.Models;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BettingSite.Api {
    [Route("api/[controller]")]
    [ApiController]
    public class BettingController : ControllerBase {

        private readonly ApplicationDbContext database;
        public BettingController(ApplicationDbContext dbcontext) {
            database = dbcontext;
        }
        // GET: api/<BettingController>

        [HttpGet]
        public List<Bets> GetBets() {
            var bets =  database.Bets.ToList();
            return bets;
        }

        // GET api/<BettingController>/5
        [HttpGet("{id}")]
        public ActionResult<Bets> Get(Guid id) {
            var bets = database.Bets.Find(id);
            return bets;
        }

        // POST api/<BettingController>
        [HttpPost]
        public async Task<ActionResult> Post([Bind("Id,Amount, LastUpdatedBet")]
            Bets bet) {
            bet.LastUpdatedBet = null;
            var bets = database.Bets.Add(bet);
            await database.SaveChangesAsync();
            return RedirectToAction("/swagger/index.html");
        }

        // PUT api/<BettingController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Bets>> Put(Guid id, [FromBody] [Bind("Id,Amount, LastUpdatedBet")]
            Bets bet) {
            //Entry(person).Property(p => p.Name).IsModified = true;
            var bets = database.Bets.FirstOrDefault(i => i.Id == id);
            database.Entry(bets).State = EntityState.Detached;
            database.Entry(bet).State = EntityState.Modified;
            bet.LastUpdatedBet = DateTime.Now;
            
            database.Bets.Update(bet);
            await database.SaveChangesAsync();
            return bet;
        }

        // DELETE api/<BettingController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bets>> Delete(Guid id) {
            var bets = database.Bets.FirstOrDefault(i => i.Id == id);
            if(bets == null) {
                return NotFound();
            }
            database.Bets.Remove(bets);
            await database.SaveChangesAsync();

            return Ok();
        }
    }
}
