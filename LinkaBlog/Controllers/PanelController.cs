using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkaBlog.Data.File_Manager;
using LinkaBlog.Data.Repository;
using LinkaBlog.Models;
using LinkaBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkaBlog.Controllers
{
    public class PanelController : Controller
    {
        private IRepository _context;
        private IFileManager _fileManager;


        public PanelController(IRepository context,IFileManager fileManager)
        {
            _context = context;
            _fileManager = fileManager;
        }
        [Authorize(Roles="Admin")]
        public IActionResult Index(int pageNumber,string category) 
        {
            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1, category });
            //    var vm = new IndexViewModel
            //    {
            //        PageNumber=pageNumber,
            //   Posts= string.IsNullOrEmpty(category) ?
            //        _context.GetAllPosts(pageNumber):
            //        _context.GetAllPosts(category)
            //};
            var vm = _context.GetAllPosts(pageNumber, category);
            return View(vm);
        }
        public IActionResult Post(int id)
        {

            return View(new Post());
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new PostViewModel());
            }
            else
            {
                var post = _context.GetPost((int)id);
                return View(new PostViewModel 
                {
                    Id = post.Id,
                    Title =post.Title,
                    Body = post.Body,
                    AdminName = post.AdminName,
                    CurrentImage =post.Image,
                    Tags=post.Tags,
                    IsActive = post.IsActive,
                    Category = post.Category,
                    SubCategory = post.SubCategory,
                    Description = post.Description

                });

            };
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel postView)
        {
            if (ModelState.IsValid)
            {
                var post = new Post

            {
                Id = postView.Id,
                Title = postView.Title,
                    AdminName = postView.AdminName,
                    Body = postView.Body,
                Tags = postView.Tags,
                    IsActive = postView.IsActive,
                    Category = postView.Category,
                    SubCategory = postView.SubCategory,

                    Description = postView.Description,
               
            };
                if (postView.Image == null)
                    post.Image = postView.CurrentImage;
                else
                {
                    if (!string.IsNullOrEmpty(postView.CurrentImage))
                        _fileManager.RemoveImage(postView.CurrentImage);
                    post.Image = await _fileManager.SaveImage(postView.Image);
                }
            if (postView.Id > 0)
                _context.UpdatePost(post);
            else
                _context.AddPost(post);


            if (await _context.SaveChangesAsync())
                return RedirectToAction("Index", "Panel");
            else return View();
            }
            ModelState.AddModelError("", "Invalid Post");
            return View(postView);
        }
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _context.RemovePost(id);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}