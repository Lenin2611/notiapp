using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class HiloRespuestaNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HiloRespuestaNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<HiloRespuestaNotificacionDto>>> Get()
    {
        var hiloRespuestaNotificacion = await _unitOfWork.HiloRespuestaNotificaciones.GetAllAsync();
        return _mapper.Map<List<HiloRespuestaNotificacionDto>>(hiloRespuestaNotificacion);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Get(int Id)
    {
        var hiloRespuestaNotificacion = await _unitOfWork.HiloRespuestaNotificaciones.GetByIdAsync(Id);
        if (hiloRespuestaNotificacion == null)
        {
            return NotFound();
        }
        return _mapper.Map<HiloRespuestaNotificacionDto>(hiloRespuestaNotificacion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Post(HiloRespuestaNotificacionDto hiloRespuestaNotificacionDto)
    {
        var hiloRespuestaNotificacion = _mapper.Map<HiloRespuestaNotificacion>(hiloRespuestaNotificacionDto);
        if (hiloRespuestaNotificacionDto.FechaCreacion == DateOnly.MinValue)
        {
            hiloRespuestaNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            hiloRespuestaNotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (hiloRespuestaNotificacionDto.FechaModificacion == DateOnly.MinValue)
        {
            hiloRespuestaNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            hiloRespuestaNotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.HiloRespuestaNotificaciones.Add(hiloRespuestaNotificacion);
        await _unitOfWork.SaveAsync();
        if (hiloRespuestaNotificacionDto == null)
        {
            return BadRequest();
        }
        hiloRespuestaNotificacionDto.Id = hiloRespuestaNotificacion.Id;
        return CreatedAtAction(nameof(Post), new { id = hiloRespuestaNotificacionDto.Id }, hiloRespuestaNotificacionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Put(int id, [FromBody] HiloRespuestaNotificacionDto hiloRespuestaNotificacionDto)
    {
        if (hiloRespuestaNotificacionDto.Id == 0)
        {
            hiloRespuestaNotificacionDto.Id = id;
        }
        if (hiloRespuestaNotificacionDto.Id != id)
        {
            return NotFound();
        }
        var hiloRespuestaNotificacion = _mapper.Map<HiloRespuestaNotificacion>(hiloRespuestaNotificacionDto);
        if (hiloRespuestaNotificacionDto.FechaCreacion == DateOnly.MinValue)
        {
            hiloRespuestaNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            hiloRespuestaNotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (hiloRespuestaNotificacionDto.FechaModificacion == DateOnly.MinValue)
        {
            hiloRespuestaNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            hiloRespuestaNotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        hiloRespuestaNotificacionDto.Id = hiloRespuestaNotificacion.Id;
        _unitOfWork.HiloRespuestaNotificaciones.Update(hiloRespuestaNotificacion);
        await _unitOfWork.SaveAsync();
        return hiloRespuestaNotificacionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var hiloRespuestaNotificacion = await _unitOfWork.HiloRespuestaNotificaciones.GetByIdAsync(id);
        if (hiloRespuestaNotificacion == null)
        {
            return NotFound();
        }
        _unitOfWork.HiloRespuestaNotificaciones.Remove(hiloRespuestaNotificacion);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
