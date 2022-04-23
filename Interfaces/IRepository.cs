using Ardalis.Specification;

namespace Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
