namespace Library_WebApi.Dtos
{
    public class DeleteBookResultDto
    {
        public bool IsDeleted { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
}