using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
using Sklep_Internetowy.Repositories.Interfaces;

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
            IEnumerable<Producer> producers = _prodRepo.GetProducers();
            return View(producers);
        }

        [Route("admin/producers/details/{id}")]
        // GET: Producers/Details/guid
        public IActionResult Details(string id)
        {
            Producer? producer = _prodRepo.GetProducerByGuid(id);
            if (producer == null)
                return RedirectToAction("Index","Producers");
            return View(producer);
        }

        [Route("admin/producers/create")]
        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/producers/create")]
        public IActionResult Create([Bind("Name,Description")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _prodRepo.AddProducer(producer);
                _prodRepo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        [Route("admin/producers/edit/{id}")]
        // GET: Producers/Edit/5
        public IActionResult Edit(string id)
        {
            Producer? producer = _prodRepo.GetProducerByGuid(id);
            if (producer == null)
                return RedirectToAction("Index", "Producers");

            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/producers/edit/{id}")]
        public IActionResult Edit(string id, [Bind("Guid,Name,Description")] Producer producer)
        {

            if (ModelState.IsValid)
            {
                Producer? entity = _prodRepo.GetProducerByGuid(id);

                if(entity == null)
                    return RedirectToAction("Index", "Producers");

                entity.Name = producer.Name;
                entity.Description = producer.Description;

                _prodRepo.Save();
  
                return RedirectToAction(nameof(Index));
            }

            return View(producer);
        }

        [Route("admin/producers/delete/{id}")]
        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            Producer? producer = _prodRepo.GetProducerByGuid(id);

            if (producer == null)
            {
                return RedirectToAction("Index", "Producers");
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("admin/producers/delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            Producer? producer = _prodRepo.GetProducerByGuid(id);
            if(producer == null)
            {
                return RedirectToAction("Index", "Producers");
            }
            _prodRepo.RemoveProducer(id);
            _prodRepo.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
