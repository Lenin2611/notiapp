using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Formatos : BaseEntity
{
    public string NombreFormato { get; set; }
    public ICollection<ModuloNotificacion> ModuloNotificaciones { get; set; }

}
