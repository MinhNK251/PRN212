using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class ActorRepository
    {
		private Imdbtop1000Context _context;
		public List<Actor> GetAll()
        {
			_context = new();
			return _context.Actors.ToList();
        }

		public async Task<int> GetOrInsertActorAsync(string actorName)
		{
			_context = new();
			var actor = await _context.Actors.FirstOrDefaultAsync(d => d.ActorName == actorName);
			if (actor != null)
				return actor.ActorId;
			var newActor = new Actor { ActorName = actorName };
			_context.Actors.Add(newActor);
			await _context.SaveChangesAsync();
			return newActor.ActorId;
		}
	}
}
