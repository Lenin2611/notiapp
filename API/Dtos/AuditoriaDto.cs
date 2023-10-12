using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class AuditoriaDto
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; }
    public int DescAccion { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public DateOnly FechaModificacion { get; set; }
}
