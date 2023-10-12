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

public class RolVsMaestroController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RolVsMaestroController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RolVsMaestroDto>>> Get()
    {
        var rolVsMaestro = await _unitOfWork.RolVsMaestros.GetAllAsync();
        return _mapper.Map<List<RolVsMaestroDto>>(rolVsMaestro);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RolVsMaestroDto>> Get(int Id)
    {
        var rolVsMaestro = await _unitOfWork.RolVsMaestros.GetByIdAsync(Id);
        if (rolVsMaestro == null)
        {
            return NotFound();
        }
        return _mapper.Map<RolVsMaestroDto>(rolVsMaestro);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RolVsMaestroDto>> Post(RolVsMaestroDto rolVsMaestroDto)
    {
        var rolVsMaestro = _mapper.Map<RolVsMaestro>(rolVsMaestroDto);
        if (rolVsMaestroDto.FechaCreacion == DateOnly.MinValue)
        {
            rolVsMaestroDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            rolVsMaestro.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (rolVsMaestroDto.FechaModificacion == DateOnly.MinValue)
        {
            rolVsMaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            rolVsMaestro.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.RolVsMaestros.Add(rolVsMaestro);
        await _unitOfWork.SaveAsync();
        if (rolVsMaestroDto == null)
        {
            return BadRequest();
        }
        rolVsMaestroDto.Id = rolVsMaestro.Id;
        return CreatedAtAction(nameof(Post), new { id = rolVsMaestroDto.Id }, rolVsMaestroDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RolVsMaestroDto>> Put(int id, [FromBody] RolVsMaestroDto rolVsMaestroDto)
    {
        if (rolVsMaestroDto.Id == 0)
        {
            rolVsMaestroDto.Id = id;
        }
        if (rolVsMaestroDto.Id != id)
        {
            return NotFound();
        }
        var rolVsMaestro = _mapper.Map<RolVsMaestro>(rolVsMaestroDto);
        if (rolVsMaestroDto.FechaCreacion == DateOnly.MinValue)
        {
            rolVsMaestroDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            rolVsMaestro.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (rolVsMaestroDto.FechaModificacion == DateOnly.MinValue)
        {
            rolVsMaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            rolVsMaestro.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        rolVsMaestroDto.Id = rolVsMaestro.Id;
        _unitOfWork.RolVsMaestros.Update(rolVsMaestro);
        await _unitOfWork.SaveAsync();
        return rolVsMaestroDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var rolVsMaestro = await _unitOfWork.RolVsMaestros.GetByIdAsync(id);
        if (rolVsMaestro == null)
        {
            return NotFound();
        }
        _unitOfWork.RolVsMaestros.Remove(rolVsMaestro);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
