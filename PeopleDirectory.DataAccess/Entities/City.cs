using System.ComponentModel.DataAnnotations.Schema;

namespace PeopleDirectory.DataAccess.Entities
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Individual> Individuals { get; set; }
    }
}
