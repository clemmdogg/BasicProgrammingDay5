﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static BasicProgrammingDay5.Exercises.AdvancedPhonebook;

namespace BasicProgrammingDay5.Exercises
{
    internal class AdvancedPhonebook
    {
        public class Person
        {
            public int PersonID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int PhoneNumber { get; set; }
            /// <summary>
            /// Constructs a person to the phone book.
            /// </summary>
            /// <param name="personID"></param>
            /// <param name="firstName"></param>
            /// <param name="lastName"></param>
            /// <param name="email"></param>
            /// <param name="phoneNumber"></param>
            public Person(int personID, string firstName, string lastName, string email, int phoneNumber)
            {
                PersonID = personID;
                FirstName = firstName;
                LastName = lastName;
                Email = email;
                PhoneNumber = phoneNumber;
            }
        }
        public static void RunPhoneBookProgram()
        {
            PhonebookMenu();
        }
        public static void PhonebookMenu()
        {
            bool isUserQuittingMenu = false;
            while (!isUserQuittingMenu)
            {
                string userInput = "";
                Console.Clear();
                Console.WriteLine("Halløj og velkommen til den advancerede telefonbog..");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Tast [I] for at indtaste en ny person i telefonbogen.");
                Console.WriteLine("Tast [SL] for at slette en person i telefonbogen.");
                Console.WriteLine("Tast [SØ] for at søge efter personer i telefonbogen.");
                Console.WriteLine("Tast [A] for at afslutte programmet.");
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "i")
                {
                    AddNewPersonToPhoneBook();
                }
                else if (userInput.ToLower() == "sl")
                {
                    DeletePersonInPhoneBook();
                }
                else if (userInput.ToLower() == "sø")
                {
                    SearchMenuPhoneBook();
                }
                else if (userInput.ToLower() == "a")
                {
                    isUserQuittingMenu = true;
                }
                else
                {
                    Console.WriteLine("Forkert input. Prøv igen!!");
                    Console.ReadKey();
                }
            }
            Console.WriteLine("Halløj tak fordi du brugte den advancerede telefonbog. Farvel!!");
            Console.ReadKey();
        }
        public static void AddNewPersonToPhoneBook()
        {
            List<Person> phoneBook = DeserializePersons();
            phoneBook.Add(GetNewPerson(phoneBook));
            SerializePersons(phoneBook);
        }
        public static void DeletePersonInPhoneBook()
        {
            List<Person> phoneBook = DeserializePersons();
            int userPersonIDInput = GetPersonIDInput();
            Person personToDelete = phoneBook.FirstOrDefault(person => person.PersonID == userPersonIDInput);
            if (personToDelete != null)
            {
                phoneBook.Remove(personToDelete);
                SerializePersons(phoneBook);
                Console.WriteLine($"{personToDelete.FirstName} {personToDelete.LastName} er ny slettet fra telefonbogen!!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Der findes ingen person med dette person ID i telefonbogen!!");
                Console.ReadKey();
            }
           
        }
        public static void SearchMenuPhoneBook()
        {
            bool isUserQuittingMenu = false;
            while (!isUserQuittingMenu)
            {
                string userInput = "";
                Console.Clear();
                Console.WriteLine("Halløj og velkommen til den advancerede telefonbog søgefunktion..");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Tast [V] for at vise hele telefonbogen.");
                Console.WriteLine("Tast [F] for at søge efter et fornavn.");
                Console.WriteLine("Tast [E] for at søge efter et efternavn.");
                Console.WriteLine("Tast [T] for at søge efter et telefonnummer.");
                Console.WriteLine("Tast [A] for for at gå tilbage til hovedmenuen.");
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "v")
                {
                    List<Person> phoneBook = DeserializePersons();
                    ShowPhoneBook(phoneBook);
                }
                else if (userInput.ToLower() == "f")
                {
                    List<Person> phoneBook = DeserializePersons();
                    Console.Clear();
                    Console.Write("Skriv tekst du vil søge i fornavn efter:");
                    string userFirstNameInput = Console.ReadLine();
                    List<Person> filteredPhoneBook = FilterFirstName(phoneBook, userFirstNameInput);
                    if (filteredPhoneBook.Any())
                    {
                        ShowPhoneBook(filteredPhoneBook);
                    }
                    else
                    {
                        Console.WriteLine($"{userFirstNameInput} findes ikke i nogen fornavne!!");
                        Console.ReadKey();
                    }
                }
                else if (userInput.ToLower() == "e")
                {
                    List<Person> phoneBook = DeserializePersons();
                    Console.Clear();
                    Console.Write("Skriv tekst du vil søge i efternavn efter:");
                    string userLastNameInput = Console.ReadLine();
                    List<Person> filteredPhoneBook = FilterLastName(phoneBook, userLastNameInput);
                    if (filteredPhoneBook.Any())
                    {
                        ShowPhoneBook(filteredPhoneBook);
                    }
                    else
                    {
                        Console.WriteLine($"{userLastNameInput} findes ikke i nogen efternavne!!");
                        Console.ReadKey();
                    }
                }
                else if (userInput.ToLower() == "t")
                {
                    List<Person> phoneBook = DeserializePersons();
                    Console.Clear();
                    int userTelephoneNumberInput = GetPhoneNumberInput("Skriv tal du vil søge i telefonnummer efter: ");
                    List<Person> filteredPhoneBook = FilterTelephoneNumber(phoneBook, userTelephoneNumberInput);
                    if (filteredPhoneBook.Any())
                    {
                        ShowPhoneBook(filteredPhoneBook);
                    }
                    else
                    {
                        Console.WriteLine($"{userTelephoneNumberInput} findes ikke i nogen telefonnum!!");
                        Console.ReadKey();
                    }
                }
                else if (userInput.ToLower() == "a")
                {
                    isUserQuittingMenu = true;
                }
                else
                {
                    Console.WriteLine("Forkert input. Prøv igen!!");
                }
            }
        }
        public static List<Person> FilterFirstName(List<Person> phoneBook, string userFirstNameInput)
        {
            List<Person> filteredPhoneBook = [];
            foreach (Person person in phoneBook)
            {
                if (person.FirstName.Contains(userFirstNameInput))
                {
                    filteredPhoneBook.Add(person);
                }
            }
            return filteredPhoneBook;
        }
        public static List<Person> FilterLastName(List<Person> phoneBook, string userLastNameInput)
        {
            List<Person> filteredPhoneBook = [];
            foreach (Person person in phoneBook)
            {
                if (person.LastName.Contains(userLastNameInput))
                {
                    filteredPhoneBook.Add(person);
                }
            }
            return filteredPhoneBook;
        }
        public static List<Person> FilterTelephoneNumber(List<Person> phoneBook, int userLastNameInput)
        {
            List<Person> filteredPhoneBook = [];
            foreach (Person person in phoneBook)
            {
                if (person.PhoneNumber.ToString().Contains(userLastNameInput.ToString()))
                {
                    filteredPhoneBook.Add(person);
                }
            }
            return filteredPhoneBook;
        }
        public static void ShowPhoneBook(List<Person> phoneBook)
        {
            int counter = 0;
            int tenCounter = -1;
            List<Person> sortedByLastNameThenFirstName = phoneBook.OrderBy(person => person.LastName).ThenBy(person => person.FirstName).ToList();
            double pages = sortedByLastNameThenFirstName.Count / 10;
            int numberOfPages = (int) Math.Ceiling(pages);
            foreach (Person person in sortedByLastNameThenFirstName)
            {
                if (counter % 10 == 0)
                {
                    Console.Clear();
                    tenCounter++;
                }
                Console.WriteLine($"Navn: {person.LastName}, {person.FirstName}\tPerson ID: {person.PersonID}\tEmail: {person.PhoneNumber}\tTelefonnummer: {person.Email}");
                if (counter + 1 == sortedByLastNameThenFirstName.Count)
                {
                    for (int i = 0; i <= 10 - ((counter + 1) % 10); i++)
                    {
                        Console.WriteLine();
                    }
                }
                if (counter % 10 == 9 || counter + 1 == sortedByLastNameThenFirstName.Count)
                {
                    Console.WriteLine($"\t \t \t \t \t \t \t \t \t SIDE {tenCounter +1 } / {numberOfPages + 1 }");
                    Console.ReadKey();
                }
                counter++;
            }
        }
        public static int GetPersonIDInput()
        {
            int userInput = 0;
            bool isInputvalidated = false;
            while (!isInputvalidated)
            {
                Console.Clear();
                Console.Write("Indtast person ID'et: ");
                if (int.TryParse(Console.ReadLine(), out userInput))
                {
                    isInputvalidated = true;
                }
                else
                {
                    Console.WriteLine("Dette er ikke et tal. Prøv igen!!");
                    Console.ReadKey();
                }
            }
            return userInput;
        }
        public static Person GetNewPerson(List<Person> phoneBook)
        {
            int personID;
            if (phoneBook.Any())
            {
                personID = phoneBook.Max(person => person.PersonID)+1;
            }
            else
            {
                personID = 1000001;
            }
            Person newPerson = new Person(
                personID,
                GetNameInput("Indtast fornavn: "),
                GetNameInput("Indtast efternavn: "),
                GetEmailInput(),
                GetPhoneNumberInput("Indtast telefonnummer: ")
            );
            return newPerson;
        }
        /// <summary>
        /// Getting name from the user. Takes the parameter textToUser, 
        /// where it can be specified what kind of name is wanted.
        /// </summary>
        /// <param name="textToUser"></param>
        /// <returns></returns>
        public static string GetNameInput(string textToUser)
        {
            char[] validCharacters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'æ', 'ø', 'å' };
            bool isInputvalidated = false;
            string userInput = "";
            while (!isInputvalidated)
            {
                Console.Clear();
                Console.Write(textToUser);
                userInput = Console.ReadLine();
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine("Det skal være et rigtigt navn. Prøv igen!!");
                    Console.ReadKey();
                    continue;
                }
                bool isAllCharactersValid = true;
                foreach (char c in userInput.ToLower())
                {
                    if (!validCharacters.Contains(c))
                    {
                        Console.WriteLine("Navnet må kun bestå af bogstaver. Prøv igen!!");
                        Console.ReadKey();
                        isAllCharactersValid = false;
                        break;
                    }
                }
                if (!isAllCharactersValid)
                {
                    continue;
                }
                if (!char.IsUpper(userInput[0]))
                {
                    Console.WriteLine("Navnet skal starte med stort. Prøv igen!!");
                    Console.ReadKey();
                    continue;
                }
                bool isAllCharactersLowerAfterTheFirst = true;
                for (int i = 1; i < userInput.Length; i++)
                {
                    if (!char.IsLower(userInput[i]))
                    {
                        Console.WriteLine("Navnet skal have små bogstaver efter det første. Prøv igen!!");
                        Console.ReadKey();
                        isAllCharactersLowerAfterTheFirst = false;
                        break;
                    }
                }
                if (!isAllCharactersLowerAfterTheFirst)
                {
                    continue;
                }
                break;
            }
            return userInput;
        }
        /// <summary>
        /// Getting phone number from the user.
        /// </summary>
        /// <param name="textToUser"></param>
        /// <returns></returns>
        public static int GetPhoneNumberInput(string textToUser)
        {
            int userInput = 0;
            string userInputAsString;
            bool isInputvalidated = false;
            while (!isInputvalidated)
            {
                Console.Clear();
                Console.Write(textToUser);
                userInputAsString = Console.ReadLine();
                if (userInputAsString.Length != 8)
                {
                    Console.WriteLine("Tallet skal være 8 cifre. Prøv igen!!");
                    Console.ReadKey();
                    continue;
                }
                if (!int.TryParse(userInputAsString, out userInput))
                {
                    Console.WriteLine("Tallet må kun bestå af tal. Prøv igen!!");
                    Console.ReadKey();
                    continue;
                };
                isInputvalidated = true;
            }
            return userInput;
        }
        public static string GetEmailInput()
        {
            string userInputEmail = "";
            bool isInputvalidated = false;
            while (!isInputvalidated)
            {
                Console.Clear();
                Console.Write("Indtast email: ");
                userInputEmail = Console.ReadLine();
                if (IsEmailValid(userInputEmail))
                {
                    isInputvalidated = true;
                }
                else
                {
                    Console.WriteLine("Dette er ikke en valid email. Prøv igen!!");
                    Console.ReadKey();
                };
            }
            return userInputEmail;
        }
        public static bool IsEmailValid(string email)
        {
            if (email.Contains('@'))
            {
                if (email[0] != '@')
                {
                    for (int i = email.Length - 1; i > -1; i--)
                    {
                        if (email[i] == '.' && i != email.Length - 1)
                        {
                            return true;
                        }
                        else if (email[i] == '.' && i == email.Length - 1)
                        {
                            return false;
                        }
                        else if (email[i] == '@')
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static void SerializePersons(List<Person> persons)
        {
            string jsonString = JsonSerializer.Serialize(persons, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("phoneBook.json", jsonString);
        }
        static List<Person> DeserializePersons()
        {
            string jsonString = File.ReadAllText("phoneBook.json");
            return JsonSerializer.Deserialize<List<Person>>(jsonString);
        }
    }
}