using PeopleDirectory.DataAccess.Entities;
using PeopleDirectory.DataContracts.Enums;

namespace PeopleDirectory.DataAccess
{
    public static class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Cities.Any() || !context.Individuals.Any() || !context.PhoneNumbers.Any() || !context.RelatedIndividuals.Any())
            {
                SeedCities(context);
                context.SaveChanges();
                SeedIndividuals(context);
                context.SaveChanges();
                SeedPhoneNumbers(context);
                context.SaveChanges();
                SeedRelationships(context);
                context.SaveChanges();
            }
        }

        private static void SeedCities(ApplicationDbContext context)
        {
            var cities = new List<City>
        {
            new() { Name = "Tbilisi" },
            new() { Name = "London" },
            new() { Name = "Milan" }
        };

            context.Cities.AddRange(cities);
        }

        private static void SeedIndividuals(ApplicationDbContext context)
        {
            var individuals = new List<Individual>
        {
            new Individual
            {
                FirstName = "John",
                LastName = "Doe",
                Gender = Gender.Male,
                PersonalNumber = "01008059453",
                BirthDate = new DateTime(1994, 1, 1),
                CityId = context.Cities.First().Id,
                ImagePath = "/images/john.jpg"
            },
            new Individual
            {
                FirstName = "Jane",
                LastName = "Smith",
                Gender = Gender.Female,
                PersonalNumber = "01987654321",
                BirthDate = new DateTime(1985, 5, 15),
                CityId = context.Cities.Skip(1).First().Id,
                ImagePath = "/images/jane.jpg"
            }
        };

            context.Individuals.AddRange(individuals);
        }

        private static void SeedPhoneNumbers(ApplicationDbContext context)
        {
            var phoneNumbers = new List<PhoneNumber>
        {
            new PhoneNumber
            {
                IndividualId = context.Individuals.First().Id,
                Type = PhoneType.Mobile,
                Number = "599-456-789"
            },
            new PhoneNumber
            {
                IndividualId = context.Individuals.First().Id,
                Type = PhoneType.Home,
                Number = "232-17-22"
            },
            new PhoneNumber
            {
                IndividualId = context.Individuals.Skip(1).First().Id,
                Type = PhoneType.Office,
                Number = "568-123-456"
            }
        };

            context.PhoneNumbers.AddRange(phoneNumbers);
        }

        private static void SeedRelationships(ApplicationDbContext context)
        {
            var relationships = new List<RelatedIndividual>
        {
            new RelatedIndividual
            {
                IndividualId = context.Individuals.First().Id,
                RelatedIndividualId = context.Individuals.Skip(1).First().Id,
                RelationshipType = RelationType.Colleague
            },
            new RelatedIndividual
            {
                IndividualId = context.Individuals.Skip(1).First().Id,
                RelatedIndividualId = context.Individuals.First().Id,
                RelationshipType = RelationType.Acquaintance
            }
        };

            context.RelatedIndividuals.AddRange(relationships);
        }
    }
}
