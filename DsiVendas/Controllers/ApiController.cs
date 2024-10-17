using Microsoft.AspNetCore.Mvc;
using DsiVendas.Models;

namespace DsiVendas.Controllers;

public class ApiController(ApplicationDbContext context) : Controller
{
  public List<Cliente> Index()
  {
    var listaClientes = context.Clientes.ToList();
    return listaClientes;
  }

  [HttpPost]
  public IActionResult Create([FromBody] Cliente cliente)
  {
    var clientedb = context.Clientes.Add(cliente);
    context.SaveChanges();  
    return CreatedAtAction(nameof(Create), new { id = clientedb.Entity.Id }, clientedb.Entity);
  }

    [HttpPost]
  public IActionResult Update(Cliente cliente)
  {
      var clientedb = context.Clientes.Find(cliente.Id); 
      if (clientedb == null)
      {
        return NotFound();
      }
        clientedb.Nome = cliente.Nome;
        clientedb.Email = cliente.Email;
        clientedb.Telefone = cliente.Telefone;
      context.Clientes.Update(clientedb); 
      context.SaveChanges();
       return Ok(clientedb);

    }
}
