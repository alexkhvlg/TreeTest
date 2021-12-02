using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeTest
{
    internal class Program
    {
        static string ConnectionString = "Host=localhost;Port=5432;Database=companytest;Username=postgres;Password=postgres";
        static void Main()
        {
            ClearAndGenerateDb();
            Test();
        }

        private static void Test()
        {
            var db = new AppContext(ConnectionString);
            var company2 = db.Companies
                .Include(x=>x.Childs)
                .FirstOrDefault(x => x.Name == "2");
            Print(new List<Company> { company2 });
        }

        private static void ClearAndGenerateDb()
        {
            var db = new AppContext(ConnectionString);

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var list = Generate("", 4);
            Print(list);

            db.Companies.AddRange(list);

            db.SaveChanges();
        }

        private static List<Company> Generate(string name, int maxLevel, int curLevel = 0)
        {
            if (curLevel >= maxLevel)
            {
                return null;
            }

            var list = new List<Company>();

            for (int i = 1; i <= maxLevel; i++)
            {
                var localName = string.IsNullOrEmpty(name) ? i.ToString() : $"{name}.{i}";
                var company = new Company
                {
                    Name = localName,
                    Childs = Generate(localName, maxLevel, curLevel + 1)
                };
                list.Add(company);
            }

            return list;
        }

        private static void Print(List<Company> companyList, int tab = 0)
        {
            if (companyList == null)
            {
                return;
            }

            var space = string.Empty;
            for (int i = 0; i < tab; i++)
            {
                space += "  ";
            }

            companyList.ForEach(company => {
                Console.WriteLine(space + company.Name);
                Print(company.Childs, tab + 1);
            });
        }
    }
}
