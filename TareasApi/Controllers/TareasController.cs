using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TareasApi.Data;
using TareasApi.Models;

namespace TareasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly TareasApiContext _context;
        protected ResultadoApi _resultadoApi;

        public TareasController(TareasApiContext context)
        {
            _context = context;
            _resultadoApi = new();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTarea()
        {
            List<Tarea> ut = await _context.Tarea.ToListAsync();
            if (ut != null)
            {
                _resultadoApi.LTareas = ut;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            Tarea usu = await _context.Tarea.FirstOrDefaultAsync(x => x.Id == id);
            if (usu != null)
            {
                _resultadoApi.Tarea = usu;
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(int id, [FromBody]Tarea tarea)
        {
            Tarea usunew = await _context.Tarea.FirstOrDefaultAsync(x => x.Id == id);

            if (usunew != null)
            {
                usunew.Title = tarea.Title != null ? tarea.Title : usunew.Title;
                usunew.Description = tarea.Description != null ? tarea.Description : usunew.Description;
                usunew.Status = tarea.Status != null ? tarea.Status : usunew.Status;

                _context.Update(usunew);
                await _context.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea([FromBody] Tarea tarea)
        {
            Tarea us = await _context.Tarea.FirstOrDefaultAsync(x => x.Id == tarea.Id);
            if (us == null)
            {
                await _context.Tarea.AddAsync(tarea);
                await _context.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString().ToUpper();
                return Ok(_resultadoApi);
            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString().ToUpper();
                return BadRequest(_resultadoApi);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            Tarea tec = await _context.Tarea.FirstOrDefaultAsync(x => x.Id == id);
            if (tec != null)
            {
                _context.Tarea.Remove(tec);
                await _context.SaveChangesAsync();
                _resultadoApi.httpResponseCode = HttpStatusCode.OK.ToString();
                return Ok(_resultadoApi);

            }
            else
            {
                _resultadoApi.httpResponseCode = HttpStatusCode.BadRequest.ToString();
                return BadRequest(_resultadoApi);
            }
        }
    }
}
