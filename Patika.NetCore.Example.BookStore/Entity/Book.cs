using System;

using System.ComponentModel.DataAnnotations.Schema;


namespace Patika.NetCore.Example.BookStore.Entity
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }

        // Todo: Servis içerisindeki modellere author bilgileri eklenecek
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
