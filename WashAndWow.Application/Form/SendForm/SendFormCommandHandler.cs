using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;
using WashAndWow.Domain.Entities;
using WashAndWow.Domain.Repositories;
using static Wash_Wow.Domain.Enums.Enums;

namespace WashAndWow.Application.Form.SendForm
{
    public class SendFormCommandHandler : IRequestHandler<SendFormCommand, string>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IFormRepository _formRepository;
        private readonly IFormImageRepository _formImageRepository;
        public SendFormCommandHandler(ICurrentUserService currentUserService
            , IFormRepository formRepository
            , IFormImageRepository formImageRepository)
        {
            _currentUserService = currentUserService;
            _formRepository = formRepository;
            _formImageRepository = formImageRepository;
        }
        public async Task<string> Handle(SendFormCommand request, CancellationToken cancellationToken)
        {
            FormEntity form = new FormEntity
            {
                Content = request.Content,
                Title = request.Title,
                Status = FormStatus.PENDING,
                CreatedAt = DateTime.Now,
                CreatorID = _currentUserService.UserId,
            };
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
