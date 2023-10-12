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

public class RadicadosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RadicadosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RadicadosDto>>> Get()
    {
        var radicados = await _unitOfWork.Radicados.GetAllAsync();
        return _mapper.Map<List<RadicadosDto>>(radicados);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RadicadosDto>> Get(int Id)
    {
        var radicados = await _unitOfWork.Radicados.GetByIdAsync(Id);
        if (radicados == null)
        {
            return NotFound();
        }
        return _mapper.Map<RadicadosDto>(radicados);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RadicadosDto>> Post(RadicadosDto radicadosDto)
    {
        var radicados = _mapper.Map<Radicados>(radicadosDto);
        if (radicadosDto.FechaCreacion == DateOnly.MinValue)
        {
            radicadosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            radicados.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (radicadosDto.FechaModificacion == DateOnly.MinValue)
        {
            radicadosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            radicados.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Radicados.Add(radicados);
        await _unitOfWork.SaveAsync();
        if (radicadosDto == null)
        {
            return BadRequest();
        }
        radicadosDto.Id = radicados.Id;
        return CreatedAtAction(nameof(Post), new { id = radicadosDto.Id }, radicadosDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RadicadosDto>> Put(int id, [FromBody] RadicadosDto radicadosDto)
    {
        if (radicadosDto.Id == 0)
        {
            radicadosDto.Id = id;
        }
        if (radicadosDto.Id != id)
        {
            return NotFound();
        }
        var radicados = _mapper.Map<Radicados>(radicadosDto);
        if (radicadosDto.FechaCreacion == DateOnly.MinValue)
        {
            radicadosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            radicados.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (radicadosDto.FechaModificacion == DateOnly.MinValue)
        {
            radicadosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            radicados.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        radicadosDto.Id = radicados.Id;
        _unitOfWork.Radicados.Update(radicados);
        await _unitOfWork.SaveAsync();
        return radicadosDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var radicados = await _unitOfWork.Radicados.GetByIdAsync(id);
        if (radicados == null)
        {
            return NotFound();
        }
        _unitOfWork.Radicados.Remove(radicados);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
