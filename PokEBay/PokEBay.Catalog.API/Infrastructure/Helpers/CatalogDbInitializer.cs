using PokEBay.Catalog.API.Domain.Entities;
using System;
using System.Linq;

namespace PokEBay.Catalog.API.Infrastructure.Helpers
{
    public static class CatalogDbInitializer
    {
        public static void Initialize(CatalogContext catalogContext)
        {
            catalogContext.Database.EnsureCreated();

            if (catalogContext.CatalogItems.Any())
            {
                return;
            }

            var catalogItems = new CatalogItem[]
            {
                new CatalogItem
                {
                    Name = "Bulbasaur",
                    Description ="There is a plant seed on its back right from the day this Pokémon is born. The seed slowly grows larger.",
                    Price=100,
                    PictureUri="https://assets.pokemon.com/assets/cms2/img/pokedex/full/001.png",
                    CreatedBy="Admin",
                    CreatedOn=DateTime.Now,
                    LastModifiedBy="Admin",
                    LastModifiedOn =DateTime.Now
                },
                new CatalogItem
                {
                    Name = "Pikachu",
                    Description ="Pikachu that can generate powerful electricity have cheek sacs that are extra soft and super stretchy.",
                    Price=500,
                    PictureUri="https://assets.pokemon.com/assets/cms2/img/pokedex/full/025.png",
                    CreatedBy="Admin",
                    CreatedOn=DateTime.Now,
                    LastModifiedBy="Admin",
                    LastModifiedOn =DateTime.Now
                }
            };

            foreach (var item in catalogItems)
            {
                catalogContext.CatalogItems.Add(item);
            }

            catalogContext.SaveChanges();
        }
    }
}
