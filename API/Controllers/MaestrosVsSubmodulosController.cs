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

public class MaestrosVsSubmodulosController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MaestrosVsSubmodulosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MaestrosVsSubmodulosDto>>> Get()
    {
        var maestrosVsSubmodulos = await _unitOfWork.MaestrosVsSubmodulos.GetAllAsync();
        return _mapper.Map<List<MaestrosVsSubmodulosDto>>(maestrosVsSubmodulos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaestrosVsSubmodulosDto>> Get(int id)
    {
        var maestrosVsSubmodulos = await _unitOfWork.MaestrosVsSubmodulos.GetByIdAsync(id);
        if (maestrosVsSubmodulos == null)
        {
            return NotFound();
        }
        return _mapper.Map<MaestrosVsSubmodulosDto>(maestrosVsSubmodulos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MaestrosVsSubmodulosDto>> Post(MaestrosVsSubmodulosDto maestrosVsSubmodulosDto)
    {
        var maestrosVsSubmodulos = _mapper.Map<MaestrosVsSubmodulos>(maestrosVsSubmodulosDto);
        if (maestrosVsSubmodulosDto.FechaCreacion == DateOnly.MinValue)
        {
            maestrosVsSubmodulosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            maestrosVsSubmodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (maestrosVsSubmodulosDto.FechaModificacion == DateOnly.MinValue)
        {
            maestrosVsSubmodulosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            maestrosVsSubmodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.MaestrosVsSubmodulos.Add(maestrosVsSubmodulos);
        await _unitOfWork.SaveAsync();
        if (maestrosVsSubmodulosDto == null)
        {
            return BadRequest();
        }
        maestrosVsSubmodulosDto.Id = maestrosVsSubmodulos.Id;
        return CreatedAtAction(nameof(Post), new { id = maestrosVsSubmodulosDto.Id }, maestrosVsSubmodulosDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MaestrosVsSubmodulosDto>> Put(int id, [FromBody] MaestrosVsSubmodulosDto maestrosVsSubmodulosDto)
    {
        if (maestrosVsSubmodulosDto.Id == 0)
        {
            maestrosVsSubmodulosDto.Id = id;
        }
        if (maestrosVsSubmodulosDto.Id != id)
        {
            return NotFound();
        }
        var maestrosVsSubmodulos = _mapper.Map<MaestrosVsSubmodulos>(maestrosVsSubmodulosDto);
        if (maestrosVsSubmodulosDto.FechaCreacion == DateOnly.MinValue)
        {
            maestrosVsSubmodulosDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            maestrosVsSubmodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (maestrosVsSubmodulosDto.FechaModificacion == DateOnly.MinValue)
        {
            maestrosVsSubmodulosDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            maestrosVsSubmodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        maestrosVsSubmodulosDto.Id = maestrosVsSubmodulos.Id;
        _unitOfWork.MaestrosVsSubmodulos.Update(maestrosVsSubmodulos);
        await _unitOfWork.SaveAsync();
        return maestrosVsSubmodulosDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var maestrosVsSubmodulos = await _unitOfWork.MaestrosVsSubmodulos.GetByIdAsync(id);
        if (maestrosVsSubmodulos == null)
        {
            return NotFound();
        }
        _unitOfWork.MaestrosVsSubmodulos.Remove(maestrosVsSubmodulos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
