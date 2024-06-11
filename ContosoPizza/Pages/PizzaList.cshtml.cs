using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Pages
{
    public class PizzaListModel : PageModel
    {
        private readonly PizzaService _service;
        public IList<Pizza> PizzaList { get; set; } = default!;

        public PizzaListModel(PizzaService service)
        {
            _service = service;
        }
        /* The BindProperty attribute is used to bind the NewPizza property to the Razor page. When an HTTP POST request is made, 
        the NewPizza property will be populated with the user's input.*/

        [BindProperty]
        // The default! keyword is used to initialize the NewPizza property to null. 
        public Pizza NewPizza { get; set; } = default!;

        public IActionResult OnPost()
        {
            //The ModelState.IsValid property is used to determine if the user's input is valid.
            if (!ModelState.IsValid || NewPizza == null)
            {
                return Page();
            }

            _service.AddPizza(NewPizza);
            //The RedirectToAction method is used to redirect the user to the Get page handler, which will re-render the page with the updated list of pizzas.
            return RedirectToAction("Get");
        }
        public IActionResult OnPostDelete(int id)
        {
            _service.DeletePizza(id);

            return RedirectToAction("Get");
        }
        public void OnGet()
        {
            PizzaList = _service.GetPizzas();
        }
    }
}