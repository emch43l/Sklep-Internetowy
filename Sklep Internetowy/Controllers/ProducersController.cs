using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.ViewModels.DTO;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProducersController : Controller
    {
        private readonly IProducerRepository _prodRepo; 

        public ProducersController(IProducerRepository producer)
        {
            _prodRepo= producer;
        }

        [Route("admin/producers")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Producer> producers = await _prodRepo.GetAll();
            return View(producers);
        }

        [Route("admin/producers/details/{id}")]

        public async Task<IActionResult> Details(Guid id)
        {
            Producer? producer = await _prodRepo.GetOneByGuid(id);
            if (producer == null)
                return RedirectToAction("Index","Producers");
            return View(producer);
        }

        [Route("admin/producers/create")]

        public IActionResult Create()
        {
            ViewData["Referer"] = HttpContext.Request.Headers["Referer"];
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/producers/create")]
        public async Task<IActionResult> Create(ProducerDTO producer, string? From)
        {
            if (ModelState.IsValid)
            {
                await _prodRepo.Add(producer.MapTo());
                await _prodRepo.SaveChanges();
                if(From == null)
                    return RedirectToAction(nameof(Index));
                return Redirect(From);
            }
            return View(producer.MapTo());
        }

        [HttpGet]
        [Route("admin/producers/edit/{id}")]

        public async Task<IActionResult> Edit(Guid id)
        {
            Producer? producer = await _prodRepo.GetOneByGuid(id);
            if (producer == null)
                return RedirectToAction("Index", "Producers");

            return View(producer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/producers/edit/{id}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid,Name,Description")] Producer producer)
        {

            if (ModelState.IsValid)
            {
                Producer? entity = await _prodRepo.GetOneByGuid(id);

                if(entity == null)
                    return RedirectToAction("Index", "Producers");

                entity.Name = producer.Name;
                entity.Description = producer.Description;

                await _prodRepo.SaveChanges();
  
                return RedirectToAction(nameof(Index));
            }

            return View(producer);
        }

        [Route("admin/producers/delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Producer? producer = await _prodRepo.GetOneByGuid(id);
            if(producer == null)
            {
                return RedirectToAction("Index", "Producers");
            }
            await _prodRepo.Remove(producer.Guid);
            await _prodRepo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
