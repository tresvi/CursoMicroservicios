using System;
using System.Collections.Generic;

namespace Transferencias.Web.Api.Models;

public partial class Transferencia
{
    public int Id { get; set; }

    public string CuilOriginante { get; set; }

    public string CuilDestinatario { get; set; }

    public string CbuOrigen { get; set; }

    public string CbuDestino { get; set; }

    public decimal Importe { get; set; }

    public string Concepto { get; set; }

    public string Descripcion { get; set; }
}
