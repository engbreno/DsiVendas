using Microsoft.AspNetCore.Mvc;
using DsiVendas.Models;
using Microsoft.EntityFrameworkCore;

namespace DsiVendas.Controllers;

public class ProdutosController(ApplicationDbContext context) : Controller
{
  public IActionResult Index()
  {
    var listaProdutos = context.Produtos.Include(x => x.Fornecedor).ToList();
    return View(listaProdutos);
  }


  public IActionResult Criar()
  {
    ViewBag.Fornecedores = context.Fornecedores.ToList();
    return View();
  }

  [HttpPost]
  public IActionResult Criar(Produto produto)
  {
    context.Produtos.Add(produto);
    context.SaveChanges();

    return RedirectToAction("Index");
  }

  public IActionResult Editar(int id)
  {
    var produto = context.Produtos.Find(id);
    if (produto == null)
    {
      return NotFound();
    }
    ViewBag.Fornecedores = context.Fornecedores.ToList();
    return View(produto);
  }

  [HttpPost]
  public IActionResult Editar(Produto produto)
  {
    var produtoExistente = context.Produtos.Find(produto.Id);
    if (produtoExistente == null)
    {
      return NotFound();
    }
    produtoExistente.Nome = produto.Nome;
    produtoExistente.Preco = produto.Preco;
    produtoExistente.FornecedorId = produto.FornecedorId;
    context.Produtos.Update(produtoExistente);
    context.SaveChanges();
    return RedirectToAction("Index");

  }
}
