using Repository;
using Repository.Models;

namespace Service
{
    public class DirectorsService
	{
        private DirectorRepository repository = new();
        public List<Director> GetAll()
        {
            return repository.GetAll();
        }
    }
}
