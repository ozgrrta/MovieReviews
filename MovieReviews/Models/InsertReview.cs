using MovieReviews.GraphQL.Types;

namespace MovieReviews.Models
{
	public class InsertReview
	{
		public Guid Id { get; set; }
		public ReviewInputObject Review { get; set; }
	}
}
