using Microsoft.AspNetCore.Mvc;
using RealEstateManagement.Models;

namespace RealEstateManagement.ViewComponents
{
    public class ItemList : ViewComponent
    {

        public IViewComponentResult Invoke(List<Owner> info)
        {

            ViewBag.Count = info.Count;
            ViewBag.Total = info.Sum(i => i.TotalYearlyIncome);

            return View(info);
        }

    }
}
