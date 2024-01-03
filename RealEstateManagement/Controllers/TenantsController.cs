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
    public class TenantsController : Controller
    {
        private readonly EstateContext _context;
        private readonly IWebHostEnvironment _environment;

        public TenantsController(EstateContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

       
        public async Task<IActionResult> Index()
        {
            return View(await _context.TenantTble.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.TenantTble
                .FirstOrDefaultAsync(m => m.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }



        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantId,TenantName,T_ContactInformation,LeaseStartDate, ImageUpload")] Tenant tenant)
        {

            if (tenant.ImageUpload != null)
            {


                tenant.TenantImage = "\\Image\\" + tenant.ImageUpload.FileName;


                string serverPath = _environment.WebRootPath + tenant.TenantImage;


                using FileStream stream = new FileStream(serverPath, FileMode.Create);


                await tenant.ImageUpload.CopyToAsync(stream);


            }

            if (ModelState.IsValid)
            {
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tenant);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.TenantTble.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }
            return View(tenant);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantId,TenantName,T_ContactInformation,LeaseStartDate,TenantImage, ImageUpload")] Tenant tenant)
        {
            if (id != tenant.TenantId)
            {
                return NotFound();
            }


            if (tenant.ImageUpload != null)
            {



                tenant.TenantImage = "\\Image\\" + tenant.ImageUpload.FileName;


                string serverPath = _environment.WebRootPath + tenant.TenantImage;


                using FileStream stream = new FileStream(serverPath, FileMode.Create);


                await tenant.ImageUpload.CopyToAsync(stream);


            }



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.TenantId))
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
            return View(tenant);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _context.TenantTble
                .FirstOrDefaultAsync(m => m.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.TenantTble.FindAsync(id);
            if (tenant != null)
            {
                _context.TenantTble.Remove(tenant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool TenantExists(int id)
        {
            return _context.TenantTble.Any(e => e.TenantId == id);
        }
    }
}
