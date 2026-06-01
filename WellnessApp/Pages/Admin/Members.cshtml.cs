using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lumina.Pages.Admin;

[Authorize(Roles = "Owner")]
public class MembersModel : PageModel
{
    public void OnGet() { }
}