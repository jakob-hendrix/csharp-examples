using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }

        // derived field
        public int NumberOfPointsOfInterest => PointsOfInterest.Count;

        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } 
            = new List<PointOfInterestDto>();
    }
}
