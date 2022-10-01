using System.ComponentModel.DataAnnotations;
using CityInfo.API.Models;

namespace CityInfo.API.Entities
{
    public class City
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        // list of children
        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; }
            = new List<PointOfInterestDto>();

        public City(string name)
        {
            Name = name;
        }
    }
}
