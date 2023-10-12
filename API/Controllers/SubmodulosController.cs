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

public class SubmodulosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubmodulosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SubmodulosDto>>> Get()
    {
        var submodulos = await _unitOfWork.Submodulos.GetAllAsync();
        return _mapper.Map<List<SubmodulosDto>>(submodulos);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubmodulosDto>> Get(int Id)
    {
        var submodulos = await _unitOfWork.Submodulos.GetByIdAsync(Id);
        if (submodulos == null)
        {
            return NotFound();
        }
        return _mapper.Map<SubmodulosDto>(submodulos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SubmodulosDto>> Post(SubmodulosDto submodulosDto)
    {
        var submodulos = _mapper.Map<Submodulos>(submodulosDto);
        if (submodulosDto.FechaCreacion == DateOnly.MinValue)
        {
            submodulosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            submodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (submodulosDto.FechaModificacion == DateOnly.MinValue)
        {
            submodulosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            submodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Submodulos.Add(submodulos);
        await _unitOfWork.SaveAsync();
        if (submodulosDto == null)
        {
            return BadRequest();
        }
        submodulosDto.Id = submodulos.Id;
        return CreatedAtAction(nameof(Post), new { id = submodulosDto.Id }, submodulosDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubmodulosDto>> Put(int id, [FromBody] SubmodulosDto submodulosDto)
    {
        if (submodulosDto.Id == 0)
        {
            submodulosDto.Id = id;
        }
        if (submodulosDto.Id != id)
        {
            return NotFound();
        }
        var submodulos = _mapper.Map<Submodulos>(submodulosDto);
        if (submodulosDto.FechaCreacion == DateOnly.MinValue)
        {
            submodulosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            submodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (submodulosDto.FechaModificacion == DateOnly.MinValue)
        {
            submodulosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            submodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        submodulosDto.Id = submodulos.Id;
        _unitOfWork.Submodulos.Update(submodulos);
        await _unitOfWork.SaveAsync();
        return submodulosDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var submodulos = await _unitOfWork.Submodulos.GetByIdAsync(id);
        if (submodulos == null)
        {
            return NotFound();
        }
        _unitOfWork.Submodulos.Remove(submodulos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
