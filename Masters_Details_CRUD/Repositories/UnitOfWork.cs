using Masters_Details_CRUD.Models;
using Masters_Details_CRUD.Repositories.interfaces;

namespace Masters_Details_CRUD.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicantDbContext db;
        public UnitOfWork(ApplicantDbContext db)
        {
            this.db = db;
        }
        public IGenericRepo<T> GetRepo<T>() where T : class, new()
        {
            return new GenericRepo<T>(db);
        }

        public async Task<bool> SaveAsync()
        {
            int rowsEffected = await db.SaveChangesAsync();
            return rowsEffected > 0;
        }
    }
}
