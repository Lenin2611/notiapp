using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles() // Remember adding : Profile in the class
    {
        CreateMap<Auditoria, AuditoriaDto>().ReverseMap();

        CreateMap<BlockChain, BlockChainDto>().ReverseMap();

        CreateMap<EstadoNotificacion, EstadoNotificacionDto>().ReverseMap();

        CreateMap<Formatos, FormatosDto>().ReverseMap();

        CreateMap<GenericoVsSubmodulos, GenericoVsSubmodulosDto>().ReverseMap();

        CreateMap<HiloRespuestaNotificacion, HiloRespuestaNotificacionDto>().ReverseMap();

        CreateMap<MaestrosVsSubmodulos, MaestrosVsSubmodulosDto>().ReverseMap();

        CreateMap<ModuloMaestros, ModuloMaestrosDto>().ReverseMap();

        CreateMap<ModuloNotificacion, ModuloNotificacionDto>().ReverseMap();

        CreateMap<PermisosGenericos, PermisosGenericosDto>().ReverseMap();

        CreateMap<Radicados, RadicadosDto>().ReverseMap();

        CreateMap<Rol, RolDto>().ReverseMap();

        CreateMap<RolVsMaestro, RolVsMaestroDto>().ReverseMap();

        CreateMap<Submodulos, SubmodulosDto>().ReverseMap();

        CreateMap<TipoNotificaciones, TipoNotificacionesDto>().ReverseMap();

        CreateMap<TipoRequerimiento, TipoRequerimientoDto>().ReverseMap();
    }
}
