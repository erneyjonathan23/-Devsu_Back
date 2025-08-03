using AutoMapper;
using MediatR;
using OP.Prueba.Application.Exceptions;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Cliente.Commands.UpdateClienteCommand
{
    public class UpdateClienteCommand : IRequest<int>
    {
        public int? Id { get; set; } = 0;

        public string? Nombre { get; set; }
        public int? GeneroId { get; set; }
        public int? Edad { get; set; }
        public string? Identificacion { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Contrasena { get; set; }
        public int? EstadoId { get; set; }
    }

    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, int>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cliente> _clienteRepository;
        private readonly IMapper _mapper;

        public UpdateClienteCommandHandler(IRepositoryAsync<Domain.Entities.Cliente> clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Cliente? cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null)
                throw new ApiException($"Cliente no encontrado con el id {request.Id}");

            _mapper.Map(request, cliente);

            await _clienteRepository.UpdateAsync(cliente);

            return cliente.PersonaId;
        }
    }
}