using AutoMapper;
using OP.Prueba.Application.Features.Cliente.Commands.CreateClienteCommand;
using OP.Prueba.Application.Features.Cliente.Commands.UpdateClienteCommand;
using OP.Prueba.Application.Features.Cuenta.Commands.CreateCuentaCommand;
using OP.Prueba.Application.Features.Cuenta.Commands.UpdateCuentaCommand;
using OP.Prueba.Application.Features.Movimiento.Commands.CreateMovimientoCommand;
using OP.Prueba.Application.Features.Movimiento.Commands.UpdateMovimientoCommand;
using OP.Prueba.Domain.Entities;

namespace OP.Prueba.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<CreateClienteCommand, Cliente>();
            CreateMap<UpdateClienteCommand, Cliente>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreateCuentaCommand, Cuenta>();
            CreateMap<UpdateCuentaCommand, Cuenta>();
            //CreateMap<Cuenta, Cuenta>()
            //    .ForMember(dest => dest.TipoCuenta, opt => opt.MapFrom(src => src.TipoCuenta.Descripcion))
            //    .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado.Descripcion));

            CreateMap<CreateMovimientoCommand, Movimiento>();
            CreateMap<UpdateMovimientoCommand, Movimiento>();
            //CreateMap<Movimiento, MovimientoDto>()
            //    .ForMember(dest => dest.TipoMovimiento, opt => opt.MapFrom(src => src.TipoMovimiento.Descripcion));
        }
    }
}