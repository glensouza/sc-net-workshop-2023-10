/*
this application will display option for user to select rock, paper, scissor, lizard or spock
then the computer will randomly select an option
then the application will display the result of the game
*/

// display the welcome message
Console.WriteLine("Welcome to Rock, Paper, Scissor, Lizard, Spock Game!");
int userChoice = 0;
int computerChoice = 0;

do
{
    // display the options
    Console.WriteLine("Please select one of the following options:");
    Console.WriteLine("1. Rock");
    Console.WriteLine("2. Paper");
    Console.WriteLine("3. Scissor");
    Console.WriteLine("4. Lizard");
    Console.WriteLine("5. Spock");
    Console.WriteLine("6. Exit game");

    // get the user input
    Console.Write("Enter your choice: ");
    string? userInput = string.Empty;
    while (string.IsNullOrEmpty(userInput))
    {
        userInput = Console.ReadLine();

        // validate the user input
        if (string.IsNullOrEmpty(userInput) || (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4" && userInput != "5" && userInput != "6"))
        {
            Console.WriteLine("Invalid input. Please try again.");
            Console.Write("Enter your choice: ");
            userInput = string.Empty;
        }
    }

    // convert the user input to integer
    userChoice = Convert.ToInt32(userInput);

    if (userChoice != 6)
    {
        // generate a random number between 1 and 5
        Random random = new Random();
        computerChoice = random.Next(1, 6);

        switch (userChoice)
        {
            case 1:
                Console.WriteLine("You selected Rock.");
                break;
            case 2:
                Console.WriteLine("You selected Paper.");
                break;
            case 3:
                Console.WriteLine("You selected Scissor.");
                break;
            case 4:
                Console.WriteLine("You selected Lizard.");
                break;
            case 5:
                Console.WriteLine("You selected Spock.");
                break;
        }

        switch (computerChoice)
        {
            case 1:
                Console.WriteLine("Computer selected Rock.");
                break;
            case 2:
                Console.WriteLine("Computer selected Paper.");
                break;
            case 3:
                Console.WriteLine("Computer selected Scissor.");
                break;
            case 4:
                Console.WriteLine("Computer selected Lizard.");
                break;
            case 5:
                Console.WriteLine("Computer selected Spock.");
                break;
        }

        // check the result
        if (userChoice == computerChoice)
        {
            Console.WriteLine("It's a tie.");
        }
        else if (userChoice == 1 && (computerChoice == 3 || computerChoice == 4))
        {
            Console.WriteLine("You won!");
        }
        else if (userChoice == 2 && (computerChoice == 1 || computerChoice == 5))
        {
            Console.WriteLine("You won!");
        }
        else if (userChoice == 3 && (computerChoice == 2 || computerChoice == 4))
        {
            Console.WriteLine("You won!");
        }
        else if (userChoice == 4 && (computerChoice == 2 || computerChoice == 5))
        {
            Console.WriteLine("You won!");
        }
        else if (userChoice == 5 && (computerChoice == 1 || computerChoice == 3))
        {
            Console.WriteLine("You won!");
        }
        else
        {
            Console.WriteLine("You lost!");
        }

        Console.WriteLine("Press Enter to play again");
        Console.ReadLine();
        Console.Clear();
    }
} while (userChoice != 6);
