using AutoMapper;
using MediatR;
using Wash_Wow.Domain.Common.Exceptions;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Form.GetFormByID
{
    public class GetFormByIDHandler : IRequestHandler<GetFormByIDQuery, FormDto>
    {
        private readonly IFormRepository _formRepository;
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IFormTemplateContentRepository _formTemplateContentRepository;
        private readonly IFormFieldValueRepository _formFieldValueRepository;
        private readonly IMapper _mapper;

        public GetFormByIDHandler(IFormRepository formRepository
            , IFormTemplateRepository formTemplateRepository
            , IFormTemplateContentRepository formTemplateContentRepository
            , IFormFieldValueRepository formFieldValueRepository
            , IMapper mapper)
        {
            _formRepository = formRepository;
            _formTemplateRepository = formTemplateRepository;
            _formTemplateContentRepository = formTemplateContentRepository;
            _formFieldValueRepository = formFieldValueRepository;
            _mapper = mapper;
        }

        public async Task<FormDto> Handle(GetFormByIDQuery request, CancellationToken cancellationToken)
        {
            var exist = await _formRepository.FindAsync(x => x.ID == request.ID && x.DeletedAt == null, cancellationToken);
            if (exist == null)
            {
                throw new NotFoundException("Form is not exist");
            }

            var contents = await _formTemplateContentRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null && x.FormTemplateID == exist.FormTemplateID, x => x.ID, x => x.Name, cancellationToken);
            return exist.MapToFormDto(_mapper, exist.FormTemplate.Name, contents);
        }
    }
}
