Console.Clear();
Console.WriteLine("Prezado investidor, digite o seu nome: ");
string? nomeInvestidor = Console.ReadLine();

Console.WriteLine($"""
Bem-vindo ao PagBank, sr(a) {nomeInvestidor}.
Qual o saldo que deseja investir?
""");
double saldoInvestimento = Convert.ToDouble(Console.ReadLine());

saldoInvestimento *= (1 - 0.05/100);

Console.WriteLine($"O valor investido é de: R$ {saldoInvestimento.ToString("0.00")}");

Console.WriteLine("""
Deseja sacar dinheiro do seu investimento? 
1-Sim
2-Não
""");
int desejaSacar = Convert.ToInt16(Console.ReadLine());
Console.Clear();

switch (desejaSacar)
{
    case (1):
        Console.WriteLine("Qual o valor do seu investimento que deseja sacar?");
        double valorSaque = Convert.ToDouble(Console.ReadLine());
        if (valorSaque > saldoInvestimento)
        {
            Console.WriteLine($"""
                O valor do saque é maior do que o disponível em seu investimento.
                Valor investido: R$ {saldoInvestimento.ToString("0.00")}.
                Valor do saque: R$ {valorSaque.ToString("0.00")}.
            Tente novamente.
            """);
        } 
        else 
        {
            saldoInvestimento -= valorSaque;
            Console.WriteLine($"""
                Valor sacado com sucesso.
                Valor investido: R$ {saldoInvestimento.ToString("0.00")}.
                Valor do saque: R$ {valorSaque.ToString("0.00")}.
            """);
        }
        break;
    default:
        Console.WriteLine($"""Prezado(a) {nomeInvestidor}, tenha um ótimo dia!""");
        break;
}