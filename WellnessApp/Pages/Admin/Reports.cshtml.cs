using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lumina.Pages.Admin;

[Authorize(Roles = "Owner")]
public class ReportsModel : PageModel
{
    public void OnGet() { }
}