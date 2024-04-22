namespace Library_WebApi.Dtos
{
    public class ReturnBookResultDto
    {
        public bool IsReturnSuccess { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public int UpdatedCopies { get; set; }
    }
}