using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{

    public class MovieRepository
    {
        private Imdbtop1000Context _context;
        public Movie? Get(int id)
        {
            _context = new();
            return _context.Movies.Find(id);
        }
        public List<Movie> GetAll() 
        {
            _context = new();
            return _context.Movies.Include(x => x.Director).Include(x => x.Star1).Include(x => x.Star2).Include(x => x.Star3).Include(x => x.Star4).OrderBy(m => m.MovieId).ToList();
        }
        public void Create(Movie movie) 
        {
            _context = new();
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }
        public void Update(Movie movie) 
        {
            _context = new();
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }
        public void Delete(Movie movie) 
        {
            _context = new();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
        public List<Movie> Search(string name)
		{
			_context = new();
            if(!string.IsNullOrEmpty(name))
			    return _context.Movies.Where(x => x.SeriesTitle.Contains(name)).ToList();
			return _context.Movies.Include(x => x.Director).Include(x => x.Star1).Include(x => x.Star2).Include(x => x.Star3).Include(x => x.Star4).ToList();
		}

		public void ClearAll()
		{
			_context = new();
			_context.Movies.RemoveRange(_context.Movies);
			_context.SaveChanges();
		}

		public async Task InsertMovieAsync(Movie movie)
		{
			_context = new();
			_context.Movies.Add(movie);
			await _context.SaveChangesAsync();
		}
	}
}
    