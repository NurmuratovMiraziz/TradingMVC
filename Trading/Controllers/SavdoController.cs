using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trading.DbContexts;
using Trading.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Trading.Controllers
{
    public class SavdoController : Controller
    {
        AppDbContext dbContext;

        public SavdoController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            ViewBag.Traderlar = dbContext.Traderlar.ToList();
            TovarSavdo tovarSavdo = new TovarSavdo();
            tovarSavdo.Tovarlar = dbContext.SaleObjects.ToList();
            tovarSavdo.Savdolar = dbContext.Savdolar.Where(p => p.isOpen == true).ToList();
            return View(tovarSavdo);
        }

        public IActionResult RefreshPrice()
        {
            var list = dbContext.SaleObjects.ToList();
            Random random = new Random();
            foreach (var item in list)
            {
                int a = random.Next(-1, 2);
                item.HozirgiNarxi = item.HozirgiNarxi + random.Next(0, item.Amplituda + 1) * a;
                if (item.HozirgiNarxi < 2)
                    item.HozirgiNarxi = item.HozirgiNarxi + item.Amplituda;
            }
            dbContext.SaveChanges();
            IList<Savdo> savdo = new List<Savdo>();
            if (dbContext.Savdolar.Count() != 0)
            {
                savdo = dbContext.Savdolar.ToList().Where(p => p.isOpen == true).ToList();
                foreach (var item in savdo)
                {
                    if (!item.SotibOlindi)
                        item.Foyda = (item.XaridNarxi - (item.SaleObject.HozirgiNarxi + item.SaleObject.SotishFarqi)) * item.XaridSoni;
                    else
                        item.Foyda = (item.SaleObject.HozirgiNarxi - item.XaridNarxi) * item.XaridSoni;
                }
            }
            
            return Json( new
                {
                    tovarlar = list,
                    savdo = savdo
                }
            );
        }

        public IActionResult Sotish(int objectId, int traderId, int soni)
        {
            var saleObject = dbContext.SaleObjects.FirstOrDefault(p => p.Id == objectId);
            var trader = dbContext.Traderlar.FirstOrDefault(p => p.Id == traderId);

            Savdo savdo = new Savdo();
            savdo.Trader = trader;
            savdo.SaleObject = saleObject;
            savdo.Foyda = saleObject.SotishFarqi * soni * (-1);
            savdo.XaridNarxi = saleObject.HozirgiNarxi;
            savdo.XaridSoni = soni;
            savdo.UmumiyHarajat = saleObject.HozirgiNarxi * soni;
            savdo.UmumiyTushum = 0;
            savdo.isOpen = true;
            savdo.SotibOlindi = false;
            savdo.Trader.Balansi -= savdo.UmumiyHarajat;
            dbContext.Savdolar.Add(savdo);
            dbContext.SaveChanges();
            return NoContent();
        }
        public IActionResult SotibOlish(int objectId, int traderId, int soni)
        {
            var saleObject = dbContext.SaleObjects.FirstOrDefault(p => p.Id == objectId);
            var trader = dbContext.Traderlar.FirstOrDefault(p => p.Id == traderId);

            Savdo savdo = new Savdo();
            savdo.Trader = trader;
            savdo.SaleObject = saleObject;
            savdo.Foyda = saleObject.SotishFarqi * soni * (-1);
            savdo.XaridNarxi = saleObject.HozirgiNarxi + saleObject.SotishFarqi;
            savdo.XaridSoni = soni;
            savdo.UmumiyHarajat = saleObject.HozirgiNarxi * soni;
            savdo.UmumiyTushum = 0;
            savdo.Trader.Balansi -= savdo.UmumiyHarajat;
            savdo.isOpen = true;
            savdo.SotibOlindi = true;
            dbContext.Savdolar.Add(savdo);
            dbContext.SaveChanges();
            return NoContent();
        }

        public IActionResult Yopish(int id)
        {
            var savdo = dbContext.Savdolar.Include(p => p.SaleObject).Include(p => p.Trader).FirstOrDefault(p => p.Id == id);
            if (savdo.SotibOlindi)
            {
                savdo.Foyda = (savdo.SaleObject.HozirgiNarxi - savdo.XaridNarxi) * savdo.XaridSoni;
            }
            else
            {
                savdo.Foyda = (savdo.XaridNarxi - (savdo.SaleObject.HozirgiNarxi + savdo.SaleObject.SotishFarqi)) * savdo.XaridSoni;
            }
            savdo.UmumiyTushum = savdo.UmumiyHarajat + savdo.Foyda;
            savdo.isOpen = false;
            savdo.Trader.Balansi += savdo.UmumiyTushum;
            dbContext.SaveChanges();
            return NoContent();
        }
    }
}
