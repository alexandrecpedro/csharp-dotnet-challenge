// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
namespace console;

// [Table(typeof(string))] // C# 10 or lower
[Table<string>] // C# 11 or greater
// namespace Name
// {
    public class Program 
    {
        static void Main(string[] args) 
        {
            var cli = new ClientRecord(0, "", "", "");
            
            // var client = new Client();
            var client = new Client() { CPF = "2e2342" };

            client.NameNotNull("name");
            
            /** VALIDATIONS **/
            // if (client.CPF != null) {}
            if (client.CPF is not null) {}
            // if (client.CPF == null) {}
            if (client.CPF is null) {}

            var i = 0;
            string template = $$"""
                Hello,
                World!

                {
                    "employees": [
                        {"firstName": "John", "lastName": "Doe - {{ i }}"},
                        {"firstName": "Anna", "lastName": "Smith"},
                        {"firstName": "Peter", "lastName": "Jones"}
                    ]
                }
            """;
            // The value of i is: {{i}}
            Console.WriteLine("Hello, World!");
            Console.WriteLine(template);
        }
    }
// }