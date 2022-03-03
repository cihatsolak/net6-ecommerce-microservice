﻿namespace CourseSales.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(
            ICatalogService catalogService,
            IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var basketViewModel = await _basketService.GetAsync();
            return View(basketViewModel);
        }

        public async Task<IActionResult> AddBasketItem(string courseId)
        {
            var course = await _catalogService.GetByCourseId(courseId);

            BasketItemViewModel basketItem = new()
            {
                CourseId = course.Id,
                CourseName = course.Name,
                Price = course.Price
            };

            await _basketService.AddBasketItemAsync(basketItem);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveBasketItem(string courseId)
        {
            var result = await _basketService.RemoveBasketItemAsync(courseId);
            return RedirectToAction(nameof(Index));
        }
    }
}
