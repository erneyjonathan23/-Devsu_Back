using MediatR;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Movimiento.Commands.DeleteMovimientoCommand
{
    public class DeleteMovimientoCommand : IRequest<int>
    {
        public int MovimientoId { get; set; }
    }

    public class DeleteMovimientoCommandHandler : IRequestHandler<DeleteMovimientoCommand, int>
    {
        private readonly IRepositoryAsync<Domain.Entities.Movimiento> _repository;

        public DeleteMovimientoCommandHandler(IRepositoryAsync<Domain.Entities.Movimiento> repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteMovimientoCommand request, CancellationToken cancellationToken)
        {
            var movimiento = await _repository.GetByIdAsync(request.MovimientoId);
            if (movimiento == null)
                throw new KeyNotFoundException($"Movimiento con ID {request.MovimientoId} no encontrado.");

            await _repository.DeleteAsync(movimiento);
            return movimiento.MovimientoId;
        }
    }
}