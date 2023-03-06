using EFC_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC_App.Services
{
    internal class MenuService
    {
        public async Task CreateNewContactAsync()
        {
            var customer = new Customer();

            Console.Write("Förnamn:");
            customer.FirstName = Console.ReadLine() ?? "";

            Console.Write("Efternamn:");
            customer.LastName = Console.ReadLine() ?? "";

            Console.Write("E-postadress:");
            customer.Email = Console.ReadLine() ?? "";

            Console.Write("Telefonnummer:");
            customer.PhoneNumber = Console.ReadLine() ?? "";

            Console.Write("Gatuadress:");
            customer.StreetName = Console.ReadLine() ?? "";


            Console.Write("Postnummer:");
            customer.PostalCode = Console.ReadLine() ?? "";

            Console.Write("Stad:");
            customer.City = Console.ReadLine() ?? "";

            // måste ha en service för att spara ner allt detta, lägger in det i en seperat del under services(DatabaseServices)

            // Save customer to datebase
            await CustomerService.SaveAsync(customer);
        }

        public async Task ListAllContactsAsync()
        {
            // get all customers + addresses from database
            var customers = await CustomerService.GetAllAsync(); // sparar in i en variabel

            if (customers.Any())
            {
                foreach (Customer customer in customers)
                {
                    Console.WriteLine($"Kundnummer:{customer.Id}");
                    Console.WriteLine($"Namn: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"E-postadress:{customer.Email}");
                    Console.WriteLine($"Telefonnummer:{customer.PhoneNumber}");
                    Console.WriteLine($"Adress:{customer.StreetName}, {customer.PostalCode} {customer.City}");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("Inga kunder finns i databasen.");
                Console.WriteLine("");
            }
        }
        public async Task ListSpecificContactAsync()
        {
            // Get specific customer + address from database

            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine();


            if (!string.IsNullOrEmpty(email))
            {
                var customer = await CustomerService.GetAsync(email);

                if (customer != null)
                {
                    Console.WriteLine($"Kundnummer:{customer.Id}");
                    Console.WriteLine($"Namn: {customer.FirstName} {customer.LastName}");
                    Console.WriteLine($"E-postadress:{customer.Email}");
                    Console.WriteLine($"Telefonnummer:{customer.PhoneNumber}");
                    Console.WriteLine($"Adress:{customer.StreetName}, {customer.PostalCode} {customer.City}");
                    Console.WriteLine("");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Ingen kund med den angivna e-postadressen {email} hittades.");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine($"Ingen e-postadress angiven.");
                Console.WriteLine("");
            }
        }

        public async Task UppdateSpecificContactAsync()
        {
            Console.WriteLine("Ange e-postadressen på kunden.");
            var email = Console.ReadLine();

            if (!string.IsNullOrEmpty(email))
            {
                var customer = await CustomerService.GetAsync(email);
                if (customer != null)
                {
                    Console.WriteLine("Skriv in information på de fält som du vill uppdatera. \n");

                    Console.Write("Förnamn:");
                    customer.FirstName = Console.ReadLine() ?? null!;

                    Console.Write("Efternamn:");
                    customer.LastName = Console.ReadLine() ?? null!;

                    Console.Write("E-postadress:");
                    customer.Email = Console.ReadLine() ?? null!;

                    Console.Write("Telefonnummer:");
                    customer.PhoneNumber = Console.ReadLine() ?? null!;

                    Console.Write("Gatuadress:");
                    customer.StreetName = Console.ReadLine() ?? null!;


                    Console.Write("Postnummer:");
                    customer.PostalCode = Console.ReadLine() ?? null!;

                    Console.Write("Stad:");
                    customer.City = Console.ReadLine() ?? null!;


                    await CustomerService.UppdateAsync(customer);

                }
                else
                {
                    Console.WriteLine($"Hittade inte någon kund med en angivna e-postadressen.");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine($"Ingen e-postadress angiven.");
                Console.WriteLine("");
            }
        }

        public async Task DeleteSpecificContactAsync()
        {
            // Delete specific customer from database

            Console.Write("Ange e-postadress på kunden: ");
            var email = Console.ReadLine();


            if (!string.IsNullOrEmpty(email))
            {
                await CustomerService.DeleteAsync(email);

            }
            else
            {
                Console.WriteLine($"Ingen e-postadress angiven.");
                Console.WriteLine("");
            }
        }
    }
}
