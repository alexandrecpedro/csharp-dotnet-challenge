/** COMMANDS **/
// (1) dotnet run - Run the application
// (2) dotnet build - Compile the application
// (3) dotnet watch run - Compile and run

/** PSEUDOCODE **/
// Part 1 => show() - Show at screen
// Part 2 => read() - Require an information from user (enter by screen)

Console.WriteLine("==========================");
Console.WriteLine("Welcome to my first system");
Console.WriteLine("========================== \n");

/** VARIABLES **/
// Data Types - C#
// Console.WriteLine("Enter an information: ");
// var information = Console.ReadLine();

// Console.WriteLine($"""
//     Entered value: {information}
// """);

// Console.WriteLine("==========[Ending system]==========");

/** MATHEMATICAL OPERATIONS **/
// int number1 = 1;
// int number2 = 20;
// var sum = number1 + number2;
// Console.WriteLine($""" The sum is: {sum} """);

// double number3 = 100.50;
// double number4 = 20.5;
// var division = number3 / number4;
// Console.WriteLine($""" The division is: {division.ToString("0.00")} """);

// string number5 = "10.50";
// double number6 = 2.5;
// var sum2 = Convert.ToDouble(number5) + number6;
// // var sum2 = double.Parse(number5) + number6;
// Console.WriteLine($""" The sum is: {sum2} """);

/** CONVERSIONS **/
// Console.WriteLine("Enter a number");
// double number7 = Convert.ToDouble(Console.ReadLine());

// var result = number7 * 100;
// Console.WriteLine($"The operation result is {result}");

/** CONDITIONALS AND LOGICAL OPERATORS **/
// if else
// Console.WriteLine("Enter a number");
// var number8 = Convert.ToInt32(Console.ReadLine());

// if (number8 >= 18 || number8 >= 16)
// {
//     Console.WriteLine("You have enter a number greater than 18 or greater than 16")
// }
// else if (number8 > 16) 
// {
//     Console.WriteLine("You have enter a number greater than 16 and less than 18")
// }
// else 
// {
//     Console.WriteLine("Invalid option");
// }

// switch
// switch (number8)
// {
//     case 18:
//         Console.WriteLine($"You have entered the number {number8}");
//         break;
//     case 16:
//         Console.WriteLine($"You have entered the number {number8}")
//         break;
//     default:
//         Console.WriteLine("Invalid option");
//         break;
// }