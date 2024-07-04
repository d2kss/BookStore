using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreMgmt.Data;
using BookStoreMgmt.Models;
using BookStoreMgmt.Repository.Interface;
using BookStoreMgmt.Utility;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreMgmt.Controllers
{
    public class BooksController : Controller
    {
        private readonly IUnitOfWork _context;

        public BooksController(IUnitOfWork context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            List<Books> lstClasses = _context.BooksRepository.GetAll().ToList();
            return View(lstClasses);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = _context.BooksRepository
                .Get(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Create
        [Authorize(Roles = RoleEnum.Role_Admin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleEnum.Role_Admin)]
        public async Task<IActionResult> Create([Bind("Id,BookName,Author,Price")] Books books)
        {
            if (ModelState.IsValid)
            {
                _context.BooksRepository.Add(books);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = RoleEnum.Role_Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books =  _context.BooksRepository.Get(u => u.Id == id);
            if (books == null)
            {
                return NotFound();
            }
            return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleEnum.Role_Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookName,Author,Price")] Books books)
        {
            if (id != books.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.BooksRepository.Update(books);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.Id))
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
            return View(books);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = RoleEnum.Role_Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books =  _context.BooksRepository
                .Get(m => m.Id == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = _context.BooksRepository.Get(u => u.Id == id);
            if (books != null)
            {
                _context.BooksRepository.Remove(books);
            }

            _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return _context.BooksRepository.Get(u => u.Id == id).Id > 0;
        }
    }
}
