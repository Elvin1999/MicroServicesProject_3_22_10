namespace Contact.API.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Fullname { get; set; }= "";
    }
}
