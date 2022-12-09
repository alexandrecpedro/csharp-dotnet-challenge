using System.Collections.Generic;
using Programa.Infra.Interfaces;
using Programa.Models;

namespace Programa.Services;

public class ClienteServico
{
    /* ATTRIBUTES */
    public IPersistence<Cliente> Persistence;

    /* CONSTRUCTOR */
    public ClienteServico(IPersistence<Cliente> persistence) 
    {
        this.Persistence = persistence;
    }

    /* METHODS */
    
}
