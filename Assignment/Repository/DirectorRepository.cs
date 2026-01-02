using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class DirectorRepository
    {
		private Imdbtop1000Context _context;
		public List<Director> GetAll()
        {
			_context = new();
			return _context.Directors.ToList();
        }

		public async Task<int> GetOrInsertDirectorAsync(string directorName)
		{
			_context = new();
			var director = await _context.Directors.FirstOrDefaultAsync(d => d.DirectorName == directorName);
			if (director != null)
				return director.DirectorId;
			var newDirector = new Director { DirectorName = directorName };
			_context.Directors.Add(newDirector);
			await _context.SaveChangesAsync();
			return newDirector.DirectorId;
		}
	}
}
