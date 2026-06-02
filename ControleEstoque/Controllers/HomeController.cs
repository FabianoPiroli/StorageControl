using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ControleEstoque.Models;
using ControleEstoque.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleEstoque.Controllers;

public class HomeController : Controller
{
    private readonly EstoqueContext _context;

    public HomeController(EstoqueContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalProdutos = await _context.Produtos.CountAsync();
        var estoqueBaixo = await _context.Produtos.CountAsync(p => p.QuantidadeAtual <= p.EstoqueMinimo);
        var totalCategorias = await _context.Categorias.CountAsync();
        var totalFornecedores = await _context.Fornecedores.CountAsync();

        ViewBag.TotalProdutos = totalProdutos;
        ViewBag.EstoqueBaixo = estoqueBaixo;
        ViewBag.TotalCategorias = totalCategorias;
        ViewBag.TotalFornecedores = totalFornecedores;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
