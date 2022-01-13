using AutoMapper;
using Patika.NetCore.Example.BookStore.DBOperations;
using Patika.NetCore.Example.BookStore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patika.NetCore.Example.BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        
        public int AuthorId { get; set; }
        public UpdateAuthorCommandVM Model { get; set; }
        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public void Handle()
        {
            if (AuthorId == 0 || Model == null)
            {
                throw new InvalidOperationException("Aradığınız yazar bulunamadı");
            }
            var author = _dbContext.Authors.Find(AuthorId);
            if (author != null)
            {
                throw new InvalidOperationException("Aradığınız yazar bulunamadı");
            }
            author.FirstName = Model.FirstName;
            author.LastName = Model.LastName;
            _dbContext.SaveChanges();
        }

        public class UpdateAuthorCommandVM
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            
        }


    }
}
