using DsiVendas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DsiVendas.Controllers
{
    public class VendasController(ApplicationDbContext context) : Controller
    {
        // GET: Criação de Venda
        public IActionResult Create()
        {
            ViewBag.Clientes = new SelectList(context.Clientes, "IdCliente", "Nome");
            ViewBag.Produtos = new SelectList(context.Produtos, "IdProduto", "Nome");
            return View();
        }

        // POST: Salvar a Venda e seus itens
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venda venda, List<ItemVenda> itensVenda)
        {
            if (ModelState.IsValid)
            {
                context.Add(venda);
                await context.SaveChangesAsync();

                foreach (var item in itensVenda)
                {
                    item.IdVenda = venda.Id;
                    item.PrecoUnitario = context.Produtos.Find(item.IdProduto).Preco;
                    context.ItemsVenda.Add(item);
                }

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Clientes = new SelectList(context.Clientes, "IdCliente", "Nome", venda.IdCliente);
            ViewBag.Produtos = new SelectList(context.Produtos, "IdProduto", "Nome");
            return View(venda);
        }
    }
}