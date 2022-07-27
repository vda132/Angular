namespace Logic.Logic;

public interface ILogicBase<TModel> where TModel : DBLayer.Models.ModelBase
{
    Task<Guid> AddAsync(TModel model);
    Task<IReadOnlyCollection<TModel>> GetAllAsync();
    Task<bool> UpdateAsync(Guid id, TModel newModel);
    Task<bool> DeleteAsync(Guid id);
}

