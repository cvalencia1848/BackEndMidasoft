using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndMidasoft.DTOs
{
    public class RespuestaAutenticacion
    {
        public string token { get; set; }
        public DateTime expiracion { get; set; }
    }
}
