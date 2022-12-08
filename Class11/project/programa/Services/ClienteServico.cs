using System.Collections.Generic;
using Programa.Models;

namespace Programa.Services;

public class ClienteServico
{
    /* ATTRIBUTES */
    private List<Cliente> contaCorrente = new List<Cliente>();
    private static ClienteServico instancia = default!; // cannot be null

    /* CONSTRUCTOR */
    private ClienteServico() { }

    /* GETTERS/SETTERS */
    public static ClienteServico Get() {
        if (instancia == null) instancia = new ClienteServico();
        return instancia;
    }

    /* METHODS */
    public List<Cliente> Lista = new List<Cliente>();
}
