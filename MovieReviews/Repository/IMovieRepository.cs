﻿using MovieReviews.Models;

namespace MovieReviews.Repository
{
	public interface IMovieRepository
	{
		Task<Movie> GetMovieByIdAsync(Guid id);
		Task<Movie> AddReviewToMovieAsync(Guid id, Review review);

	}
}
