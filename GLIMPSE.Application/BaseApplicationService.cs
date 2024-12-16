using AutoMapper;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Models;
using GLIMPSE.Domain.Services.Interfaces;

namespace GLIMPSE.Application
{
    public class BaseApplicationService<TEntity, TDTO> : IBaseApplicationService<TDTO>
    where TEntity : class
    where TDTO : class
{
    protected readonly IMapper _mapper;
    protected readonly IBaseService<TEntity> _service;

    public BaseApplicationService(IMapper mapper, IBaseService<TEntity> service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<TDTO> Add(TDTO dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _service.Add(entity);
        return _mapper.Map<TDTO>(entity);
    }

    public async Task<IList<TDTO>> GetAll()
    {
        var entities = await _service.GetAll();
        return _mapper.Map<IList<TDTO>>(entities);
    }

    public async Task<TDTO> GetById(int id)
    {
        var entity = await _service.GetById(id);
        return _mapper.Map<TDTO>(entity);
    }

    public async Task<TDTO> Remove(int id)
    {
        var entity = await _service.Remove(id);
        return _mapper.Map<TDTO>(entity);
    }

    public virtual async Task<TDTO> Update(TDTO dto)
    {
        var entity = await _service.GetById(((dynamic)dto).Id);
        _mapper.Map(dto, entity);
        await _service.Update(entity);
        return dto;
    }

    public async Task<IList<TDTO>> GetAllIgnoreFilters()
    {
        var entities = await _service.GetAllIgnoreFilters();
        return _mapper.Map<IList<TDTO>>(entities);
    }
}
}
