using MediatR;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Form.SendForm
{
    public class SendFormCommand : IRequest<string>, ICommand
    {
        public SendFormCommand()
        {

        }
        public SendFormCommand(int formTemplateID, List<string> imageUrl, List<FormFieldValueDto> fieldValues)
        {
            FormTemplateID = formTemplateID;
            ImageUrl = imageUrl;
            FieldValues = fieldValues;
        }
        public int FormTemplateID { get; set; }
        public List<string>? ImageUrl { get; set; }
        public List<FormFieldValueDto> FieldValues { get; set; }
    }
}
