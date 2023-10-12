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

public class TipoRequerimientoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoRequerimientoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoRequerimientoDto>>> Get()
    {
        var tipoRequerimiento = await _unitOfWork.TipoRequerimientos.GetAllAsync();
        return _mapper.Map<List<TipoRequerimientoDto>>(tipoRequerimiento);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoRequerimientoDto>> Get(int Id)
    {
        var tipoRequerimiento = await _unitOfWork.TipoRequerimientos.GetByIdAsync(Id);
        if (tipoRequerimiento == null)
        {
            return NotFound();
        }
        return _mapper.Map<TipoRequerimientoDto>(tipoRequerimiento);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoRequerimientoDto>> Post(TipoRequerimientoDto tipoRequerimientoDto)
    {
        var tipoRequerimiento = _mapper.Map<TipoRequerimiento>(tipoRequerimientoDto);
        if (tipoRequerimientoDto.FechaCreacion == DateOnly.MinValue)
        {
            tipoRequerimientoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            tipoRequerimiento.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (tipoRequerimientoDto.FechaModificacion == DateOnly.MinValue)
        {
            tipoRequerimientoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            tipoRequerimiento.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.TipoRequerimientos.Add(tipoRequerimiento);
        await _unitOfWork.SaveAsync();
        if (tipoRequerimientoDto == null)
        {
            return BadRequest();
        }
        tipoRequerimientoDto.Id = tipoRequerimiento.Id;
        return CreatedAtAction(nameof(Post), new { id = tipoRequerimientoDto.Id }, tipoRequerimientoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoRequerimientoDto>> Put(int id, [FromBody] TipoRequerimientoDto tipoRequerimientoDto)
    {
        if (tipoRequerimientoDto.Id == 0)
        {
            tipoRequerimientoDto.Id = id;
        }
        if (tipoRequerimientoDto.Id != id)
        {
            return NotFound();
        }
        var tipoRequerimiento = _mapper.Map<TipoRequerimiento>(tipoRequerimientoDto);
        if (tipoRequerimientoDto.FechaCreacion == DateOnly.MinValue)
        {
            tipoRequerimientoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            tipoRequerimiento.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (tipoRequerimientoDto.FechaModificacion == DateOnly.MinValue)
        {
            tipoRequerimientoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            tipoRequerimiento.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        tipoRequerimientoDto.Id = tipoRequerimiento.Id;
        _unitOfWork.TipoRequerimientos.Update(tipoRequerimiento);
        await _unitOfWork.SaveAsync();
        return tipoRequerimientoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var tipoRequerimiento = await _unitOfWork.TipoRequerimientos.GetByIdAsync(id);
        if (tipoRequerimiento == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoRequerimientos.Remove(tipoRequerimiento);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
