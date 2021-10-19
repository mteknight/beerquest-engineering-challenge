using System.Collections.Generic;

namespace BeerQuest.Data.Models
{
    public record PubResponseData(PubResponseInfo Info, ICollection<PubResponseField> Fields, ICollection<PubResponseRow> Rows);
}
