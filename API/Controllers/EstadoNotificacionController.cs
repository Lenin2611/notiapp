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

public class EstadoNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EstadoNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EstadoNotificacionDto>>> Get()
    {
        var estadoNotificacion = await _unitOfWork.EstadoNotificaciones.GetAllAsync();
        return _mapper.Map<List<EstadoNotificacionDto>>(estadoNotificacion);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstadoNotificacionDto>> Get(int Id)
    {
        var estadoNotificacion = await _unitOfWork.EstadoNotificaciones.GetByIdAsync(Id);
        if (estadoNotificacion == null)
        {
            return NotFound();
        }
        return _mapper.Map<EstadoNotificacionDto>(estadoNotificacion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstadoNotificacionDto>> Post(EstadoNotificacionDto estadoNotificacionDto)
    {
        var estadoNotificacion = _mapper.Map<EstadoNotificacion>(estadoNotificacionDto);
        if (estadoNotificacionDto.FechaCreacion == DateOnly.MinValue)
        {
            estadoNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            estadoNotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (estadoNotificacionDto.FechaModificacion == DateOnly.MinValue)
        {
            estadoNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            estadoNotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.EstadoNotificaciones.Add(estadoNotificacion);
        await _unitOfWork.SaveAsync();
        if (estadoNotificacionDto == null)
        {
            return BadRequest();
        }
        estadoNotificacionDto.Id = estadoNotificacion.Id;
        return CreatedAtAction(nameof(Post), new { id = estadoNotificacionDto.Id }, estadoNotificacionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EstadoNotificacionDto>> Put(int id, [FromBody] EstadoNotificacionDto estadoNotificacionDto)
    {
        if (estadoNotificacionDto.Id == 0)
        {
            estadoNotificacionDto.Id = id;
        }
        if (estadoNotificacionDto.Id != id)
        {
            return NotFound();
        }
        var estadoNotificacion = _mapper.Map<EstadoNotificacion>(estadoNotificacionDto);
        if (estadoNotificacionDto.FechaCreacion == DateOnly.MinValue)
        {
            estadoNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            estadoNotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (estadoNotificacionDto.FechaModificacion == DateOnly.MinValue)
        {
            estadoNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            estadoNotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        estadoNotificacionDto.Id = estadoNotificacion.Id;
        _unitOfWork.EstadoNotificaciones.Update(estadoNotificacion);
        await _unitOfWork.SaveAsync();
        return estadoNotificacionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var estadoNotificacion = await _unitOfWork.EstadoNotificaciones.GetByIdAsync(id);
        if (estadoNotificacion == null)
        {
            return NotFound();
        }
        _unitOfWork.EstadoNotificaciones.Remove(estadoNotificacion);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
