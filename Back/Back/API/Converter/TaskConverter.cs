namespace API.Converter;

public static class TaskConverter
{
    public static DBLayer.Models.Task ConvertFromDTOToModel(DTOs.TaskDTO dto) =>
        new DBLayer.Models.Task
        {
            Name = dto.Name,
            Description = dto.Description
        };
}
