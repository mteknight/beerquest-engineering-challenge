using System.Collections.Generic;

using BeerQuest.Data.Models;

namespace BeerQuest.Domain.Mapper
{
    public interface IPubMapper
    {
        IEnumerable<Pub> Map(PubResponseData responseData);

        Pub Map(PubResponseRow pubData);
    }
}
