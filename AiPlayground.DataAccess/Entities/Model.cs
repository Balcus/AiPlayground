namespace AiPlayground.DataAccess.Entities;
public class Model
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PlatformId { get; set; }

    // why is this virtual ??
    public virtual Platform Platform { get; set; } = null!;
    public virtual Run Run { get; set; } = null!;
}