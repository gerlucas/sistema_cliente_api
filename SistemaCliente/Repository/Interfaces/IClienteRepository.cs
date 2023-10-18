using SistemaCliente.Models;

namespace SistemaCliente.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<ClienteModel>> BuscarTodosClientes();
        Task<ClienteModel> BuscarClienteId(int id);
        Task<ClienteModel> AdicionarCliente(ClienteModel cliente);
        Task <ClienteModel> Atualizar(ClienteModel cliente, int id);
        ClienteModel GerarBoleto();
        Task<bool> Deletar(int id); 
    }
}
