using DsiVendas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DsiVendas.Controllers
{
    public class VendasController(ApplicationDbContext context) : Controller
    {
        // GET: Criação de Venda
        public IActionResult Criar()
        {
            var ListaFormaPagamento = new List<string>
            {
                "Cartão de Débito",
                "Cartão de Crédito",
                "Boleto",
                "PIX"
            };
            ViewBag.Clientes = new SelectList(context.Clientes, "Id", "Nome");
            ViewBag.Produtos = new SelectList(context.Produtos, "Id", "Nome");
            ViewBag.FormaPagamentos = new SelectList(ListaFormaPagamento);
            return View();
        }

        [HttpGet]
        public JsonResult GetPrecoProduto(int idProduto)
        {
            var produto = context.Produtos.FirstOrDefault(p => p.Id == idProduto);
            if (produto != null)
            {
                return Json(produto.Preco);
            }
            return Json(0);
        }

        // POST: Salvar a Venda e seus itens
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Venda venda, List<ItemVenda> itensVenda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Add(venda);
                    await context.SaveChangesAsync();

                    foreach (var item in itensVenda)
                    {
                        item.VendaId = venda.Id;
                        item.PrecoUnitario = context.Produtos.Find(item.ProdutoId).Preco;
                        context.ItemsVenda.Add(item);
                    }

                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Erro ao salvar a venda: {ex.Message}");
                }
            }

            // Recarregar os dados para os dropdowns em caso de erro de validação
            var ListaFormaPagamento = new List<string>
            {
                "Cartão de Débito",
                "Cartão de Crédito",
                "Boleto",
                "PIX"
            };
            ViewBag.Clientes = new SelectList(context.Clientes, "Id", "Nome");
            ViewBag.Produtos = new SelectList(context.Produtos, "Id", "Nome");
            ViewBag.FormaPagamentos = new SelectList(ListaFormaPagamento);

            return View(venda);
        }

        // GET: Vendas/Editar/5
        public IActionResult Editar(int id)
        {
            var venda = context.Vendas
                .Include(v => v.ItensVenda)
                .FirstOrDefault(v => v.Id == id);

            if (venda == null)
            {
                return NotFound();
            }

            var ListaFormaPagamento = new List<string>
            {
                "Cartão de Débito",
                "Cartão de Crédito",
                "Boleto",
                "PIX"
            };
            ViewBag.Clientes = new SelectList(context.Clientes, "Id", "Nome", venda.ClienteId);
            ViewBag.Produtos = new SelectList(context.Produtos, "Id", "Nome");
            ViewBag.FormaPagamentos = new SelectList(ListaFormaPagamento, venda.FormaPagamento);

            return View(venda);
        }

        // POST: Vendas/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Venda venda, List<ItemVenda> itensVenda)
        {
            if (id != venda.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(venda);
                    
                    // Atualizar os itens da venda
                    var itensExistentes = context.ItemsVenda.Where(iv => iv.VendaId == id).ToList();
                    context.ItemsVenda.RemoveRange(itensExistentes);

                    foreach (var item in itensVenda)
                    {
                        item.VendaId = venda.Id;
                        item.PrecoUnitario = context.Produtos.Find(item.ProdutoId).Preco;
                        context.ItemsVenda.Add(item);
                    }

                    await context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Vendas.Any(v => v.Id == venda.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Recarregar os dados para os dropdowns em caso de erro de validação
            var ListaFormaPagamento = new List<string>
            {
                "Cartão de Débito",
                "Cartão de Crédito",
                "Boleto",
                "PIX"
            };
            ViewBag.Clientes = new SelectList(context.Clientes, "Id", "Nome", venda.ClienteId);
            ViewBag.Produtos = new SelectList(context.Produtos, "Id", "Nome");
            ViewBag.FormaPagamentos = new SelectList(ListaFormaPagamento, venda.FormaPagamento);

            return View(venda);
        }
    }
}
