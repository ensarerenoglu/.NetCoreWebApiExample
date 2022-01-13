

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patika.NetCore.Example.BookStore.Entity
{
    public class Genre
    {
        //ID Generator - using System.ComponentModel.DataAnnotations.Schema; eklenmeli
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        // Todo: Servisler içindeki viewmodellere eklenecek
        public IEnumerable<Book> Books { get; set; }
    }
}
