using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.Form.GetAll
{
    public class GetAllFormQueryHandler : IRequestHandler<GetAllFormQuery, List<FormDto>>
    {
        private readonly IFormRepository _formRepository;
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IFormTemplateContentRepository _formTemplateContentRepository;
        private readonly IFormFieldValueRepository _formFieldValueRepository;
        private readonly IMapper _mapper;
        public GetAllFormQueryHandler(IFormRepository formRepository
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
        public async Task<List<FormDto>> Handle(GetAllFormQuery request, CancellationToken cancellationToken)
        {
            var contents = await _formTemplateContentRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.Name, cancellationToken);
            var result = await _formRepository.FindAllAsync(x => x.DeletedAt == null, cancellationToken);
            var title = await _formTemplateRepository.FindAllToDictionaryAsync(x => x.DeletedAt == null, x => x.ID, x => x.Name, cancellationToken);
            return result.MapToFormDtoList(_mapper, title, contents);
        }
    }
}
