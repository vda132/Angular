using DBLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace DBLayer.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly TasksContext context;

    public TaskRepository(TasksContext context) => 
        this.context = context;

    public async Task<Guid> AddAsync(Models.Task model)
    {
        var id = Guid.NewGuid();
        model.Id = id;

        await context.Tasks.AddAsync(model);
        await context.SaveChangesAsync();

        return id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var task = await GetTaskAsync(id, context);

        if(task is null)
            return false;

        context.Tasks.Remove(task);
        await context.SaveChangesAsync();

        return true;
    }

    public async Task<IReadOnlyCollection<Models.Task>> GetAllAsync() =>
        await this.context.Tasks.ToListAsync();

    public async Task<bool> UpdateAsync(Guid id, Models.Task newModel)
    {
        var task = await GetTaskAsync(id, context);

        if (task is null)
            return false;

        task.Name = newModel.Name;
        task.Description = newModel.Description;

        await this.context.SaveChangesAsync();
        
        return true;
    }

    private static async Task<Models.Task?> GetTaskAsync(Guid id, TasksContext context) =>
        await context.Tasks.FindAsync(id);
}
