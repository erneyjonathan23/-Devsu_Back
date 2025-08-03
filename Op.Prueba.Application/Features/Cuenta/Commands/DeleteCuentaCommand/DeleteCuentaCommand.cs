using MediatR;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Cuenta.Commands.DeleteCuentaCommand
{
    public class DeleteCuentaCommand : IRequest<int>
    {
        public int CuentaId { get; set; }
    }

    public class DeleteCuentaCommandHandler : IRequestHandler<DeleteCuentaCommand, int>
    {
        private readonly IRepositoryAsync<Domain.Entities.Cuenta> _repository;

        public DeleteCuentaCommandHandler(IRepositoryAsync<Domain.Entities.Cuenta> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteCuentaCommand request, CancellationToken cancellationToken)
        {
            var cuenta = await _repository.GetByIdAsync(request.CuentaId);
            if (cuenta == null)
                throw new KeyNotFoundException($"Cuenta con ID {request.CuentaId} no encontrada.");

            await _repository.DeleteAsync(cuenta);
            return cuenta.CuentaId;
        }
    }
}