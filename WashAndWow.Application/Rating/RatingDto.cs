namespace WashAndWow.Application.Rating
{
    public class RatingDto
    {
        public string Id { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public string LaundryShopId { get; set; }
        public string CreatorId { get; set; }
    }
}
