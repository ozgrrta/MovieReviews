using MovieReviews.Models;

namespace MovieReviews.Repository
{
	public interface IMovieRepository
	{
		Task<List<Movie>> GetMoviesAsync();
		Task<Movie> GetMovieByIdAsync(Guid id);
		Task<Movie> AddReviewToMovieAsync(Guid id, Review review);
	}
}
