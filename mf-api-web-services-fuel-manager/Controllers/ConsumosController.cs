﻿using mf_api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_api_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ConsumosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Consumos.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Consumo model)
        {
          
            _context.Consumos.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetById", new { id = model.Id }, model);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Consumos
                 .FirstOrDefaultAsync(x => x.Id == id);
            if (model == null) return NotFound(new { message = "Consumo não encontrado" });
            return Ok(model);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Consumo model)
        {
            if (id != model.Id) return BadRequest(new { message = "Id informado não existe" });

            var modeloDb = await _context.Consumos.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (modeloDb == null) return NotFound(new { message = "Consumo não encontrado" });

            _context.Consumos.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Consumos
                .FindAsync(id);
            if (model == null) return NotFound(new { message = "Consumo não encontrado" });

            _context.Consumos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
