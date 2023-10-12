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

public class ModuloNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModuloNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ModuloNotificacionDto>>> Get()
    {
        var moduloNotificacion = await _unitOfWork.ModuloNotificaciones.GetAllAsync();
        return _mapper.Map<List<ModuloNotificacionDto>>(moduloNotificacion);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ModuloNotificacionDto>> Get(int Id)
    {
        var moduloNotificacion = await _unitOfWork.ModuloNotificaciones.GetByIdAsync(Id);
        if (moduloNotificacion == null)
        {
            return NotFound();
        }
        return _mapper.Map<ModuloNotificacionDto>(moduloNotificacion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ModuloNotificacionDto>> Post(ModuloNotificacionDto moduloNotificacionDto)
    {
        var moduloNotificacion = _mapper.Map<ModuloNotificacion>(moduloNotificacionDto);
        if (moduloNotificacionDto.FechaCreacion == DateOnly.MinValue)
        {
            moduloNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            moduloNotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (moduloNotificacionDto.FechaModificacion == DateOnly.MinValue)
        {
            moduloNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            moduloNotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.ModuloNotificaciones.Add(moduloNotificacion);
        await _unitOfWork.SaveAsync();
        if (moduloNotificacionDto == null)
        {
            return BadRequest();
        }
        moduloNotificacionDto.Id = moduloNotificacion.Id;
        return CreatedAtAction(nameof(Post), new { id = moduloNotificacionDto.Id }, moduloNotificacionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModuloNotificacionDto>> Put(int id, [FromBody] ModuloNotificacionDto moduloNotificacionDto)
    {
        if (moduloNotificacionDto.Id == 0)
        {
            moduloNotificacionDto.Id = id;
        }
        if (moduloNotificacionDto.Id != id)
        {
            return NotFound();
        }
        var moduloNotificacion = _mapper.Map<ModuloNotificacion>(moduloNotificacionDto);
        if (moduloNotificacionDto.FechaCreacion == DateOnly.MinValue)
        {
            moduloNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            moduloNotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (moduloNotificacionDto.FechaModificacion == DateOnly.MinValue)
        {
            moduloNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            moduloNotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        moduloNotificacionDto.Id = moduloNotificacion.Id;
        _unitOfWork.ModuloNotificaciones.Update(moduloNotificacion);
        await _unitOfWork.SaveAsync();
        return moduloNotificacionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var moduloNotificacion = await _unitOfWork.ModuloNotificaciones.GetByIdAsync(id);
        if (moduloNotificacion == null)
        {
            return NotFound();
        }
        _unitOfWork.ModuloNotificaciones.Remove(moduloNotificacion);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
