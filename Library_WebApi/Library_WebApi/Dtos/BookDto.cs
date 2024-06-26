﻿namespace Library_WebApi.Dtos
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int Copies { get; set; }
        public int? BorrowedCopies { get; set; }
    }
}