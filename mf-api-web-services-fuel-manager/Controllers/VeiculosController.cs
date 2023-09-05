using mf_api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace mf_api_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Veiculos.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Veiculo model)
        {
            if (model.AnoFabricao <= 0 || model.AnoModelo <= 0) return BadRequest(new { message = "Ano de fabricação ou do modelo não suportados" });
           
            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetById", new {id = model.Id}, model);
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
           var model = await _context.Veiculos
                .FirstOrDefaultAsync(x => x.Id == id);
            if(model ==  null) return NotFound(new { message = "Veículo não encontrado" });
            return Ok(model);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model)
        {
            if(id != model.Id) return BadRequest(new { message = "Id informado não existe" }); 
            
            var modeloDb = await _context.Veiculos.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if(modeloDb == null) return NotFound(new { message = "Veículo não encontrado" });

            _context.Veiculos.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Veiculos
                .FindAsync(id);
            if(model == null) return NotFound(new { message = "Veículo não encontrado" });

            _context.Veiculos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
