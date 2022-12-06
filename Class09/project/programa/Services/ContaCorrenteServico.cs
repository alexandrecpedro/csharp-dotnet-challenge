using System.Collections.Generic;
using System.Linq;
using Programa.Models;

namespace Programa.Services;

public class ContaCorrenteServico
{
    /* ATTRIBUTES */
    private List<ContaCorrente> contaCorrente = new List<ContaCorrente>();
    private static ContaCorrenteServico instancia = default!; // cannot be null

    /* CONSTRUCTOR */
    private ContaCorrenteServico() { }

    /* GETTERS/SETTERS */
    public static ContaCorrenteServico Get() {
        if (instancia == null) instancia = new ContaCorrenteServico();
        return instancia;
    }

    /* METHODS */
    public List<ContaCorrente> Lista = new List<ContaCorrente>();

    public List<ContaCorrente> ExtratoCliente(string idCliente)
    {
        var contaCorrenteCliente = this.Lista.FindAll(cc => cc.IdCliente == idCliente);
        if (contaCorrenteCliente.Count == 0) return new List<ContaCorrente>();

        return contaCorrenteCliente;
    }

    public double SaldoCliente(string idCliente, List<ContaCorrente>? contaCorrenteCliente = null)
    {
        // contaCorrenteCliente = contaCorrente.FindAll(cc => cc[0] == idCliente);

        if (contaCorrenteCliente == null)
            contaCorrenteCliente = ExtratoCliente(idCliente);

        if (contaCorrenteCliente.Count == 0) return 0;
        
        // double soma = 0;
        // foreach (var cc in contaCorrenteCliente)
        // {
        //     soma += Convert.ToDouble(cc.Valor);
        // }

        // return soma;

        return contaCorrenteCliente.Sum(cc => cc.Valor);
    }
}
