using Programa.Infra;
using Programa.Models;
using Programa.Services;

var recordLocalPath = Environment.GetEnvironmentVariable("RECORD_LOCAL_DEV_DOTNET7_CHALLENGE") ?? "/tmp";
ClienteServico clienteServico = new ClienteServico(new JsonDriver<Cliente>(recordLocalPath));
ContaCorrenteServico contaCorrenteServico = new ContaCorrenteServico(new JsonDriver<ContaCorrente>(recordLocalPath));

async Task<List<Cliente>> FindAllClientes()
{
    return await clienteServico.Persistence.FindAll();
};

async Task<List<ContaCorrente>> FindAllExtratos()
{
    return await contaCorrenteServico.Persistence.FindAll();
};

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
            await cadastrarCliente();
            break;
        case "2":
            Console.Clear();
            await mostrarContaCorrente();
            break;
        case "3":
            Console.Clear();
            await adicionarCreditoCliente();
            break;
        case "4":
            Console.Clear();
            await fazerDebitoCliente();
            break;
        case "5":
            Console.Clear();
            await mostrarClientes();
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

async Task cadastrarCliente() 
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

    if ((await FindAllClientes()).Count > 0)
    {
        Cliente? cli = (await FindAllClientes()).Find(c => c.Telefone == telefoneCliente);
        if (cli != null)
        {
            mensagem($"""
            Cliente já cadastrado com este telefone: {telefoneCliente}.
            Cadastre novamente!
            """);
            await cadastrarCliente();
        }
    }

    await clienteServico.Persistence.Save(new Cliente{
        ID = id,
        Nome = nomeCliente ?? "[Sem Nome]",
        // Telefone = telefoneCliente.ToString() ?? "[Sem Telefone]",
        // Email = emailCliente.ToString() ?? "[Sem Email]"
        Telefone = telefoneCliente ?? "[Sem Telefone]",
        Email = emailCliente ?? "[Sem Email]"
    });

    mensagem($"""{nomeCliente} cadastrado com sucesso. """);

}

void mensagem(string msg)
{
    Console.Clear();
    Console.WriteLine(msg);
    Thread.Sleep(1500);
}

async Task listarClientesCadastrados()
{
    if ((await FindAllClientes()).Count == 0)
    {
        await menuCadastraClienteSeNaoExiste();
    }

    await mostrarClientes(false, 0, "=================[ Selecione um cliente da lista ]=================");
}

async Task mostrarClientes(
    bool sleep = true, 
    int timerSleep = 2000, 
    string header = "=================[ Lista de clientes ]=================") 
{
    Console.Clear();
    Console.WriteLine(header);

    foreach (var cliente in (await FindAllClientes()))
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

async Task adicionarCreditoCliente()
{
    Console.Clear();
    var cliente = await capturaCliente();

    Console.Clear();
    Console.WriteLine("Digite o valor do crédito: ");
    double credito = Convert.ToDouble(Console.ReadLine());

    await contaCorrenteServico.Persistence.Save(new ContaCorrente{
        ID = Guid.NewGuid().ToString(),
        IdCliente = cliente.ID,
        Valor = credito,
        Data = DateTime.Now
    });

    // Saldo do cliente {cliente[1]}: R$ {saldoCliente(idCliente).ToString("0.00")}
    mensagem($"""
    Créditos adicionado com sucesso ...
    Saldo do cliente {cliente.Nome}: R$ {(await contaCorrenteServico.SaldoCliente(cliente.ID)).ToString("0.00")}
    """);
}

async Task fazerDebitoCliente()
{
    Console.Clear();
    var cliente = await capturaCliente();

    Console.Clear();
    Console.WriteLine("Digite o valor de retirada: ");
    double debito = Convert.ToDouble(Console.ReadLine());

    await contaCorrenteServico.Persistence.Save(new ContaCorrente{
        ID = Guid.NewGuid().ToString(),
        IdCliente = cliente.ID,
        Valor = (-1) * debito,
        Data = DateTime.Now
    });

    mensagem($"""
    Retirada realizada com sucesso ...
    Saldo do cliente {cliente.Nome}: R$ {(await contaCorrenteServico.SaldoCliente(cliente.ID)).ToString("0.00")}
    """);
}

async Task<Cliente> capturaCliente()
{
    await listarClientesCadastrados();
    Console.WriteLine("Digite o ID do cliente");
    var idCliente = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(idCliente))
    {
        mensagem("Id do cliente inválido.");
        Console.Clear();

        await menuCadastraClienteSeNaoExiste();

        return await capturaCliente();
    }
    Cliente? cliente = await clienteServico.Persistence.FindById(idCliente);

    if (cliente == null)
    {
        mensagem("Cliente não encontrado na lista.");
        Thread.Sleep(1000);
        Console.Clear();

        await menuCadastraClienteSeNaoExiste();

        return await capturaCliente();
    }

    return cliente;
}

async Task mostrarContaCorrente()
{
    Console.Clear();

    var clientes = await FindAllClientes();
    var dadosNoExtrato = await FindAllExtratos();
    if (clientes.Count == 0 || dadosNoExtrato.Count == 0)
    {
        mensagem("""
        Não existem clientes ou movimentações em conta corrente.
        Cadastre um cliente e faça crédito em conta.
        """);
        return;
    }

    var cliente = await capturaCliente();

    var contaCorrenteCliente = await contaCorrenteServico.ExtratoCliente(cliente.ID);
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
    R$ {(await contaCorrenteServico.SaldoCliente(cliente.ID, contaCorrenteCliente)).ToString("0.00")}


    """);

    Console.WriteLine("Digite enter para continuar");
    Console.ReadLine();
}

async Task menuCadastraClienteSeNaoExiste()
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
            await cadastrarCliente();
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