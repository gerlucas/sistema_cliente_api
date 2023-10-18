using BoletoNet;
using Microsoft.EntityFrameworkCore;
using SistemaCliente.Data;
using SistemaCliente.Models;
using SistemaCliente.Repository.Interfaces;

namespace SistemaCliente.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly SistemaDbContext _dbContext;

        public ClienteRepository(SistemaDbContext sistemaDbContext)
        {
            _dbContext = sistemaDbContext;
        }
        public async Task<List<ClienteModel>> BuscarTodosClientes()
        {
            return await _dbContext.Clientes.ToListAsync();
        }

        public async Task<ClienteModel> BuscarClienteId(int id)
        {
            return await _dbContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ClienteModel> AdicionarCliente(ClienteModel cliente)
        {
             await _dbContext.Clientes.AddAsync(cliente);
             await _dbContext.SaveChangesAsync();

            return cliente;
        }

        public async Task<ClienteModel> Atualizar(ClienteModel cliente, int id)
        {
            ClienteModel clienteId = await BuscarClienteId(id);

            if(clienteId == null)
            {
                throw new Exception($"Cliente do ID: {id} não foi encontrado no banco de dados.");
            }

            clienteId.Nome = cliente.Nome;
            clienteId.Email = cliente.Email;
            clienteId.CPF = cliente.CPF;

            _dbContext.Clientes.Update(clienteId);
            await _dbContext.SaveChangesAsync();

            return clienteId;
        }

        public async Task<bool> Deletar(int id)
        {
            ClienteModel clienteId = await BuscarClienteId(id);

            if (clienteId == null)
            {
                throw new Exception($"Cliente do ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Clientes.Remove(clienteId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        public ClienteModel GerarBoleto()
        {
            ClienteModel gerarBoleto = GerarBoleto();

            DateTime vencimento = new DateTime(2020, 6, 14);
            var cedente = new Cedente("00.000.000/0000-00", "Empresa Teste", "0131", "7", "00059127", "0");
            BoletoNet.Boleto boleto = new BoletoNet.Boleto(vencimento, 1700, "17-019", "18204", cedente);
            boleto.NumeroDocumento = "18204";

            var boletoBancario = new BoletoBancario();
            boletoBancario.CodigoBanco = 237;
            boletoBancario.Boleto = boleto;

            return gerarBoleto;
        }
    }
}
