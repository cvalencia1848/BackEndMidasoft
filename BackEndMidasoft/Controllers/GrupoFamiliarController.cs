using BackEndMidasoft.DB;
using BackEndMidasoft.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace BackEndMidasoft.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GrupoFamiliarController : ControllerBase
    {
        private readonly string cadenaConexion;
        private readonly ILogger<GrupoFamiliarController> logger;
        private readonly UserManager<IdentityUser> userManager;

        public GrupoFamiliarController(IConfiguration configuration, ILogger<GrupoFamiliarController> logger, UserManager<IdentityUser> userManager)
        {
            cadenaConexion = configuration.GetConnectionString("defaultConnection");
            this.logger = logger;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<GrupoFamiliar>>> GetGrupoFamiliar([Required] string Usuario)
        {
            try
            {
                GrupoFamliarDB grupo = new GrupoFamliarDB(cadenaConexion);
                List<GrupoFamiliar> Respuesta = await grupo.GetGrupoFamiliars("SEL", Usuario);

                logger.LogInformation("Obteniendo los Familiares del usuario: " + Usuario);
                return Ok(Respuesta);
            }
            catch (Exception e)
            {
                logger.LogError("Error al traer los datos del usuario " + Usuario + " exeption: " + e.Message);
                return StatusCode(500);
            }
        }
        [HttpPost]
        public async Task<ActionResult> InsertDatos([FromBody] GrupoFamiliar datos)
        {
            try
            {

                GrupoFamliarDB grupo = new GrupoFamliarDB(cadenaConexion);
                string estado = await grupo.GrupoFamiliar("INS", datos);

                if (estado.Equals("INSERT"))
                {
                    logger.LogInformation("Datos del usuario " + datos.Usuario + " Insertados correctamente");
                    return Ok("Datos Insertados Correctamente");
                }
                else
                {
                    logger.LogWarning("Error, con la cedula " + datos.Cedula + " revisar que no este registrada");
                    return BadRequest("Error en la insersion de datos,  Revise que no este registrado");
                }
            }
            catch (Exception e)
            {
                logger.LogError("Error en la ejecucion de Insertar datos, Mensaje de la exception: " + e.Message);
                return StatusCode(500);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateDatos([FromBody] GrupoFamiliar datos)
        {
            try
            {
                GrupoFamliarDB grupo = new GrupoFamliarDB(cadenaConexion);
                string estado = await grupo.GrupoFamiliar("UPD", datos);

                if (estado.Equals("UPDATE"))
                {
                    logger.LogInformation("Datos del familiar " + datos.Cedula + " Actualizados correctamente");
                    return Ok("Datos Actualizados Correctamente");
                }
                else
                {
                    logger.LogWarning("Ocurrio un error en la actualización de datos del familiar " + datos.Cedula);
                    return BadRequest("Error en la Actualizacion de de datos");
                }
            }
            catch (Exception e)
            {
                logger.LogError("Error en la actualizacion de datos." + e.Message);
                return StatusCode(500);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteDatos([FromBody] GrupoFamiliar datos)
        {
            try
            {
                GrupoFamliarDB grupo = new GrupoFamliarDB(cadenaConexion);
                string estado = await grupo.GrupoFamiliar("DEL", datos);

                if (estado.Equals("DELETE"))
                {
                    logger.LogInformation("Datos del familiar " + datos.Cedula + " Eliminados correctamente");
                    return Ok("Datos Eliminados Correctamente");
                }
                else
                {
                    logger.LogWarning("Ocurrio un error en la ELIMINACION del dato del familiar " + datos.Cedula);
                    return BadRequest("Error en la Actualizacion de de datos");
                }
            }
            catch (Exception e)
            {
                logger.LogError("Error en la Eliminacion de datos." + e.Message);
                return StatusCode(500);
            }
        }

    }
}
