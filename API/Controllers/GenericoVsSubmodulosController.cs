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

public class GenericoVsSubmodulosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenericoVsSubmodulosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GenericoVsSubmodulosDto>>> Get()
    {
        var genericoVsSubmodulos = await _unitOfWork.GenericoVsSubmodulos.GetAllAsync();
        return _mapper.Map<List<GenericoVsSubmodulosDto>>(genericoVsSubmodulos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenericoVsSubmodulosDto>> Get(int id)
    {
        var genericoVsSubmodulos = await _unitOfWork.GenericoVsSubmodulos.GetByIdAsync(id);
        if (genericoVsSubmodulos == null)
        {
            return NotFound();
        }
        return _mapper.Map<GenericoVsSubmodulosDto>(genericoVsSubmodulos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenericoVsSubmodulosDto>> Post(GenericoVsSubmodulosDto genericoVsSubmodulosDto)
    {
        var genericoVsSubmodulos = _mapper.Map<GenericoVsSubmodulos>(genericoVsSubmodulosDto);
        if (genericoVsSubmodulosDto.FechaCreacion == DateOnly.MinValue)
        {
            genericoVsSubmodulosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            genericoVsSubmodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (genericoVsSubmodulosDto.FechaModificacion == DateOnly.MinValue)
        {
            genericoVsSubmodulosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            genericoVsSubmodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.GenericoVsSubmodulos.Add(genericoVsSubmodulos);
        await _unitOfWork.SaveAsync();
        if (genericoVsSubmodulosDto == null)
        {
            return BadRequest();
        }
        genericoVsSubmodulosDto.Id = genericoVsSubmodulos.Id;
        return CreatedAtAction(nameof(Post), new { id = genericoVsSubmodulosDto.Id }, genericoVsSubmodulosDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GenericoVsSubmodulosDto>> Put(int id, [FromBody] GenericoVsSubmodulosDto genericoVsSubmodulosDto)
    {
        if (genericoVsSubmodulosDto.Id == 0)
        {
            genericoVsSubmodulosDto.Id = id;
        }
        if (genericoVsSubmodulosDto.Id != id)
        {
            return NotFound();
        }
        var genericoVsSubmodulos = _mapper.Map<GenericoVsSubmodulos>(genericoVsSubmodulosDto);
        if (genericoVsSubmodulosDto.FechaCreacion == DateOnly.MinValue)
        {
            genericoVsSubmodulosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            genericoVsSubmodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (genericoVsSubmodulosDto.FechaModificacion == DateOnly.MinValue)
        {
            genericoVsSubmodulosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            genericoVsSubmodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        genericoVsSubmodulosDto.Id = genericoVsSubmodulos.Id;
        _unitOfWork.GenericoVsSubmodulos.Update(genericoVsSubmodulos);
        await _unitOfWork.SaveAsync();
        return genericoVsSubmodulosDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var genericoVsSubmodulos = await _unitOfWork.GenericoVsSubmodulos.GetByIdAsync(id);
        if (genericoVsSubmodulos == null)
        {
            return NotFound();
        }
        _unitOfWork.GenericoVsSubmodulos.Remove(genericoVsSubmodulos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
