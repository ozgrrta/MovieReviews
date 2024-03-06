using Microsoft.EntityFrameworkCore;
using MovieReviews.Database;
using MovieReviews.Models;

namespace MovieReviews.Repository
{
	public class MovieRepository : IMovieRepository
	{
		private readonly MovieContext _context;

		public MovieRepository(MovieContext context)
		{
			_context = context;
			_context.Database.EnsureCreated();
		}

		public async Task<List<Movie>> GetMoviesAsync()
		{
			return await _context.Movies.AsNoTracking().ToListAsync();
		}

		public async Task<Movie> GetMovieByIdAsync(Guid id)
		{
			return await _context.Movies.Where(m => m.Id == id).AsNoTracking().FirstOrDefaultAsync();
		}

		public async Task<Movie> AddReviewToMovieAsync(Guid id, Review review)
		{
			var movie = await _context.Movies.Where(m => m.Id == id).FirstOrDefaultAsync();
			movie.AddReview(review);
			await _context.SaveChangesAsync();
			return movie;
		}
	}
}
