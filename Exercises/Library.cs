using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static BasicProgrammingDay5.Exercises.AdvancedPhonebook;

namespace BasicProgrammingDay5.Exercises
{
    internal class Library
    {
        public class User
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int PhoneNumber { get; set; }
            public bool IsSuperUser { get; set; }
            public User(string email, string password, string firstName, string lastName, int phoneNumber, bool isSuperUser)
            {
                Email = email;
                Password = password;
                FirstName = firstName;
                LastName = lastName;
                PhoneNumber = phoneNumber;
                IsSuperUser = isSuperUser;
            }
        }
        public static void RunLibrary()
        {
            bool userWantsToExit = false;
            while (!userWantsToExit)
            {
                string userInput = "";
                List<User> users;
                string userEmail;
                Console.Clear();
                Console.WriteLine("Halløj og velkommen til Det Spændende Bibliotek.");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Tast [L] for at logge ind med eksisterende bruger.");
                Console.WriteLine("Tast [O] for at oprette bruger.");
                Console.WriteLine("Tast [A] for at afslutte.");
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "l")
                {
                    Console.Clear();
                    try { users = DeserializeUsers(); }
                    catch { users = new List<User>(); }
                    userEmail = AdvancedPhonebook.GetEmailInput();
                    User user = users.FirstOrDefault(user => user.Email == userEmail);
                    if (user != null)
                    {
                        if (user.Password == GetPasswordInput())
                        {
                            Console.WriteLine("Du er nu logget på!!");
                            Console.ReadKey();
                            // users.Add(LibraryMenu(user));
                            // SerializeUsers(users);
                        }
                        else
                        {
                            Console.WriteLine("Passwordet er forkert, din klovn!!");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Emailen findes ikke i databasen, din klovn!!");
                        Console.ReadKey();
                    }

                }
                else if (userInput.ToLower() == "o")
                {
                    try { users = DeserializeUsers(); }
                    catch { users = new List<User>(); }
                    userEmail = AdvancedPhonebook.GetEmailInput();
                    if (!users.Any(user => user.Email == userEmail))
                    {
                        users.Add(CreateNewUser(userEmail));
                        SerializeUsers(users);
                    }
                    else
                    {
                        Console.WriteLine("Emailaddressen findes allerede i databasen, din idiot!!");
                        Console.ReadKey();
                    }

                }
                else if (userInput.ToLower() == "a")
                {
                    userWantsToExit = true;
                }
                else
                {
                    Console.WriteLine("Forkert input. Prøv igen!!");
                    Console.ReadKey();
                }
            }
        }
        public static User LibraryMenu(User user)
        {
            Console.WriteLine("");
            return user;
        }
        public static User CreateNewUser(string email)
        {
            User newUser = new User(
                email,
                GetPasswordInput(),
                AdvancedPhonebook.GetNameInput("Indtast fornavn: "),
                AdvancedPhonebook.GetNameInput("Indtast efternavn: "),
                GetPhoneNumberInput("Indtast telefonnummer: "),
                isSuperUser()
            );

            return newUser;
        }
        public static string GetPasswordInput()
        {
            string passwordInput = "";
            while (true)
            {
                Console.Clear();
                Console.Write("Indtast password: ");
                passwordInput = Console.ReadLine();
                if (passwordInput.Length > 5 && passwordInput.Length < 25)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Passwordet skal være mere 5 karakterer og mindre end 25 karakterer. Prøv igen!!");
                    Console.ReadKey();
                }
            }
            return passwordInput;
        }
        public static bool isSuperUser()
        {
            bool isSuperUser = false;
            Console.Clear();
            Console.Write("Indtast superbruger kodeordet (det er ikke Passw0rd1234): ");
            string superUserPass = Console.ReadLine();
            if (superUserPass == "Passw0rd1234")
            {
                Console.WriteLine("Tillykke, du er superbruger nu!!");
                isSuperUser = true;
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Du er ikke superbruger, din taber!!");
                Console.ReadKey();
            }
            return isSuperUser;
        }
        public static void SerializeUsers(List<User> persons)
        {
            string jsonString = JsonSerializer.Serialize(persons, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("userList.json", jsonString);
        }
        static List<User> DeserializeUsers()
        {
            string jsonString = File.ReadAllText("userList.json");
            return JsonSerializer.Deserialize<List<User>>(jsonString);
        }
    }
}
