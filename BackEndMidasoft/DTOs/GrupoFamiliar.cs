using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndMidasoft.DTOs
{
    public class GrupoFamiliar
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Usuario es requerido")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Cedula es requerido")]
        public int Cedula { get; set; }
        [Required(ErrorMessage = "Nombres es requerido")]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "Apellidos es requerido")]
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public string Parentesco { get; set; }
        [Required(ErrorMessage = "Edad es requerido")]
        public int Edad { get; set; }
        public string MenorEdad { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}
