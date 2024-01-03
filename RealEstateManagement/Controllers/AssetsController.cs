using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateManagement.Models;

namespace RealEstateManagement.Controllers
{
    public class AssetsController : Controller
    {
        private readonly EstateContext _context;

        public AssetsController(EstateContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var info = await _context.AssetTble.Include(a => a.OwnerList).ThenInclude(w => w.GetTenant).ToListAsync();

            ViewBag.Count = info.Count;
            ViewBag.GrandTotal = info.Sum(i => i.OwnerList.Sum(l => l.TotalYearlyIncome));

            ViewBag.Average = info.Count > 0 ? info.Average(i => i.OwnerList.Sum(l => l.TotalYearlyIncome)) : 0;


            return View(info);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.AssetTble.Include(i => i.OwnerList).ThenInclude(p => p.GetTenant)
                .FirstOrDefaultAsync(m => m.AssetId == id);

            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }


        public IActionResult Create()
        {
            return View(new Asset());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asset asset, string command = "")
        {
            if (command == "Add")
            {
                asset.OwnerList.Add(new());
                return View(asset);
            }
            else if (command.Contains("delete"))
            {
                int idx = int.Parse(command.Split('-')[1]);

                asset.OwnerList.RemoveAt(idx);
                ModelState.Clear();
                return View(asset);
            }


            if (ModelState.IsValid)
            {

                var rows = await _context.Database.ExecuteSqlRawAsync("exec SpInsertAsset @p0, @p1, @p2, @p3", asset.PropertyName, asset.P_Address, asset.NumberOfUnits, asset.RentAmount);


                if (rows > 0)
                {
                    asset.AssetId = _context.AssetTble.Max(x => x.AssetId);

                    foreach (var item in asset.OwnerList)
                    {
                        await _context.Database.ExecuteSqlRawAsync("exec SpInsertOwner @p0, @p1, @p2, @p3, @p4", item.OwnerName, item.Own_ContactInformation, item.Salary, item.TenantId, asset.AssetId);
                    }
                }

                return RedirectToAction(nameof(Index));

            }

            return View(asset);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assetEdit = await _context.AssetTble.Include(i => i.OwnerList).ThenInclude(p => p.GetTenant).FirstOrDefaultAsync(i => i.AssetId == id);

            if (assetEdit == null)
            {
                return NotFound();
            }
            return View(assetEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Asset asset, string command = "")
        {
            if (command == "Add")
            {
                asset.OwnerList.Add(new());
                return View(asset);
            }
            else if (command.Contains("delete"))
            {
                int idx = int.Parse(command.Split('-')[1]);

                asset.OwnerList.RemoveAt(idx);
                ModelState.Clear();
                return View(asset);
            }


            if (id != asset.AssetId)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var row = await _context.Database.ExecuteSqlRawAsync("exec SpUpdateAsset @p0, @p1, @p2, @p3, @p4", asset.PropertyName, asset.P_Address, asset.NumberOfUnits, asset.RentAmount, asset.AssetId);


                    foreach (var item in asset.OwnerList)
                    {
                        await _context.Database.ExecuteSqlRawAsync("exec SpInsertOwner @p0, @p1, @p2, @p3, @p4", item.OwnerName, item.Own_ContactInformation, item.Salary, item.TenantId, asset.AssetId);
                    }

                    return RedirectToAction(nameof(Index));
                }


                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.AssetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(asset);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.AssetTble.Include(i => i.OwnerList).ThenInclude(p => p.GetTenant)
                .FirstOrDefaultAsync(m => m.AssetId == id);

            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            await _context.Database.ExecuteSqlAsync($"exec SpDeleteAsset {id}");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        [Route("~/deleteasset/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAjax(int id)
        {

            await _context.Database.ExecuteSqlAsync($"exec SpDeleteAsset {id}");

            return Ok();
        }



        private bool AssetExists(int id)
        {
            return _context.AssetTble.Any(e => e.AssetId == id);
        }
    }
}
