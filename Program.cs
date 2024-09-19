using System.Dynamic;

namespace NumbersGame
{
    internal class Program
    {
        
        //method for the gameplayloop
        static void GameStart(int diff)
        {
            
            bool gameLoop = true;
            Random random = new Random();
            int number = diff == 1 ? random.Next(1, 10) :
                diff == 2 ? random.Next(1,20) : random.Next(1,30);
            int lives = 5;
            if (diff == 3)
                lives = 4;
            while (gameLoop)
            {

               //3 different strings depending on difficulty to give users the range to guess and number of lives

                string guesser = diff == 1 ? $"Jag tänker på ett nummer mellan 1-10. Kan du gissa vilket? Du har {lives} försök." :
                    diff == 2 ? $"Jag tänker på ett nummer mellan 1-20. Kan du gissa vilket? Du har {lives} försök." :
                    $"Jag tänker på ett nummer mellan 1-30. Kan du gissa vilket? Du har {lives} försök.";
                Console.WriteLine(guesser);
                int userGuess = 0;
                int.TryParse(Console.ReadLine(), out userGuess);
                   
                //int answer = AnswerCheck(number, userGuess);
                int answer = GuessTheNumber(userGuess, random, number);
                
                // GuessTheNumber returns 3 if user guessed correctly
                if (answer == 3)
                {
                    gameLoop = false;
                    Console.WriteLine($"Du hade hela {lives} liv kvar");
                    Console.WriteLine("tryck Enter för att återgå till menyn");
                    Console.ReadLine();
                    break;
                }
                lives--;
                if (lives == 0)
                {
                    gameLoop = false;
                    Console.WriteLine("Aj då nu tog liven slut!");
                    Console.WriteLine("Tryck Enter för att återgå till huvudmenyn");
                    Console.ReadLine();
                }

            }
        }
        
        static int GuessTheNumber(int userGuess, Random random, int number)
        {
            //method for response to the input from user
            //using a few different answers for variation, by using random.next
            //note we dont make a new instance of random since its seeded by using a timestap from the local machine
            //if we make a new random for every guess there is a risk that we get the same number generated multiple times.
            //kom ihåg att lägga in något om svaret är mindre ller högre, lägg in math.abs här och nå + - i strängen
            string[] answers = [ "Oj Denna gång var du inte nära!", "oj Det var långt ifrån", "Inte ens nära",
                "Det bränns", "Nu är du nära", "Inte riktigt men nära!",
                "Gratulerar! du gissade rätt", "Grattis du hade rätt","Grattis du vann!" ];

            //we substract the number user guesses with the number generated, first we check if its negativ or positive and save it
            //then we use Math.Abs that converts it to a positive number
            userGuess = userGuess - number;
            string highLow = (userGuess < 0) ? "Du var lägre, " : "Du var högre, ";
            userGuess = Math.Abs(userGuess);

            // we then check how how close we are and set the result to answer, we then use answer to get the right phrase from the array
            int answer = (userGuess > 5) ? 1 : 2;
            if (userGuess == 0)
            {
                answer = 3;
                Console.WriteLine(answers[random.Next(6, 9)]);
                return answer;
            }

            if (answer == 1)
            {
                Console.WriteLine(highLow+answers[random.Next(0, 3)]);
                
            }
            else if (answer == 2)
            { 
                Console.WriteLine(highLow+answers[random.Next(3, 6)]);
            }
            else
                Console.WriteLine(answers[random.Next(6, 9)]);
            return 0;
        }

      
    static int DiffMenu (int diff)
        {
            //method for difficulty menu, 
            int setDiff = 0;
            bool diffmenu = true;
            while (diffmenu)
            {
                Console.WriteLine("Välj svårighetsgrad");
                Console.WriteLine("[1] Lätt");
                Console.WriteLine("[2] Medium");
                Console.WriteLine("[3] Svårt");
                int.TryParse(Console.ReadLine(), out setDiff);
                if (setDiff < 4 && setDiff > 0)
                    diffmenu = false;
                return setDiff;
            }
            return setDiff;
        }
        
        //main method that handles main meny with a while loop and switch
        static void Main(string[] args)
        {
            int diff = 2;         
            bool mainmenu = true;
            
            while (mainmenu)
            {
                string difficulty = diff == 1 ? "Lätt" :
                diff == 2 ? "Medium" : "Svår";

                Console.Clear();
                Console.WriteLine("Välkommen till Guess the Number!");
                Console.WriteLine($"[1] Starta spelet med svårighetsgrad : {difficulty} ");
                Console.WriteLine("[2] Välj svårighetsgrad");
                Console.WriteLine("[3] Avsluta");
                int.TryParse(Console.ReadLine(),out int menuChoice);
                switch (menuChoice)
                {
                    case 1:
                        GameStart(diff);
                        break;
                    case 2:
                        diff = DiffMenu(diff);
                break;

                    case 3:
                        mainmenu = false;
                        break;
                }
                
            }
        }
    }
}
