using Microsoft.AspNetCore.Mvc;
using DsiVendas.Models;

namespace DsiVendas.Controllers;

public class ApiController(ApplicationDbContext context) : Controller
{

      public List<Cliente> GetClientes()
      {
        var listaClientes = context.Clientes.ToList();
        return listaClientes;
      }

      [HttpPost]
      public IActionResult CreateCliente([FromBody] Cliente cliente)
      {
        var clientedb = context.Clientes.Add(cliente);
        context.SaveChanges();  
        return CreatedAtAction(nameof(CreateCliente), new { id = clientedb.Entity.Id }, clientedb.Entity);
      }

    [HttpPut]
    public IActionResult TestePut()
    {
        // Apenas retorna uma mensagem de sucesso para teste
        return Ok("Rota PUT funcionando corretamente!");
    }

    [HttpPut]
    public IActionResult UpdateCliente(int id, [FromBody] Cliente cliente)
    {

        // Verifica se o ID passado na URL é o mesmo do cliente no corpo
        if (id != cliente.Id)
        {
            return BadRequest("ID do cliente na URL e no corpo não coincidem.");
        }

        var clientedb = context.Clientes.Find(id);
        if (clientedb == null)
        {
            return NotFound();
        }

        clientedb.Nome = cliente.Nome;
        clientedb.Email = cliente.Email;
        clientedb.Telefone = cliente.Telefone;

        context.Clientes.Update(clientedb);
        context.SaveChanges();
        return NoContent();
    }
}
