using System;
using System.Collections.Generic;

namespace GestionF1.Web.EF;

public partial class Escuderium
{
    public int IdEscuderia { get; set; }

    public string? NombreEscuderia { get; set; }

    public virtual ICollection<Piloto> Pilotos { get; set; } = new List<Piloto>();
}
