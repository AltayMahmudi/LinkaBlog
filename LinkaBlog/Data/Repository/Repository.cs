using LinkaBlog.Models;
using LinkaBlog.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.Data.Repository
{
	public class Repository : IRepository
	{
		private LinkaBlogDbContext _context;

		public Repository(LinkaBlogDbContext context)
		{
			_context = context;
		}
		public void AddPost(Post post)
		{
			_context.Posts.Add(post);

		}
		public List<Post> GetAllPosts()
		{

			return _context.Posts.ToList();
		}



	     
			public IndexViewModel GetAllPosts(int pageNumber, string category)
		{
			Func<Post, bool> InCategory = (post) => { return post.Category.ToLower().Equals(category.ToLower()); };
			int pageSize =9;
			int skipAmount = pageSize * (pageNumber - 1);

			var query = _context.Posts.AsQueryable();
			if (!String.IsNullOrEmpty(category))
				query = query.Where(x => InCategory(x));
			int postsCount = query.Count();
			return new IndexViewModel
			{
				PageNumber = pageNumber,
				PageCount=(int)Math.Ceiling((double)postsCount /pageSize),
				NextPage = postsCount > skipAmount + pageSize,
				Category = category,
				Posts = query
				.Skip(skipAmount)
				.Take(pageSize)
				.ToList()

			};
	}
		public List<Post> GetAllPostsSub(string subcategory)
		{
			int pageSize = 6;
			return _context.Posts.Where(post => post.SubCategory.ToLower().Equals(subcategory.ToLower())).Take(pageSize).ToList();

		}
		public Post GetPost(int id)
		{
			return _context.Posts.Include(p => p.MainComments).ThenInclude(mc => mc.SubComments).FirstOrDefault(p => p.Id == id);

		}

		public void RemovePost(int id)
		{
			_context.Posts.Remove(GetPost(id));
		}
		public void UpdatePost(Post post)
		{
			_context.Posts.Update(post);
		}

		public async Task<bool> SaveChangesAsync()
		{
			if (await _context.SaveChangesAsync() > 0) 
			{
				return true;
			}return false;
		}
		public void AddSubComment(SubComment comment)
		{
			_context.SubComments.Add(comment);
		}



	}
}
