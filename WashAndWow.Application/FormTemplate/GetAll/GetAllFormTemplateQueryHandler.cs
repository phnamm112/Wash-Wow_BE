using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WashAndWow.Domain.Repositories;

namespace WashAndWow.Application.FormTemplate.GetAll
{
    public class GetAllFormTemplateQueryHandler : IRequestHandler<GetAllFormTemplateQuery, List<FormTemplateDto>>
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly IMapper _mapper;
        public GetAllFormTemplateQueryHandler(IFormTemplateRepository formTemplateRepository, IMapper mapper)
        {
            _formTemplateRepository = formTemplateRepository;
            _mapper = mapper;
        }

        public async Task<List<FormTemplateDto>> Handle(GetAllFormTemplateQuery request, CancellationToken cancellationToken)
        {
            var result = await _formTemplateRepository.FindAllAsync(x => x.DeletedAt == null, cancellationToken);
            return result.MapToFormTemplateDtoList(_mapper);
        }
    }
}
