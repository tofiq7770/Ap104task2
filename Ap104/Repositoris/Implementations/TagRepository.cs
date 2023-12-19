
namespace Ap104.Repositoris.Implementations
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AppDbContext db) : base(db)
        {

        }

    }
}
