using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trading.DbContexts;
using Trading.Models;

namespace Trading.Controllers
{
    public class TraderController : Controller
    {
        AppDbContext dbContext;

        public TraderController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.Traderlar.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> AddAsync(Trader trader) 
        { 
            await dbContext.Traderlar.AddAsync(trader);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var trader = dbContext.Traderlar.FirstOrDefault(x => x.Id == id);

            return View();
        }
        public async Task<IActionResult> UpdateAsync(Trader newTrader)
        {
            if(ModelState.IsValid)
            {
                dbContext.Entry(newTrader).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(newTrader);
        }
        public IActionResult Delete(int id)
        {
            var trader = dbContext.Traderlar.FirstOrDefault(y => y.Id == id);

            return View(trader);
        }
        public async Task<IActionResult> DeletAsync(Trader trader)
        {
            dbContext.Traderlar.Remove(trader);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
