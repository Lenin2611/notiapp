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

public class permisosGenericosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public permisosGenericosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PermisosGenericosDto>>> Get()
    {
        var permisosGenericos = await _unitOfWork.PermisosGenericos.GetAllAsync();
        return _mapper.Map<List<PermisosGenericosDto>>(permisosGenericos);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PermisosGenericosDto>> Get(int Id)
    {
        var permisosGenericos = await _unitOfWork.PermisosGenericos.GetByIdAsync(Id);
        if (permisosGenericos == null)
        {
            return NotFound();
        }
        return _mapper.Map<PermisosGenericosDto>(permisosGenericos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PermisosGenericosDto>> Post(PermisosGenericosDto permisosGenericosDto)
    {
        var permisosGenericos = _mapper.Map<PermisosGenericos>(permisosGenericosDto);
        if (permisosGenericosDto.FechaCreacion == DateOnly.MinValue)
        {
            permisosGenericosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            permisosGenericos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (permisosGenericosDto.FechaModificacion == DateOnly.MinValue)
        {
            permisosGenericosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            permisosGenericos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.PermisosGenericos.Add(permisosGenericos);
        await _unitOfWork.SaveAsync();
        if (permisosGenericosDto == null)
        {
            return BadRequest();
        }
        permisosGenericosDto.Id = permisosGenericos.Id;
        return CreatedAtAction(nameof(Post), new { id = permisosGenericosDto.Id }, permisosGenericosDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PermisosGenericosDto>> Put(int id, [FromBody] PermisosGenericosDto permisosGenericosDto)
    {
        if (permisosGenericosDto.Id == 0)
        {
            permisosGenericosDto.Id = id;
        }
        if (permisosGenericosDto.Id != id)
        {
            return NotFound();
        }
        var permisosGenericos = _mapper.Map<PermisosGenericos>(permisosGenericosDto);
        if (permisosGenericosDto.FechaCreacion == DateOnly.MinValue)
        {
            permisosGenericosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            permisosGenericos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (permisosGenericosDto.FechaModificacion == DateOnly.MinValue)
        {
            permisosGenericosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            permisosGenericos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        permisosGenericosDto.Id = permisosGenericos.Id;
        _unitOfWork.PermisosGenericos.Update(permisosGenericos);
        await _unitOfWork.SaveAsync();
        return permisosGenericosDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var permisosGenericos = await _unitOfWork.PermisosGenericos.GetByIdAsync(id);
        if (permisosGenericos == null)
        {
            return NotFound();
        }
        _unitOfWork.PermisosGenericos.Remove(permisosGenericos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
