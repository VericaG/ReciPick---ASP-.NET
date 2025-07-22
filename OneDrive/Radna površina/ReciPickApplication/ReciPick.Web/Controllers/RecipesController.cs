using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReciPick.Domain.DomainModels;
using ReciPick.Service.Interface;

namespace ReciPick.Web.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return View(recipes);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var recipe = await _recipeService.GetRecipeWithIngredientsAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                await _recipeService.CreateRecipeAsync(recipe);
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _recipeService.UpdateRecipeAsync(recipe);
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return View(recipe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _recipeService.DeleteRecipeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _recipeService.AddRecipeToCartAsync(id, userId);
            TempData["Message"] = "Recipe added to cart successfully!";
            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> AddIngredient(Guid recipeId)
        {
            ViewBag.RecipeId = recipeId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddIngredient(Guid recipeId, Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                await _recipeService.AddIngredientToRecipeAsync(recipeId, ingredient);
                return RedirectToAction(nameof(Details), new { id = recipeId });
            }
            ViewBag.RecipeId = recipeId;
            return View(ingredient);
        }
    }
}