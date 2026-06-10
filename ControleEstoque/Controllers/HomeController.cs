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

        // Novos KPIs Financeiros
        var valorTotalCusto = await _context.Produtos
            .SumAsync(p => p.QuantidadeAtual * p.PrecoCusto);
        var valorTotalVenda = await _context.Produtos
            .SumAsync(p => p.QuantidadeAtual * p.PrecoVenda);

        // Listagem de Alertas Críticos (Estoque Baixo)
        var produtosEstoqueBaixo = await _context.Produtos
            .Include(p => p.Categoria)
            .Where(p => p.QuantidadeAtual <= p.EstoqueMinimo)
            .OrderBy(p => p.QuantidadeAtual)
            .Take(5)
            .ToListAsync();

        // Listagem de Últimas 5 Movimentações
        var ultimasMovimentacoes = await _context.Movimentacoes
            .Include(m => m.Produto)
            .OrderByDescending(m => m.DataMovimentacao)
            .Take(5)
            .ToListAsync();

        // Dados Agrupados de Produtos por Categoria (Gráfico)
        var produtosPorCategoria = await _context.Produtos
            .Include(p => p.Categoria)
            .GroupBy(p => p.Categoria != null ? p.Categoria.Nome : "Sem Categoria")
            .Select(g => new { Categoria = g.Key, Quantidade = g.Count() })
            .ToListAsync();

        // Histórico de Movimentações para Gráfico
        var historicoMovimentacoes = await _context.Movimentacoes
            .Include(m => m.Produto)
            .OrderByDescending(m => m.DataMovimentacao)
            .Take(10)
            .Select(m => new {
                Produto = m.Produto != null ? m.Produto.Nome : "Desconhecido",
                m.Quantidade,
                Tipo = m.Tipo.ToString(),
                Data = m.DataMovimentacao.ToString("dd/MM/yyyy")
            })
            .ToListAsync();

        ViewBag.TotalProdutos = totalProdutos;
        ViewBag.EstoqueBaixo = estoqueBaixo;
        ViewBag.TotalCategorias = totalCategorias;
        ViewBag.TotalFornecedores = totalFornecedores;
        ViewBag.ValorTotalCusto = valorTotalCusto;
        ViewBag.ValorTotalVenda = valorTotalVenda;
        ViewBag.ProdutosEstoqueBaixoList = produtosEstoqueBaixo;
        ViewBag.UltimasMovimentacoes = ultimasMovimentacoes;
        ViewBag.ProdutosPorCategoria = produtosPorCategoria;
        ViewBag.HistoricoMovimentacoes = historicoMovimentacoes;

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
