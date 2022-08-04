﻿using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAll(cancellationToken);
            return View(categories);
        }

        //[HttpGet]
        //public async Task<IActionResult> Create(CancellationToken cancellationToken)
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(CategoryDto model, CancellationToken cancellationToken)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var service = new CategoryDto
        //        {
        //            Title = model.Title,
                    
        //        };
        //        await _categoryAppService.Set(service, serviceFile, cancellationToken);
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}
    }
}
