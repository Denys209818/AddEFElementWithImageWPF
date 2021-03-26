using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatRenta.EFData
{
    public static class DbSeeder
    {
        public static void SeedAll(EFDataContext context) 
        {
            SeedCat(context);
        }

        private static void SeedCat(EFDataContext context) 
        {
            if (!context._cats.Any()) 
            {
                context._cats.Add(new Entities.AppCat { 
                Name = "Кіт домашній",
                Birthday = new DateTime(2018, 5,30),
                ImagePath = "https://i.ytimg.com/vi/1Ne1hqOXKKI/maxresdefault.jpg"
                });
                context.SaveChanges();
            }
        }
    }
}
