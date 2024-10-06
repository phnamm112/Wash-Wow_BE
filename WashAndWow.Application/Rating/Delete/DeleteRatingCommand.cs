using MediatR;

namespace WashAndWow.Application.Rating.Delete
{
    public class DeleteRatingCommand : IRequest<bool>
    {
        public string Id { get; }
        public DeleteRatingCommand()
        {
            
        }
        public DeleteRatingCommand(string id)
        {
            Id = id;
        }
    }
}
