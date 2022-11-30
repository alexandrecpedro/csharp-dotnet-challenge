/** COMMANDS **/
// (1) dotnet run - Run the application
// (2) dotnet build - Compile the application
// (3) dotnet watch run - Compile and run

/** PSEUDOCODE **/
// Part 1 => show() - Show at screen
// Part 2 => read() - Require an information from user (enter by screen)

Console.WriteLine("==========================");
Console.WriteLine("Welcome to my first system");
Console.WriteLine("==========================");

/** VARIABLES **/
// Data Types - C#
Console.WriteLine("Enter an information: ");
var information = Console.ReadLine();

Console.WriteLine($"""
    Entered value: {information}
""");

Console.WriteLine("==========[Ending system]==========");

// Console.WriteLine("====== EXERCISE 1 ======");
// Console.WriteLine("Register names")
// Console.WriteLine("Enter a name: ");
// string? name1 = Console.ReadLine();
// Console.WriteLine("Enter a name: ");
// string? name2 = Console.ReadLine();
// Console.WriteLine("Enter a name: ");
// string? name3 = Console.ReadLine();

// Console.WriteLine("Register last names")
// Console.WriteLine($"""Enter a last name from {name1}: """);
// var lastName1 = Console.ReadLine();
// Console.WriteLine($"""Enter a last name from {name2}: """);
// var lastName2 = Console.ReadLine();
// Console.WriteLine($"""Enter a last name from {name3}: """);
// var lastName3 = Console.ReadLine();

// Console.WriteLine($"""
//     Name 1: {name1} {lastName1}
//     Name 2: {name2} {lastName2}
//     Name 3: {name3} {lastName3}
// """);

/** MATHEMATICAL OPERATIONS **/
int number1 = 1;
int number2 = 20;
var sum = number1 + number2;
Console.WriteLine($""" The sum is: {sum} """);

double number3 = 100.50;
double number4 = 20.5;
var division = number3 / number4;
Console.WriteLine($""" The division is: {division.ToString("0.00")} """);

string number5 = "10.50";
double number6 = 2.5;
var sum2 = Convert.ToDouble(number5) + number6;
// var sum2 = double.Parse(number5) + number6;
Console.WriteLine($""" The sum is: {sum2} """);
