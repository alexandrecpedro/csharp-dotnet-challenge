using System.Collections.Generic;
using System.Linq;
using Programa.Infra.Interfaces;
using Programa.Models;

namespace Programa.Services;

public class ContaCorrenteServico
{
    /* ATTRIBUTES */
    public IPersistence<ContaCorrente> Persistence;

    /* CONSTRUCTOR */
    public ContaCorrenteServico(IPersistence<ContaCorrente> persistence) {
        this.Persistence = persistence;
    }

    /* METHODS */
    public async Task<List<ContaCorrente>> ExtratoCliente(string idCliente)
    {
        var contaCorrenteCliente = (await this.Persistence.FindAll()).FindAll(cc => cc.IdCliente == idCliente);
        if (contaCorrenteCliente.Count == 0) return new List<ContaCorrente>();

        return contaCorrenteCliente;
    }

    public async Task<double> SaldoCliente(string idCliente, List<ContaCorrente>? contaCorrenteCliente = null)
    {

        if (contaCorrenteCliente == null)
            contaCorrenteCliente = await ExtratoCliente(idCliente);

        if (contaCorrenteCliente.Count == 0) return 0;

        return contaCorrenteCliente.Sum(cc => cc.Valor);
    }
}
