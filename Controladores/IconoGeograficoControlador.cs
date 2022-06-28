using Microsoft.AspNetCore.Mvc;
using IconosGeograficos.Servicios;
using IconosGeograficos.Modelos;
using IconosGeograficos.VistasModelos.CRUD;
using IconosGeograficos.VistasModelos.CRUD.IconosGeograficos;
using Microsoft.AspNetCore.Authorization;

namespace IconosGeograficos.Controladores
{
    [ApiController]
    [Authorize]
    public class IconoGeograficoControlador : Controller
    {
        private readonly IiconoGeograficoServicio _icoGeograficoServicio;

        public IconoGeograficoControlador(IiconoGeograficoServicio icoGeograficoServicio)
        {
            _icoGeograficoServicio = icoGeograficoServicio;
        }

        //GET Icono por Denominacion y Imagen
        [HttpGet]
        [Route("IconoGeografico")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var query = _icoGeograficoServicio.GetQueryable()
                            .Select(x => new ListarIconoGeograficoDTO { denominacion = x.denominacion, urlImagen = x.urlImagen })
                            .ToList();
                return Ok(query);
            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //GET Icono Detalle
        [HttpGet]
        [Route("IconoGeografico/details")]
        public async Task<IActionResult> GetDetails()
        {
            try
            {
                var query = _icoGeograficoServicio.GetIconosGeograficosDetalles();
                return Ok(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Buscar Icono
        [HttpGet]
        [Route("IconoGeografico/byName")]
        public async Task<IActionResult> GetByName([FromQuery] BuscarIconoGeograficoDTO modelo)
        {
            var existe = await _icoGeograficoServicio.FirstOrDefaultAsync(i => i.denominacion.Contains(modelo.denominacion));

            if(existe == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "El Icono Geografico no existe!"
                });
            }
            
            try
            {
                var query = _icoGeograficoServicio.GetIcono(modelo.denominacion, modelo.fechaCreacion, modelo.altura, modelo.idPais);
                return Ok(query);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Crear Icono
        [HttpPost]
        [Route("IconoGeografico/create")]
        public async Task<IActionResult> Create(CrearIconoGeograficoDTO modelo)
        {
            var existe = await _icoGeograficoServicio.SingleOrDefaultAsync(m => m.denominacion == modelo.denominacion);

            if (existe != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "El icono que quiere crear ya existe!"
                });
            }

            var icono = new Modelos.IconosGeograficos
            {
                urlImagen = modelo.urlImagen,
                denominacion = modelo.denominacion,
                fechaCreacion = modelo.fechaCreacion,
                altura = modelo.altura,
                historia = modelo.historia
            };

            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(modelo.urlImagen))
                    {
                        return Ok("URL Imagen requerida");
                    }
                    if (string.IsNullOrEmpty(modelo.denominacion))
                    {
                        return Ok("Denominacion requerida");
                    }
                    if (string.IsNullOrEmpty(modelo.historia))
                    {
                        return Ok("Historia requerida");
                    }

                    await _icoGeograficoServicio.Insert(icono);
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return Ok(new { status = "Exitoso", Message = "El Icono Geografico ha sido creado Exitosamente!" });
        }

        //Actualizar Icono
        [HttpPut]
        [Route("IconoGeografico/Update")]
        public async Task<IActionResult> Edit(ActualizarIconoGeograficoDTO modelo)
        {
            var icono = _icoGeograficoServicio.GetQueryable().FirstOrDefault(c => c.id == modelo.id);

            if(icono == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "Numero de ID no encontrado!"
                });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(modelo.urlImagen))
                    {
                        return Ok("URL Imagen requerida");
                    }
                    if (string.IsNullOrEmpty(modelo.denominacion))
                    {
                        return Ok("Denominacion requerida");
                    }
                    if (string.IsNullOrEmpty(modelo.historia))
                    {
                        return Ok("Historia requerida");
                    }

                    await _icoGeograficoServicio.Update(icono);

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return Ok(new { status = "Exitoso", Message = "El Icono Geografico ha sido actualizado Exitosamente!" });
        }

        //Borrar Icono
        [HttpDelete]
        [Route("IconoGeografico/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == null) return NotFound();

                await _icoGeograficoServicio.Delete(id);
                return Ok(new { status = "Exitoso", Message = "El Icono Geografico ha sido eliminado Exitosamente!" });
            }
            catch(Exception e)
            {
                return NotFound("El personaje que quiere eliminar no existe");
            }
        }

    }
}
