
using Patika.NetCore.Example.BookStore.DBOperations;
using System;


namespace Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _dbContext;
      
        public DeleteGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            if (GenreId == 0)
            {
                throw new InvalidOperationException("Silinecek kategori bulunamadı!");
            }
            var genre = _dbContext.Genres.Find(GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Silinecek kategori bulunamadı!");
            }
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }


    }
}
