using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControleEstoque.Data;
using ControleEstoque.Models;

namespace ControleEstoque.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly EstoqueContext _context;

        public MovimentacoesController(EstoqueContext context)
        {
            _context = context;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var estoqueContext = _context.Movimentacoes.Include(m => m.Produto);
            return View(await estoqueContext.ToListAsync());
        }

        // GET: Movimentacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacaoEstoque = await _context.Movimentacoes
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacaoEstoque == null)
            {
                return NotFound();
            }

            return View(movimentacaoEstoque);
        }

        // GET: Movimentacoes/Create
        public IActionResult Create()
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome");
            return View();
        }

        // POST: Movimentacoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProdutoId,Tipo,Quantidade,DataMovimentacao,Observacao")] MovimentacaoEstoque movimentacaoEstoque)
        {
            if (ModelState.IsValid)
            {
                // Se o valor já está em UTC:
                movimentacaoEstoque.DataMovimentacao = DateTime.SpecifyKind(movimentacaoEstoque.DataMovimentacao, DateTimeKind.Utc);
                // Ou, se o valor do formulário é hora local e você quer converter para UTC:
                // movimentacaoEstoque.DataMovimentacao = DateTime.SpecifyKind(movimentacaoEstoque.DataMovimentacao, DateTimeKind.Local).ToUniversalTime();

                _context.Add(movimentacaoEstoque);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", movimentacaoEstoque.ProdutoId);
            return View(movimentacaoEstoque);
        }

        // GET: Movimentacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacaoEstoque = await _context.Movimentacoes.FindAsync(id);
            if (movimentacaoEstoque == null)
            {
                return NotFound();
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", movimentacaoEstoque.ProdutoId);
            return View(movimentacaoEstoque);
        }

        // POST: Movimentacoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProdutoId,Tipo,Quantidade,DataMovimentacao,Observacao")] MovimentacaoEstoque movimentacaoEstoque)
        {
            if (id != movimentacaoEstoque.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacaoEstoque);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoEstoqueExists(movimentacaoEstoque.Id))
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
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", movimentacaoEstoque.ProdutoId);
            return View(movimentacaoEstoque);
        }

        // GET: Movimentacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movimentacaoEstoque = await _context.Movimentacoes
                .Include(m => m.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movimentacaoEstoque == null)
            {
                return NotFound();
            }

            return View(movimentacaoEstoque);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movimentacaoEstoque = await _context.Movimentacoes.FindAsync(id);
            if (movimentacaoEstoque != null)
            {
                _context.Movimentacoes.Remove(movimentacaoEstoque);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoEstoqueExists(int id)
        {
            return _context.Movimentacoes.Any(e => e.Id == id);
        }
    }
}
