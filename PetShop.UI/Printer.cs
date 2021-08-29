using System;
using System.Collections.Generic;
using ConsoleTables;
using PetShop.Core.IServices;
using PetShop.Core.Models;

namespace PetShop.UI
{
    public class Printer : IPrinter
    {
        private static readonly StringConstants StringConstants = new();
        private readonly IPetService _petService;
        private readonly IPetTypeService _petTypeService;

        public Printer(IPetService petService, IPetTypeService petTypeService)
        {
            _petService = petService;
            _petTypeService = petTypeService;
        }

        #region Menu Choose Options

        public void StartUi()
        {
            var selection = ShowMenu(StringConstants.menuItems);
            while (selection != 8)
            {
                switch (selection)
                {
                    case 1:
                        ShowPets(_petService.GetPets());
                        break;
                    case 2:
                        ShowTypes(_petTypeService.GetPetTypes());
                        var idPetType = AskQuestion("Select id for pet type");
                        ShowPets(_petService.GetFilteredPetsByType(idPetType));
                        break;
                    case 3:
                        CreateNewPet();
                        break;
                    case 4:
                        ShowPets(_petService.GetPets());
                        var idPet = PrintDeletePet();
                        checkDeleted(_petService.DeletePet(idPet));
                        break;
                    case 5:
                        UpdatePet();
                        break;
                    case 6:
                        var sortOrder = AskQuestion("Enter 1 for ascending and 2 for descending order");
                        ShowPets(_petService.SortByPrice(sortOrder));
                        break;
                    case 7:
                        ShowPets(_petService.GetCheapestPets());
                        break;
                    default:
                        break;
                }

                selection = ShowMenu(StringConstants.menuItems);
            }

            Console.WriteLine(StringConstants.exit);
        }

        #endregion

        #region Menu Options

        public int ShowMenu(string[] menuItems)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Pick a option");
            Console.ResetColor();
            var id = 1;
            foreach (var menuItem in menuItems)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"{id}. {menuItem}");
                id++;
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                   || selection is < 1 or > 8)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You need to choose number between 1-6");
                Console.ResetColor();
            }

            Console.ResetColor();
            return selection;
        }

        #endregion

        #region Menu Methods

        private void ShowPets(List<Pet> getPets)
        {
            if (getPets != null)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                var table = new ConsoleTable("ID", "Name", "Type", "Birthdate", "Sold date", "Color", "Price");
                foreach (var pet in getPets)
                    table.AddRow(pet.Id, pet.Name, pet.Type.Name, pet.Birthdate.ToString("dd/MM/yyyy"),
                        pet.SoldDate.ToString("dd/MM/yyyy"), pet.Color, pet.Price + " \u20AC ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                table.Write();
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("No animals found.");
            }
        }

        private void ShowTypes(List<PetType> getPetTypes)
        {
            if (getPetTypes != null)
            {
                var table = new ConsoleTable("ID", "Type");
                foreach (var petType in getPetTypes)
                {
                    table.AddRow(petType.Id, petType.Name);
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                table.Write();
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("No Pet Types were found.");
            }
        }

        private void CreateNewPet()
        {
            var name = AskQuestion("Enter Pet Name");
            var type = AskQuestion("Enter The pet type");
            var birthdate = AskQuestion("Enter birthdate in format ex.(31/05/2021)");
            var solddate = AskQuestion("Enter sold date in format ex.(31/05/2021)");
            var color = AskQuestion("Enter the color");
            var price = AskQuestion("Enter the price");
            var newPetType = _petTypeService.CreateNewPetType(type);
            var petToBeCreated = _petService.NewPet(name, newPetType, birthdate, solddate, color, price);
            _petService.CreatePet(petToBeCreated);
        }

        private void UpdatePet()
        {
            Console.WriteLine("Please select the pet you wish to update");
            ShowPets(_petService.GetPets());
            var idForEdit = PrintIdFind();
            var petToEdit = _petService.FindPetById(idForEdit);
            Console.WriteLine(
                $"Pet to be edited {petToEdit.Name} of color {petToEdit.Color} and type {petToEdit.Type.Name}");
            var newPetName = AskQuestion("Enter new name");
            var newPetType = AskQuestion("Enter new type");
            var newBirthDate = AskQuestion("Enter new birthdate ex.(31/05/2021)");
            var newSoldDate = AskQuestion("Enter new sold date ex.(31/05/2021)");
            var newColor = AskQuestion("Enter new color");
            var newPrice = AskQuestion("Enter new price");
            string[] formatedBrithdate = newBirthDate.Split("/");
            string[] formatedSolddate = newSoldDate.Split("/");
            var updatedPet = _petService.UpdatePet(
                new Pet
                {
                    Id = idForEdit,
                    Name = newPetName,
                    Type = _petTypeService.UpdatePetType(petToEdit.Type.Id, newPetType),
                    Birthdate = new DateTime(Convert.ToInt32(formatedBrithdate[2]),
                        Convert.ToInt32(formatedBrithdate[1]), Convert.ToInt32(formatedBrithdate[0])),
                    SoldDate = new DateTime(Convert.ToInt32(formatedSolddate[2]), Convert.ToInt32(formatedSolddate[1]),
                        Convert.ToInt32(formatedSolddate[0])),
                    Color = newColor,
                    Price = double.Parse(newPrice)
                });
            CheckUpdated(updatedPet);
        }

        #endregion

        #region Menu Helpers

        private string AskQuestion(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        private int PrintDeletePet()
        {
            Console.WriteLine("Insert Pet Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id)) Console.WriteLine("Please insert a number");

            return id;
        }

        private void checkDeleted(Pet deletePet)
        {
            Console.WriteLine(deletePet != null
                ? "Your Pet was successfully deleted"
                : "Something went wrong please try again");
        }

        private int PrintIdFind()
        {
            Console.WriteLine("Insert Pet Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id)) Console.WriteLine("Please insert a number");

            return id;
        }

        private void CheckUpdated(Pet updatedPet)
        {
            Console.WriteLine(updatedPet != null
                ? "Your Pet was successfully updated"
                : "Something went wrong please try again");
        }

        #endregion
    }
}