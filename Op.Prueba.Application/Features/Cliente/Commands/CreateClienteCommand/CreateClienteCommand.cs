using AutoMapper;
using MediatR;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Cliente.Commands.CreateClienteCommand
{
    public class CreateClienteCommand : IRequest<int>
    {
        public string Nombres { get; set; }
        public int GeneroId { get; set; }
        public int Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
    }

    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, int>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cliente> _clienteRepository;
        private readonly IMapper _mapper;

        public CreateClienteCommandHandler(IRepositoryAsync<Domain.Entities.Cliente> clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Cliente newCliente = _mapper.Map<Domain.Entities.Cliente>(request);

            Domain.Entities.Cliente createdCliente = await _clienteRepository.AddAsync(newCliente);

            return createdCliente.PersonaId;
        }
    }
}
