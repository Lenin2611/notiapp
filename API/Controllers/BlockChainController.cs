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

public class BlockChainController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BlockChainController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BlockChainDto>>> Get()
    {
        var blockChain = await _unitOfWork.BlockChains.GetAllAsync();
        return _mapper.Map<List<BlockChainDto>>(blockChain);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BlockChainDto>> Get(int Id)
    {
        var blockChain = await _unitOfWork.BlockChains.GetByIdAsync(Id);
        if (blockChain == null)
        {
            return NotFound();
        }
        return _mapper.Map<BlockChainDto>(blockChain);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BlockChainDto>> Post(BlockChainDto blockChainDto)
    {
        var blockChain = _mapper.Map<BlockChain>(blockChainDto);
        if (blockChainDto.FechaCreacion == DateOnly.MinValue)
        {
            blockChainDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            blockChain.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (blockChainDto.FechaModificacion == DateOnly.MinValue)
        {
            blockChainDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            blockChain.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.BlockChains.Add(blockChain);
        await _unitOfWork.SaveAsync();
        if (blockChainDto == null)
        {
            return BadRequest();
        }
        blockChainDto.Id = blockChain.Id;
        return CreatedAtAction(nameof(Post), new { id = blockChainDto.Id }, blockChainDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BlockChainDto>> Put(int id, [FromBody] BlockChainDto blockChainDto)
    {
        if (blockChainDto.Id == 0)
        {
            blockChainDto.Id = id;
        }
        if (blockChainDto.Id != id)
        {
            return NotFound();
        }
        var blockChain = _mapper.Map<BlockChain>(blockChainDto);
        if (blockChainDto.FechaCreacion == DateOnly.MinValue)
        {
            blockChainDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            blockChain.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (blockChainDto.FechaModificacion == DateOnly.MinValue)
        {
            blockChainDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            blockChain.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        blockChainDto.Id = blockChain.Id;
        _unitOfWork.BlockChains.Update(blockChain);
        await _unitOfWork.SaveAsync();
        return blockChainDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var blockChain = await _unitOfWork.BlockChains.GetByIdAsync(id);
        if (blockChain == null)
        {
            return NotFound();
        }
        _unitOfWork.BlockChains.Remove(blockChain);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
