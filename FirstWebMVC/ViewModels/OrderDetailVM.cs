using Microsoft.AspNetCore.Mvc.Rendering;

public class OrderDetailVM
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }

    public List<SelectListItem> Products { get; set; }
}