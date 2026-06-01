namespace Lumina.Data;

public class Package
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Credits { get; set; }
    public decimal PriceZAR { get; set; }
    public int ValidityDays { get; set; } = 90;
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; }
}