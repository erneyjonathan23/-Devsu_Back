using MediatR;
using OP.Prueba.Application.Exceptions;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Cliente.Commands.DeleteClienteCommand
{
    public class DeleteClienteCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, int>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cliente> _clienteRepository;

        public DeleteClienteCommandHandler(IRepositoryAsync<Domain.Entities.Cliente> clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<int> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Cliente? cliente = await _clienteRepository.GetByIdAsync(request.Id);
            if (cliente == null)
                throw new ApiException($"Cliente no encontrado con el id {request.Id}");

            await _clienteRepository.DeleteAsync(cliente);

            return cliente.PersonaId;
        }
    }
}