namespace Masters_Details_CRUD.Repositories.interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepo<T> GetRepo<T>() where T : class, new();
        Task<bool> SaveAsync();
    }
}
