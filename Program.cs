// 1️⃣ Import necessary libraries
using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration.Attributes; // Required for CSV column mapping

// 2️⃣ Define a class to represent a Digimon
public class Digimon
{
    [Name("Digimon")] // Matches "Digimon" column in CSV
    public string Name { get; set; }

    [Name("Stage")] // Matches "Stage" column in CSV
    public string Stage { get; set; }

    [Name("Type")] // Matches "Type" column in CSV
    public string Type { get; set; }

    [Name("Attribute")] // Matches "Attribute" column in CSV
    public string Attribute { get; set; }

    [Name("Memory")] // Matches "Memory" column in CSV
    public int Memory { get; set; }

    [Name("Equip Slots")] // Matches "Equip Slots" column in CSV
    public int EquipSlots { get; set; }

    [Name("Lv50 Atk")] // Matches "Lv50 Atk" column in CSV
    public int Attack { get; set; }

    [Name("Lv50 Def")] // Matches "Lv50 Def" column in CSV
    public int Defense { get; set; }

    [Name("Lv50 Spd")] // Matches "Lv50 Spd" column in CSV
    public int Speed { get; set; }
}

// 3️⃣ Main program
class Program
{
    static void Main()
    {
        // 4️⃣ Define the CSV file location
        string filePath = "DigiDB_digimonlist.csv"; // Ensure this file is in the project folder

        // 5️⃣ Check if the file exists before proceeding
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Error: Digimon database file not found.");
            return; // Stop execution if the file is missing
        }

        // 6️⃣ Load Digimon data from CSV file into a list
        List<Digimon> digimons = LoadDigimonData(filePath);

        Console.WriteLine("Digimon database loaded successfully!");
        Console.WriteLine($"Total Digimon: {digimons.Count}");

        while (true) // Loop to allow multiple searches
        {
            // 7️⃣ Ask the user to enter a Digimon name
            Console.Write("\nEnter Digimon name to search (or type 'exit' to quit): ");
            string searchName = Console.ReadLine()?.Trim(); // Read user input and remove extra spaces

            // 8️⃣ Check if the user wants to exit
            if (string.IsNullOrEmpty(searchName)) // Prevents empty input
            {
                Console.WriteLine("Invalid input. Please enter a name.");
                continue; // Restart the loop if input is empty
            }

            if (searchName.ToLower() == "exit")
                break; // Exit the program

            // 9️⃣ Search for the Digimon in the list
            Digimon foundDigimon = digimons.Find(d => d.Name.Equals(searchName, StringComparison.OrdinalIgnoreCase));

            // 🔟 Display results or error message
            if (foundDigimon != null)
            {
                Console.WriteLine($"Found: {foundDigimon.Name}");
                Console.WriteLine($" Stage: {foundDigimon.Stage}");
                Console.WriteLine($" Type: {foundDigimon.Type}");
                Console.WriteLine($" Attribute: {foundDigimon.Attribute}");
                Console.WriteLine($" Memory: {foundDigimon.Memory}");
                Console.WriteLine($" Equip Slots: {foundDigimon.EquipSlots}");
                Console.WriteLine($" Attack: {foundDigimon.Attack}");
                Console.WriteLine($" Defense: {foundDigimon.Defense}");
                Console.WriteLine($" Speed: {foundDigimon.Speed}");
            }
            else
            {
                Console.WriteLine("Digimon not found. Try again.");
            }
        }
    }

    // 1️⃣1️⃣ Function to load Digimon data from the CSV file
    static List<Digimon> LoadDigimonData(string filePath)
    {
        using (var reader = new StreamReader(filePath)) // Open the file for reading
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) // Read CSV with correct format
        {
            return new List<Digimon>(csv.GetRecords<Digimon>()); // Store all Digimon in a list
        }
    }
}
