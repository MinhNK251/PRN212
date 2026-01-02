using Repository;
using Repository.Models;

namespace Service
{
    public class ActorsService
    {
        private ActorRepository repository = new();
        public List<Actor> GetAll()
        {
            return repository.GetAll();
        }
    }
}
