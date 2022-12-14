using Programa.Models;
using Programa.Services;

while (true)
{
    Console.Clear();

    Console.WriteLine("""
    =================[Seja bem-vindo à empresa Lina]=================
    O que você deseja fazer?
    1 - Cadastrar o cliente
    2 - Ver extrato do cliente
    3 - Crédito em conta
    4 - Retirada
    5 - Mostrar clientes
    6 - Sair do sistema
    """);

    var opcao = Console.ReadLine()?.Trim();
    Console.Clear();
    bool sair = false;

    switch (opcao)
    {
        case "1":
            Console.Clear();
            cadastrarCliente();
            break;
        case "2":
            Console.Clear();
            mostrarContaCorrente();
            break;
        case "3":
            Console.Clear();
            adicionarCreditoCliente();
            break;
        case "4":
            Console.Clear();
            fazerDebitoCliente();
            break;
        case "5":
            Console.Clear();
            mostrarClientes();
            break;
        case "6":
            sair = true;
            break;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }

    if (sair) break;
}

void cadastrarCliente() 
{
    var id = Guid.NewGuid().ToString();

    Console.WriteLine("Informe o nome do cliente:");
    string? nomeCliente = Console.ReadLine();
    Console.Clear();

    Console.WriteLine($"Informe o telefone do cliente {nomeCliente}: ");
    string? telefoneCliente = Console.ReadLine();
    Console.Clear();

    Console.WriteLine($"Informe o email do cliente {nomeCliente}: ");
    string? emailCliente = Console.ReadLine();
    Console.Clear();

    if (ClienteServico.Get().Lista.Count > 0)
    {
        Cliente? cli = ClienteServico.Get().Lista.Find(c => c.Telefone == telefoneCliente);
        if (cli != null)
        {
            mensagem($"""
            Cliente já cadastrado com este telefone: {telefoneCliente}.
            Cadastre novamente!
            """);
            cadastrarCliente();
        }
    }

    ClienteServico.Get().Lista.Add(new Cliente{
        ID = id,
        Nome = nomeCliente ?? "[Sem Nome]",
        Telefone = telefoneCliente.ToString() ?? "[Sem Telefone]",
        Email = emailCliente.ToString() ?? "[Sem Email]"
    });

    mensagem($"""{nomeCliente} cadastrado com sucesso. """);
}

void mensagem(string msg)
{
    Console.Clear();
    Console.WriteLine(msg);
    Thread.Sleep(1500);
}

void listarClientesCadastrados()
{
    if (ClienteServico.Get().Lista.Count == 0)
    {
        menuCadastraClienteSeNaoExiste();
    }

    mostrarClientes(false, 0, "=================[ Selecione um cliente da lista ]=================");
}

void mostrarClientes(
    bool sleep = true, 
    int timerSleep = 2000, 
    string header = "=================[ Lista de clientes ]=================") 
{
    Console.Clear();
    Console.WriteLine(header);

    foreach (var cliente in ClienteServico.Get().Lista)
    {
        Console.WriteLine($"""
        ID: {cliente.ID}
        Nome do cliente: {cliente.Nome}
        Telefone do cliente: {cliente.Telefone}
        Email do cliente: {cliente.Email}
        ----------------------------------
        """);

        if (sleep) 
        {
            Thread.Sleep(timerSleep);
            Console.Clear();
        }
    }
}

void adicionarCreditoCliente()
{
    Console.Clear();
    var cliente = capturaCliente();

    Console.Clear();
    Console.WriteLine("Digite o valor do crédito: ");
    double credito = Convert.ToDouble(Console.ReadLine());

    ContaCorrenteServico.Get().Lista.Add(new ContaCorrente{
        ID = Guid.NewGuid().ToString(),
        IdCliente = cliente.ID,
        Valor = credito,
        Data = DateTime.Now
    });

    // Saldo do cliente {cliente[1]}: R$ {saldoCliente(idCliente).ToString("0.00")}
    mensagem($"""
    Créditos adicionado com sucesso ...
    Saldo do cliente {cliente.Nome}: R$ {ContaCorrenteServico.Get().SaldoCliente(cliente.ID).ToString("0.00")}
    """);
}

void fazerDebitoCliente()
{
    Console.Clear();
    var cliente = capturaCliente();

    Console.Clear();
    Console.WriteLine("Digite o valor de retirada: ");
    double debito = Convert.ToDouble(Console.ReadLine());

    ContaCorrenteServico.Get().Lista.Add(new ContaCorrente{
        ID = Guid.NewGuid().ToString(),
        IdCliente = cliente.ID,
        Valor = (-1) * debito,
        Data = DateTime.Now
    });

    mensagem($"""
    Retirada realizada com sucesso ...
    Saldo do cliente {cliente.Nome}: R$ {ContaCorrenteServico.Get().SaldoCliente(cliente.ID).ToString("0.00")}
    """);
}

dynamic capturaCliente()
{
    listarClientesCadastrados();
    var idCliente = Console.ReadLine()?.Trim();
    Cliente? cliente = ClienteServico.Get().Lista.Find(c => c.ID == idCliente);

    if (cliente == null)
    {
        mensagem("Cliente não encontrado na lista.");
        Thread.Sleep(1000);
        Console.Clear();
        menuCadastraClienteSeNaoExiste();
        return capturaCliente();
    }

    return cliente;
}

void mostrarContaCorrente()
{
    Console.Clear();

    if (ClienteServico.Get().Lista.Count == 0 || ContaCorrenteServico.Get().Lista.Count == 0)
    {
        mensagem("""
        Não existem clientes ou movimentações em conta corrente.
        Cadastre um cliente e faça crédito em conta.
        """);
        return;
    }

    var cliente = capturaCliente();

    var contaCorrenteCliente = ContaCorrenteServico.Get().ExtratoCliente(cliente.ID);
    Console.Clear();
    Console.WriteLine("----------------------------------------");
    foreach (var contaCorrente in contaCorrenteCliente)
    {
        Console.WriteLine($"""
        Data: {contaCorrente.Data.ToString("dd/MM/yyyy HH:mm:ss")}
        Saldo: R$ {contaCorrente.Valor.ToString("0.00")}
        ----------------------------------------
        """);
    }

    Console.WriteLine($"""
    O valor total da conta do cliente {cliente.Nome} é de: 
    R$ {ContaCorrenteServico.Get().SaldoCliente(cliente.ID, contaCorrenteCliente).ToString("0.00")}


    """);

    Console.WriteLine("Digite enter para continuar");
    Console.ReadLine();
}

void menuCadastraClienteSeNaoExiste()
{
    Console.WriteLine("""
    O que você deseja fazer?
    1 - Cadastrar o cliente
    2 - Voltar ao menu
    3 - Sair do sistema
    """);

    var opcao = Console.ReadLine()?.Trim();

    switch (opcao)
    {
        case "1":
            cadastrarCliente();
            break;
        case "2":
            break;
        case "3":
            // Exit application with success = 0
            // Exit application with error = -1
            System.Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }
}