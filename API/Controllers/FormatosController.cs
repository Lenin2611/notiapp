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

public class FormatosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FormatosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<FormatosDto>>> Get()
    {
        var formatos = await _unitOfWork.Formatos.GetAllAsync();
        return _mapper.Map<List<FormatosDto>>(formatos);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FormatosDto>> Get(int Id)
    {
        var formatos = await _unitOfWork.Formatos.GetByIdAsync(Id);
        if (formatos == null)
        {
            return NotFound();
        }
        return _mapper.Map<FormatosDto>(formatos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FormatosDto>> Post(FormatosDto formatosDto)
    {
        var formatos = _mapper.Map<Formatos>(formatosDto);
        if (formatosDto.FechaCreacion == DateOnly.MinValue)
        {
            formatosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            formatos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (formatosDto.FechaModificacion == DateOnly.MinValue)
        {
            formatosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            formatos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Formatos.Add(formatos);
        await _unitOfWork.SaveAsync();
        if (formatosDto == null)
        {
            return BadRequest();
        }
        formatosDto.Id = formatos.Id;
        return CreatedAtAction(nameof(Post), new { id = formatosDto.Id }, formatosDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FormatosDto>> Put(int id, [FromBody] FormatosDto formatosDto)
    {
        if (formatosDto.Id == 0)
        {
            formatosDto.Id = id;
        }
        if (formatosDto.Id != id)
        {
            return NotFound();
        }
        var formatos = _mapper.Map<Formatos>(formatosDto);
        if (formatosDto.FechaCreacion == DateOnly.MinValue)
        {
            formatosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            formatos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (formatosDto.FechaModificacion == DateOnly.MinValue)
        {
            formatosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            formatos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        formatosDto.Id = formatos.Id;
        _unitOfWork.Formatos.Update(formatos);
        await _unitOfWork.SaveAsync();
        return formatosDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var formatos = await _unitOfWork.Formatos.GetByIdAsync(id);
        if (formatos == null)
        {
            return NotFound();
        }
        _unitOfWork.Formatos.Remove(formatos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
