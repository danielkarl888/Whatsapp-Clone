using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;
using WAppBIU_Server.Data;
using WAppBIU_Server.Models;

namespace WAppBIU_Server.Controllers
{
    public class RanksController : Controller
    {
        private readonly IRankService _service;

        public RanksController(IRankService service)
        {
            _service = service;
        }

        // GET: Ranks
        public  IActionResult Index()
        {
            return View(_service.GetAllRanks());
        }
        [HttpPost]
        public IActionResult Index(string query)
        {
            return View(_service.QuerySearch(query));
        }
        public IActionResult Index2(string query)
        {
            /*
            var q = from rank in _service.Rank
                    where rank.Name.Contains(query) ||
                          rank.Text.Contains(query)
                    select rank;*/
            return PartialView(_service.QuerySearch(query));
        }
        // GET: Ranks/Details/5
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rank = _service.GetRankById(id);
            if (rank == null)
            {
                return NotFound();
            }

            return View(rank);
        }

        // GET: Ranks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ranks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Number,Name,Text")] Rank rank)
        {
            if (ModelState.IsValid)
            {
                rank.Time=DateTime.Now;
                _service.AddRank(rank);
                //await _service.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rank);
        }

        // GET: Ranks/Edit/5
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rank = _service.GetRankById(id);
            if (rank == null)
            {
                return NotFound();
            }
            return View(rank);
        }

        // POST: Ranks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Number,Name,Text")] Rank rank)
        {
            if (id != rank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var r = _service.GetRankById(id);
                r.Number = rank.Number;
                r.Name = rank.Name;
                r.Text = rank.Text;
                rank.Time = DateTime.Now;
                    //await _service.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(rank);
        }

        // GET: Ranks/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rank = _service.GetRankById(id);
            if (rank == null)
            {
                return NotFound();
            }

            return View(rank);
        }

        // POST: Ranks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var rank = _service.GetRankById(id);
            _service.DeleteRank(rank);
            return RedirectToAction(nameof(Index));
        }
        /*
        private bool RankExists(int id)
        {
            return _service.Rank.Any(e => e.Id == id);
        }*/
    }
}
