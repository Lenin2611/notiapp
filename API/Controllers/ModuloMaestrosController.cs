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

public class ModuloMaestrosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModuloMaestrosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ModuloMaestrosDto>>> Get()
    {
        var moduloMaestros = await _unitOfWork.ModuloMaestros.GetAllAsync();
        return _mapper.Map<List<ModuloMaestrosDto>>(moduloMaestros);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ModuloMaestrosDto>> Get(int Id)
    {
        var moduloMaestros = await _unitOfWork.ModuloMaestros.GetByIdAsync(Id);
        if (moduloMaestros == null)
        {
            return NotFound();
        }
        return _mapper.Map<ModuloMaestrosDto>(moduloMaestros);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ModuloMaestrosDto>> Post(ModuloMaestrosDto moduloMaestrosDto)
    {
        var moduloMaestros = _mapper.Map<ModuloMaestros>(moduloMaestrosDto);
        if (moduloMaestrosDto.FechaCreacion == DateOnly.MinValue)
        {
            moduloMaestrosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            moduloMaestros.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (moduloMaestrosDto.FechaModificacion == DateOnly.MinValue)
        {
            moduloMaestrosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            moduloMaestros.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.ModuloMaestros.Add(moduloMaestros);
        await _unitOfWork.SaveAsync();
        if (moduloMaestrosDto == null)
        {
            return BadRequest();
        }
        moduloMaestrosDto.Id = moduloMaestros.Id;
        return CreatedAtAction(nameof(Post), new { id = moduloMaestrosDto.Id }, moduloMaestrosDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ModuloMaestrosDto>> Put(int id, [FromBody] ModuloMaestrosDto moduloMaestrosDto)
    {
        if (moduloMaestrosDto.Id == 0)
        {
            moduloMaestrosDto.Id = id;
        }
        if (moduloMaestrosDto.Id != id)
        {
            return NotFound();
        }
        var moduloMaestros = _mapper.Map<ModuloMaestros>(moduloMaestrosDto);
        if (moduloMaestrosDto.FechaCreacion == DateOnly.MinValue)
        {
            moduloMaestrosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            moduloMaestros.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (moduloMaestrosDto.FechaModificacion == DateOnly.MinValue)
        {
            moduloMaestrosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            moduloMaestros.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        moduloMaestrosDto.Id = moduloMaestros.Id;
        _unitOfWork.ModuloMaestros.Update(moduloMaestros);
        await _unitOfWork.SaveAsync();
        return moduloMaestrosDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var moduloMaestros = await _unitOfWork.ModuloMaestros.GetByIdAsync(id);
        if (moduloMaestros == null)
        {
            return NotFound();
        }
        _unitOfWork.ModuloMaestros.Remove(moduloMaestros);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
