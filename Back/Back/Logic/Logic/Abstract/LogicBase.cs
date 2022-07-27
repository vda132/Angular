using DBLayer.Models;
using DBLayer.Repository;

namespace Logic.Logic;

public class LogicBase<TModel> where TModel : ModelBase
{
    protected readonly IRepositoryBase<TModel> repository;

    public LogicBase(IRepositoryBase<TModel> repository) => 
        this.repository = repository;

    protected virtual Task<bool> BeforeAddAsync(TModel model) =>
       System.Threading.Tasks.Task.FromResult(true);

    public virtual async Task<Guid> AddAsync(TModel model)
    {
        if(await this.BeforeAddAsync(model))
            return await this.repository.AddAsync(model);

        return default;
    }

    public async Task<bool> DeleteAsync(Guid id) => 
        await this.repository.DeleteAsync(id);

    public async Task<IReadOnlyCollection<TModel>> GetAllAsync() => 
        await this.repository.GetAllAsync();

    protected virtual Task<bool> BeforeUpdateAsync(Guid id, TModel model) =>
       System.Threading.Tasks.Task.FromResult(true);

    public async Task<bool> UpdateAsync(Guid id, TModel newModel)
    {

        if (await this.BeforeUpdateAsync(id, newModel))
            return await this.repository.UpdateAsync(id, newModel);

        return default;
    }
}
