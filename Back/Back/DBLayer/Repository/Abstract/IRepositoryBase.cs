namespace DBLayer.Repository;

public interface IRepositoryBase<TModel> where TModel : Models.ModelBase
{
    Task<Guid> AddAsync(TModel model);
    Task<IReadOnlyCollection<TModel>> GetAllAsync();
    Task<bool> UpdateAsync(Guid id, TModel newModel);
    Task<bool> DeleteAsync(Guid id);
}
