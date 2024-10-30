using BasicProgrammingDay5.Exercises;
namespace BasicProgrammingDay5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
            Console.Clear();
            Console.WriteLine("Farvel og tak og halløj. Kom snart igen!!");
            Console.ReadKey();
        }
        public static void MainMenu()
        {
            bool userWantsToExit = false;
            while (!userWantsToExit)
            {
                string userInput = "";
                Console.Clear();
                Console.WriteLine("Halløj og velkommen til en masse spændende programmer..");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Tast [T] for at vælge Den Avancerede Telefonbog.");
                Console.WriteLine("Tast [B] for at vælge Det Spændende Bibliotek.");
                Console.WriteLine("Tast [A] for at afslutte.");
                userInput = Console.ReadLine();
                if (userInput.ToLower() == "t")
                {
                    AdvancedPhonebook.RunPhoneBookProgram();
                }
                else if (userInput.ToLower() == "b")
                {
                    Library.RunLibrary();
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
    }
}
