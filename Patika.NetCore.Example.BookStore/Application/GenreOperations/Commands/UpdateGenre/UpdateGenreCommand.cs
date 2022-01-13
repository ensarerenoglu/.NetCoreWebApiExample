using AutoMapper;
using Patika.NetCore.Example.BookStore.DBOperations;
using Patika.NetCore.Example.BookStore.Entity;
using System;


namespace Patika.NetCore.Example.BookStore.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreViewModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        

        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public void Handle()
        {
            if (GenreId == 0 || Model == null)
            {
                throw new InvalidOperationException("Aradığını kategori bulunamadı!");
            }
            var genre = _dbContext.Genres.Find(GenreId);
            if (genre == null)
            {
                throw new InvalidOperationException("Aradığını kategori bulunamadı!");
            }
            genre.Name = Model.Name;
            _dbContext.SaveChanges();
        }

        public class UpdateGenreViewModel
        {
            public string Name { get; set; }
           
        }

    }
}
