using LinkaBlog.Models;
using LinkaBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkaBlog.Data.Repository
{
	public interface IRepository
	{
		Post GetPost(int id);


		IndexViewModel GetAllPosts(int pageNumber, string Category);
		List<Post> GetAllPosts();
		List<Post> GetAllPostsSub( string Subcategory);



		void AddSubComment(SubComment comment);

		void AddPost(Post post);
		void UpdatePost(Post post);
		void RemovePost(int id);

		Task<bool> SaveChangesAsync();
		
	}
}
