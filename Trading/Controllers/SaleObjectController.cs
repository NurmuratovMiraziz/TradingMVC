using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trading.DbContexts;
using Trading.Models;

namespace Trading.Controllers
{
    public class SaleObjectController : Controller
    {
        AppDbContext dbContext;
        public SaleObjectController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.SaleObjects.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> AddAsync(SaleObject saleObject)
        {
            await dbContext.SaleObjects.AddAsync(saleObject);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var saleObject = dbContext.SaleObjects.FirstOrDefault(x => x.Id == id);

            return View(saleObject);
        }

        public async Task<IActionResult> UpdateAsync(SaleObject newSaleObject)
        {
            if(ModelState.IsValid)
            {
                dbContext.Entry(newSaleObject).State = EntityState.Modified;
                await dbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(newSaleObject);
        }

        public IActionResult Delete(int id)
        {
            var saleObject = dbContext.SaleObjects.FirstOrDefault(x => x.Id == id);

            return View(saleObject);
        }

        public async Task<IActionResult> DeletAsync(SaleObject saleObject)
        {
            dbContext.SaleObjects.Remove(saleObject);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
