using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Programaloginc.Data;
using Programaloginc.Models;

namespace Programaloginc.Pages;

public class IndexModel(ApplicationDbContext db) : PageModel
{
    [BindProperty]
    public Cliente ClienteForm { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Buscar { get; set; }

    public List<Cliente> Clientes { get; private set; } = [];

    public async Task OnGetAsync()
    {
        ClienteForm.FechaRegistro = DateTime.Today;
        await CargarClientesAsync();
    }

    public async Task<IActionResult> OnGetEditAsync(int id)
    {
        var cliente = await db.Clientes.FirstOrDefaultAsync(item => item.IdCliente == id);
        if (cliente is null)
        {
            return NotFound();
        }

        ClienteForm = cliente;
        await CargarClientesAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostSaveAsync()
    {
        if (!ModelState.IsValid)
        {
            await CargarClientesAsync();
            return Page();
        }

        if (ClienteForm.IdCliente == 0)
        {
            db.Clientes.Add(ClienteForm);
        }
        else
        {
            var cliente = await db.Clientes.FirstOrDefaultAsync(item => item.IdCliente == ClienteForm.IdCliente);
            if (cliente is null)
            {
                return NotFound();
            }

            cliente.NombreCliente = ClienteForm.NombreCliente;
            cliente.CedulaCliente = ClienteForm.CedulaCliente;
            cliente.FechaRegistro = ClienteForm.FechaRegistro;
            cliente.Edad = ClienteForm.Edad;
            cliente.Observaciones = ClienteForm.Observaciones;
        }

        await db.SaveChangesAsync();
        TempData["Mensaje"] = ClienteForm.IdCliente == 0 ? "Cliente agregado." : "Cambios guardados.";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var cliente = await db.Clientes.FirstOrDefaultAsync(item => item.IdCliente == id);
        if (cliente is not null)
        {
            db.Clientes.Remove(cliente);
            await db.SaveChangesAsync();
            TempData["Mensaje"] = "Cliente eliminado.";
        }

        return RedirectToPage();
    }

    private async Task CargarClientesAsync()
    {
        var consulta = db.Clientes.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(Buscar))
        {
            var termino = Buscar.Trim();
            consulta = consulta.Where(cliente =>
                cliente.NombreCliente.Contains(termino) || cliente.CedulaCliente.Contains(termino));
        }

        Clientes = await consulta.OrderBy(cliente => cliente.NombreCliente).ToListAsync();
    }
}
