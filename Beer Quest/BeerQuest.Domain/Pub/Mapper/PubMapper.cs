using System.Collections.Generic;
using System.Linq;

using BeerQuest.Data.Models;

namespace BeerQuest.Domain.Mapper
{
    public class PubMapper : IPubMapper
    {
        public IEnumerable<Pub> Map(PubResponseData responseData) => responseData.Rows.Select(this.Map);

        public Pub Map(PubResponseRow pubData)
        {
            return new Pub
            {
                Name = pubData.Name,
                Category = pubData.Category,
                Url = pubData.Url,
                Date = pubData.Date,
                Excerpt = pubData.Excerpt,
                Thumbnail = pubData.Thumbnail,
                Latitude = pubData.Latitude,
                Longitude = pubData.Longitude,
                Address = pubData.Address,
                Phone = pubData.Phone,
                Twitter = pubData.Twitter,
                StarsBeer = pubData.StarsBeer,
                StarsAtmosphere = pubData.StarsAtmosphere,
                StarsAmenities = pubData.StarsAmenities,
                StarsValue = pubData.StarsValue,
                Tags = pubData.Tags
            };
        }
    }
}
