using BookStoreMgmt.Data;
using BookStoreMgmt.Models;
using BookStoreMgmt.Repository.Interface;
using NuGet.DependencyResolver;
using static System.Collections.Specialized.BitVector32;

namespace BookStoreMgmt.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Books> _booksRepository;

       
        public BookStoreMgmtContext _Context { get; set; }
        public UnitOfWork(BookStoreMgmtContext Context)
        {
            _Context = Context;
        }
        public IRepository<Books> BooksRepository
        {
            get
            {
                if (_booksRepository == null)
                {
                    _booksRepository = new Repository<Books>(_Context);
                }
                return _booksRepository;
            }
        }

        public void Save()
        {
            _Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
