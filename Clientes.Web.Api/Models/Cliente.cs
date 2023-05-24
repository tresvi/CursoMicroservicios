using System;
using System.Collections.Generic;

namespace Clientes.Web.Api.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Cuil { get; set; } = null!;

    public string TipoDocumento { get; set; } = null!;

    public int NroDocumento { get; set; }

    public bool? EsEmpleadoBna { get; set; }

    public string PaisOrigen { get; set; } = null!;
}
