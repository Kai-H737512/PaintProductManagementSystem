namespace PMS.Models;

public class PaintProduct
{
    //id, name, description, guid(duluxId), CTOR
    public int Id { get; set; }
    public string PaintProductName { get; set; }
    public string Description { get; set; }
    public Guid DuluxId { get; set; }

    public PaintProduct(int id, string paintProductName, string description, Guid duluxId)
    {
        Id = id;
        PaintProductName = paintProductName;
        Description = description;
        DuluxId = duluxId; 
    }
}
