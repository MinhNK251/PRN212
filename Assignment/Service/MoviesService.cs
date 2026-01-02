using CsvHelper.Configuration;
using CsvHelper;
using Repository;
using Repository.Models;
using System.Globalization;

namespace Service
{
	public class MoviesService
	{
		private MovieRepository _movieRepository = new();
		private ActorRepository _actorRepository = new();
		private DirectorRepository _directorRepository = new();
		public List<Movie> GetAllMovies()
		{
			return _movieRepository.GetAll();
		}
		public List<Movie>? SearchMovies(string name)
		{
			return _movieRepository.Search(name);
		}
		public void Delete(Movie movie)
		{
			_movieRepository.Delete(movie);
		}
		public Movie? Get(int id)
		{
			return _movieRepository.Get(id);
		}
		public void Add(Movie movie)
		{
			_movieRepository.Create(movie);
		}
		public void Update(Movie movie)
		{
			_movieRepository.Update(movie);
		}

		public void ClearAll()
		{
			_movieRepository.ClearAll();
		}

		public async Task LoadCsvDataAsync(string filePath)
		{
			try
			{
				var config = new CsvConfiguration(CultureInfo.InvariantCulture)
				{
					BadDataFound = null,
					MissingFieldFound = null,
					HeaderValidated = null,
					Delimiter = ","
				};
				using (var reader = new StreamReader(filePath))
				using (var csv = new CsvReader(reader, config))
				{
					var records = csv.GetRecords<dynamic>();
					foreach (var record in records)
					{
						await InsertMovieAsync(record);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while loading the CSV data: {ex.Message}");
				throw;
			}
		}

		private async Task InsertMovieAsync(dynamic record)
		{
			try
			{
				var directorId = await _directorRepository.GetOrInsertDirectorAsync(record.Director);
				var star1Id = await _actorRepository.GetOrInsertActorAsync(record.Star1);
				var star2Id = await _actorRepository.GetOrInsertActorAsync(record.Star2);
				var star3Id = await _actorRepository.GetOrInsertActorAsync(record.Star3);
				var star4Id = await _actorRepository.GetOrInsertActorAsync(record.Star4);
				if (!double.TryParse(record.IMDB_Rating, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsedRating))
				{
					throw new FormatException($"Invalid IMDB_Rating: {record.IMDB_Rating}");
				}
				if (!int.TryParse(record.Meta_score, out int parsedMetaScore))
				{
					throw new FormatException($"Invalid Meta_score: {record.Meta_score}");
				}
				if (!int.TryParse(record.No_of_Votes, out int parsedNoOfVotes))
				{
					throw new FormatException($"Invalid No_of_Votes: {record.No_of_Votes}");
				}
				var movie = new Movie
				{
					PosterLink = record.Poster_Link,
					SeriesTitle = record.Series_Title,
					ReleasedYear = int.Parse(record.Released_Year),
					Certificate = record.Certificate,
					Runtime = record.Runtime,
					Genre = record.Genre,
					Imdbrating = parsedRating,
					Overview = record.Overview,
					MetaScore = parsedMetaScore,
					DirectorId = directorId,
					Star1Id = star1Id,
					Star2Id = star2Id,
					Star3Id = star3Id,
					Star4Id = star4Id,
					NoOfVotes = parsedNoOfVotes
				};
				await _movieRepository.InsertMovieAsync(movie);
			}
			catch (FormatException ex)
			{
				Console.WriteLine($"Error in record: {record}. Details: {ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred while inserting a movie: {ex.Message}");
				throw;
			}
		}
	}
}
