using CoreSignalRCRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSignalRCRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IHubContext<SignalRServer> _signalrHub;
        public ProductController(AppDBContext context, IHubContext<SignalRServer> signalrHub)
        {
            _context = context;
            _signalrHub = signalrHub;
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var data = _context.ProductModels.ToList();
            return View(data);
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var data = _context.ProductModels.ToList();
            return Ok(data);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel  objModel)
        {
            try
            {
                _context.Add(objModel);
                _context.SaveChanges();
                _signalrHub.Clients.All.SendAsync("refreshProducts");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = _context.ProductModels.Find(id);
            _context.ProductModels.Remove(data);
            _context.SaveChanges();
            _signalrHub.Clients.All.SendAsync("refreshProducts");
            return RedirectToAction(nameof(Index));
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
