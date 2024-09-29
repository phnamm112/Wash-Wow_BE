using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using Wash_Wow.Domain.Common.Exceptions;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Entities.ConfigTable;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Form.SendForm
{
    public class SendFormCommandHandler : IRequestHandler<SendFormCommand, string>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IFormRepository _formRepository;
        private readonly IFormImageRepository _formImageRepository;
        private readonly IFormTemplateRepository _formTemplateRepository;
        public SendFormCommandHandler(ICurrentUserService currentUserService
            , IFormRepository formRepository
            , IFormImageRepository formImageRepository
            , IFormTemplateRepository formTemplateRepository)
        {
            _currentUserService = currentUserService;
            _formRepository = formRepository;
            _formImageRepository = formImageRepository;
            _formTemplateRepository = formTemplateRepository;

        }
        public async Task<string> Handle(SendFormCommand request, CancellationToken cancellationToken)
        {
            var template = await _formTemplateRepository.FindAsync(x => x.ID == request.FormTemplateID && x.DeletedAt == null, cancellationToken);
            if (template == null)
            {
                throw new NotFoundException("Template is not existed");
            }

            var form = new FormEntity
            {
                CreatorID = _currentUserService.UserId,
                FormTemplateID = request.FormTemplateID,
                Status = FormStatus.PENDING,
                FieldValues = new List<FormFieldValueEntity>()
            };

            foreach (var fieldValue in request.FieldValues)
            {
                var formFieldValue = new FormFieldValueEntity
                {
                    FormID = form.ID,
                    FormTemplateContentID = fieldValue.FieldID,
                    FieldValue = fieldValue.Value
                };
                form.FieldValues.Add(formFieldValue);
            }

            foreach (var item in request.ImageUrl)
            {
                FormImageEntity formImageItem = new FormImageEntity
                {
                    FormID = form.ID,                   
                    Url = item,
                    CreatedAt = DateTime.Now,
                    CreatorID = _currentUserService.UserId,
                };
                form.FormImages.Add(formImageItem);
                _formImageRepository.Add(formImageItem);
            }
            _formRepository.Add(form);
            return await _formRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? "Create Success" : "Create Fail";
        }
    }
}
