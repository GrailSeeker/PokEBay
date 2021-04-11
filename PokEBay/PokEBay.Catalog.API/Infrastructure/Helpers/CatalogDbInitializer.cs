using PokEBay.Catalog.API.Domain.Entities;
using System;
using System.Collections.Generic;
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
                    Category = "Seed",
                    Type = "Grass,Poison",
                    Weaknesses = "Fire,Flying,Ice",
                    CreatedBy="Admin",
                    CreatedOn=DateTime.Now,
                    LastModifiedBy="Admin",
                    LastModifiedOn =DateTime.Now
                },
                new CatalogItem
                {
                    Name = "Pikachu",
                    Description ="Can generate powerful electricity have cheek sacs that are extra soft and super stretchy.",
                    Price=500,
                    PictureUri="https://assets.pokemon.com/assets/cms2/img/pokedex/full/025.png",
                    Category = "Mouse",
                    Type = "Electric",
                    Weaknesses = "Ground",
                    CreatedBy="Admin",
                    CreatedOn=DateTime.Now,
                    LastModifiedBy="Admin",
                    LastModifiedOn =DateTime.Now
                },
                new CatalogItem
                {
                    Name = "Charmander",
                    Description ="It has a preference for hot things. When it rains, steam is said to spout from the tip of its tail.",
                    Price=200,
                    PictureUri="https://assets.pokemon.com/assets/cms2/img/pokedex/full/004.png",
                    Category = "Lizard",
                    Type = "Fire",
                    Weaknesses = "Ground,Water,Rock",
                    CreatedBy="Admin",
                    CreatedOn=DateTime.Now,
                    LastModifiedBy="Admin",
                    LastModifiedOn =DateTime.Now
                },
                new CatalogItem
                {
                    Name = "Squirtle",
                    Description ="When it retracts its long neck into its shell, it squirts out water with vigorous force.",
                    Price=150,
                    PictureUri="https://assets.pokemon.com/assets/cms2/img/pokedex/full/007.png",
                    Category = "Tiny Turtle",
                    Type = "Water",
                    Weaknesses = "Grass,Electric",
                    CreatedBy="Admin",
                    CreatedOn=DateTime.Now,
                    LastModifiedBy="Admin",
                    LastModifiedOn =DateTime.Now
                },
                new CatalogItem
                {
                    Name = "Jigglypuff",
                    Description ="It has top-notch lung capacity, even by comparison to other Pokémon. It won’t stop singing its lullabies until its foes fall asleep.",
                    Price=300,
                    PictureUri="https://assets.pokemon.com/assets/cms2/img/pokedex/full/039.png",
                    Category = "Balloon",
                    Type = "Normal,Fairy",
                    Weaknesses = "Steel,Poison",
                    CreatedBy="Admin",
                    CreatedOn=DateTime.Now,
                    LastModifiedBy="Admin",
                    LastModifiedOn =DateTime.Now
                },
                new CatalogItem
                {
                    Name = "Mew",
                    Description ="When viewed through a microscope, this Pokémon’s short, fine, delicate hair can be seen.",
                    Price=600,
                    PictureUri="https://assets.pokemon.com/assets/cms2/img/pokedex/full/151.png",
                    Category = "New Species",
                    Type = "Psychic",
                    Weaknesses = "Ghost,Dark,Bug",
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
