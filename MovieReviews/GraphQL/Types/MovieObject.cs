using GraphQL.Types;
using MovieReviews.Models;

namespace MovieReviews.GraphQL.Types
{
	public sealed class MovieObject : ObjectGraphType<Movie>
	{
		public MovieObject()
		{
			Name = nameof(Movie);
			Description = "A movie in the collection";

			Field(m => m.Id, type: typeof(GuidGraphType)).Description("Identifier of the movie");
			Field(m => m.Name).Description("Name of the movie");
			Field(m => m.Reviews, type: typeof(ListGraphType<ReviewObject>)).Resolve(c => c.Source.Reviews).Description("Reviews of the movie");
		}
	}
}
