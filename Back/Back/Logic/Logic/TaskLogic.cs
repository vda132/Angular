namespace Logic.Logic;

public class TaskLogic : LogicBase<DBLayer.Models.Task>, ITaskLogic
{
    public TaskLogic(DBLayer.Repository.ITaskRepository repository) : base(repository) { }

    protected override async Task<bool> BeforeAddAsync(DBLayer.Models.Task model)
    {
        if (!IsValid(model.Name, model.Description))
            return false;

        var tasks = await this.repository.GetAllAsync();

        var result = tasks.FirstOrDefault(x => x.Name.Equals(model.Name) ||
                                                x.Description.Equals(model.Description));

        if (result is not null)
            return false;

        return true;
    }

    protected override async Task<bool> BeforeUpdateAsync(Guid id, DBLayer.Models.Task model)
    {
        if (!IsValid(model.Name, model.Description))
            return false;

        var tasks = await this.repository.GetAllAsync();

        var result = tasks.FirstOrDefault(x => ((x.Name.Equals(model.Name) ||
                                                x.Description.Equals(model.Description)) && x.Id != id));

        if (result is not null)
            return false;

        return true;
    }

    private bool IsValid(string name, string description) =>
        !(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description));
}
