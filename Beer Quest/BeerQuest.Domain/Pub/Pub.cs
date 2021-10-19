using System;

namespace BeerQuest.Domain
{
    public class Pub : IEquatable<Pub>
    {
        public string? Name { get; set; }

        public string? Category { get; set; }

        public string? Url { get; set; }

        public DateTime Date { get; set; }

        public string? Excerpt { get; set; }

        public string? Thumbnail { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Twitter { get; set; }

        public double StarsBeer { get; set; }

        public double StarsAtmosphere { get; set; }

        public double StarsAmenities { get; set; }

        public double StarsValue { get; set; }

        public string? Tags { get; set; }

        /// <inheritdoc />
        public bool Equals(Pub? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.Name == other.Name &&
                   this.Category == other.Category &&
                   this.Url == other.Url &&
                   this.Date.Equals(other.Date) &&
                   this.Excerpt == other.Excerpt &&
                   this.Thumbnail == other.Thumbnail &&
                   this.Latitude.Equals(other.Latitude) &&
                   this.Longitude.Equals(other.Longitude) &&
                   this.Address == other.Address &&
                   this.Phone == other.Phone &&
                   this.Twitter == other.Twitter &&
                   this.StarsBeer.Equals(other.StarsBeer) &&
                   this.StarsAtmosphere.Equals(other.StarsAtmosphere) &&
                   this.StarsAmenities.Equals(other.StarsAmenities) &&
                   this.StarsValue.Equals(other.StarsValue) &&
                   this.Tags == other.Tags;
        }

        // /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() &&
                   this.Equals((Pub)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            hashCode.Add(this.Name);
            hashCode.Add(this.Category);
            hashCode.Add(this.Url);
            hashCode.Add(this.Date);
            hashCode.Add(this.Excerpt);
            hashCode.Add(this.Thumbnail);
            hashCode.Add(this.Latitude);
            hashCode.Add(this.Longitude);
            hashCode.Add(this.Address);
            hashCode.Add(this.Phone);
            hashCode.Add(this.Twitter);
            hashCode.Add(this.StarsBeer);
            hashCode.Add(this.StarsAtmosphere);
            hashCode.Add(this.StarsAmenities);
            hashCode.Add(this.StarsValue);
            hashCode.Add(this.Tags);

            return hashCode.ToHashCode();
        }
    }
}
