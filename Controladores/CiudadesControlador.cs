using Microsoft.AspNetCore.Mvc;
using IconosGeograficos.Servicios;
using IconosGeograficos.Modelos;
using IconosGeograficos.VistasModelos.CRUD;
using IconosGeograficos.VistasModelos.CRUD.Ciudades;
using Microsoft.AspNetCore.Authorization;

namespace IconosGeograficos.Controladores
{
    [ApiController]
    [Authorize]
    public class CiudadesControlador : Controller
    {
        private readonly ICiudadesServicio _ciudadesServicio;

        public CiudadesControlador(ICiudadesServicio ciudadesServicio)
        {
            _ciudadesServicio = ciudadesServicio;
        }

        //GET Ciudad por Denominacion, cantidad de habitantes y Imagen
        [HttpGet]
        [Route("CiudadesPaises")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var query = _ciudadesServicio.GetQueryable()
                                             .Select(x => new ListarCiudadesDTO { denominacion = x.denominacion, cantidadHabitantes = x.cantidadHabitantes , urlImagen = x.urlImagen } )
                                             .ToList();
                return Ok(query);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //GET Ciudad Detalle
        [HttpGet]
        [Route("CiudadesPaises/details")]
        public async Task<IActionResult> GetDetails()
        {
            try
            {
                var query = _ciudadesServicio.GetDetalleCiudades();
                return Ok(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Buscar Ciudad por parametros
        [HttpGet]
        [Route("CiudadesPaises/byName")]
        public async Task<IActionResult> GetByName([FromQuery] BuscarCiudadesDTO modelo)
        {
            var existe = await _ciudadesServicio.SingleOrDefaultAsync(m => m.denominacion == modelo.denominacion);

            if (existe != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "La ciudad que se quiere buscar no existe!"
                });
            }

            try
            {
                var query = _ciudadesServicio.GetCiudades(modelo.denominacion, modelo.continenteId, modelo.orderBy);
                return Ok(query);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //Crear Ciudad
        [HttpPost]
        [Route("CiudadesPaises/Create")]
        public async Task<IActionResult> CreateCiudades(CrearCiudadesDTO modelo)
        {
            var existe = await _ciudadesServicio.SingleOrDefaultAsync(m => m.denominacion == modelo.denominacion);

            if (existe != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "La ciudad que se quiere crear ya existe!"
                });
            }

            var ciudad = new Ciudades
            {
                denominacion = modelo.denominacion,
                urlImagen = modelo.urlImagen,
                cantidadHabitantes = modelo.cantidadHabitantes,
                superficieTotal = modelo.superficieTotal,
                continenteId = modelo.continenteId
            };
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(modelo.urlImagen))
                    {
                        return Ok("URL Imagen requerido");
                    }
                    if (string.IsNullOrEmpty(modelo.denominacion))
                    {
                        return Ok("Denominacion requerida");
                    }
                    await _ciudadesServicio.Insert(ciudad);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return Ok(new { status = "Exitoso", Message = "La ciudad ha sido creado Exitosamente!" });

        }

        //Actualizar Ciudad
        [HttpPut]
        [Route("CiudadesPaises/Update")]
        public async Task<IActionResult> Edit(ActualizarCiudadesDTO modelo)
        {
            var ciudad = _ciudadesServicio.GetQueryable().FirstOrDefault(c => c.id == modelo.id);

            if (ciudad == null)
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

                    await _ciudadesServicio.Update(ciudad);

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }

            return Ok(new { status = "Exitoso", Message = "La Ciudad ha sido actualizado Exitosamente!" });
        }

        //Borrar Ciudad
        [HttpDelete]
        [Route("CiudadesPaises/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == null) return NotFound();

                await _ciudadesServicio.Delete(id);
                return Ok(new { status = "Exitoso", Message = "La Ciudad ha sido eliminada Exitosamente!" });
            }
            catch (Exception e)
            {
                return NotFound("La Ciudad que quiere eliminar no existe");
            }
        }
    }
}
