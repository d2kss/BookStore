using BookStoreMgmt.Models;
using NuGet.DependencyResolver;

namespace BookStoreMgmt.Repository.Interface
{
    public interface IUnitOfWork
    {
        IRepository<Books> BooksRepository { get; }

        void Save();
    }
}
