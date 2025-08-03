using AutoMapper;
using MediatR;
using OP.Prueba.Application.Interfaces;

namespace OP.Prueba.Application.Features.Movimiento.Commands.UpdateMovimientoCommand
{
    public class UpdateMovimientoCommand : IRequest<int>
    {
        public int MovimientoId { get; set; }
        public DateTime Fecha { get; set; }
        public int TipoMovimientoId { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }

    public class UpdateMovimientoCommandHandler : IRequestHandler<UpdateMovimientoCommand, int>
    {
        private readonly IRepositoryAsync<Domain.Entities.Movimiento> _repository;
        private readonly IMapper _mapper;

        public UpdateMovimientoCommandHandler(IRepositoryAsync<Domain.Entities.Movimiento> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateMovimientoCommand request, CancellationToken cancellationToken)
        {
            var movimiento = await _repository.GetByIdAsync(request.MovimientoId);
            if (movimiento == null)
                throw new KeyNotFoundException($"Movimiento con ID {request.MovimientoId} no encontrado.");

            _mapper.Map(request, movimiento);
            await _repository.UpdateAsync(movimiento);
            return movimiento.MovimientoId;
        }
    }
}