using BagCollection.DataAccess.Repository.IRepository;
using BagCollection.Models;
using BagCollection.Models.ViewModels;
using BagCollection.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Data;

namespace BagCollection.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        //for update
        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //create product
                return View(productVM); //tightly binded view
            }
            else
            {
                //update product
                productVM.Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
                return View(productVM);
            }

        }
        //    //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file,IFormFile? file1, IFormFile? file2, IFormFile? file3, IFormFile? file4)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);
                    if (obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if (file1 != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file1.FileName);
                    if (obj.Product.ImageUrl1 != null)
                    {
                        var oldImagePath1 = Path.Combine(wwwRootPath, obj.Product.ImageUrl1.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath1))
                        {
                            System.IO.File.Delete(oldImagePath1);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file1.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl1 = @"\images\products\" + fileName + extension;
                }
                if (file2 != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file2.FileName);
                    if (obj.Product.ImageUrl2 != null)
                    {
                        var oldImagePath2 = Path.Combine(wwwRootPath, obj.Product.ImageUrl2.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath2))
                        {
                            System.IO.File.Delete(oldImagePath2);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file2.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl2 = @"\images\products\" + fileName + extension;
                }
                if (file3 != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file3.FileName);
                    if (obj.Product.ImageUrl3 != null)
                    {
                        var oldImagePath3 = Path.Combine(wwwRootPath, obj.Product.ImageUrl3.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath3))
                        {
                            System.IO.File.Delete(oldImagePath3);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file3.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl3 = @"\images\products\" + fileName + extension;
                }
                if (file4 != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file4.FileName);
                    if (obj.Product.ImageUrl4 != null)
                    {
                        var oldImagePath4 = Path.Combine(wwwRootPath, obj.Product.ImageUrl4.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath4))
                        {
                            System.IO.File.Delete(oldImagePath4);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file4.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl4 = @"\images\products\" + fileName + extension;
                }
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    TempData["success"] = "Product created successfully";

                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                    TempData["success"] = "Product updated successfully";

                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);
        }




            #region API CALLS   
            [HttpGet]
            public IActionResult GetAll()
            {
                var productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
                return Json(new { data = productList });

            }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
        
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            if (obj.ImageUrl1 != null)
            {
                var oldImagePath1 = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl1.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath1))
                {
                    System.IO.File.Delete(oldImagePath1);
                }
            }
            if (obj.ImageUrl2 != null)
            {
                var oldImagePath2 = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl2.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath2))
                {
                    System.IO.File.Delete(oldImagePath2);
                }
            }
            if (obj.ImageUrl3 != null)
            {
                var oldImagePath3 = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl3.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath3))
                {
                    System.IO.File.Delete(oldImagePath3);
                }
            }
            if (obj.ImageUrl4 != null)
            {
                var oldImagePath4 = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl4.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath4))
                {
                    System.IO.File.Delete(oldImagePath4);
                }
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion
    }
    }
