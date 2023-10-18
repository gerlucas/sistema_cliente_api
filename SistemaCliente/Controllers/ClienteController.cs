using BoletoNet;
using Microsoft.AspNetCore.Mvc;
using SistemaCliente.Models;
using SistemaCliente.Repository;
using SistemaCliente.Repository.Interfaces;


namespace SistemaCliente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;

        }

        //[HttpGet]

        //Chamado de ENDPOINT, no caso estamos usando o GET (vai receber um recurso)

        //public ActionResult<List<ClienteModel> BuscarTodosClientes()
        //{
        //   return Ok();
        //}

        //Reparem que no ENDPOINT acima não retorna nada, somente o codigo 200 (Ok) que é um codigo de sucesso
        //Ele vai retornar uma lista 

        [HttpGet]
        public async Task<ActionResult<List<ClienteModel>>> BuscarTodosClientes()
        {
            List<ClienteModel> clientes = await _clienteRepository.BuscarTodosClientes();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteModel>> BuscarClienteId(int id)
        {
            ClienteModel cliente = await _clienteRepository.BuscarClienteId(id);
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteModel>> AdicionarCliente(ClienteModel clienteModel)
        {
            ClienteModel cliente = await _clienteRepository.AdicionarCliente(clienteModel);
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteModel>> Atualizar([FromBody]ClienteModel clienteModel, int id)
        {
            clienteModel.Id = id;
            ClienteModel cliente = await _clienteRepository.Atualizar(clienteModel, id);
            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteModel>> Deletar(int id)
        {
            bool clienteDelete = await _clienteRepository.Deletar(id);
            return Ok(clienteDelete);
        }


    }

}

