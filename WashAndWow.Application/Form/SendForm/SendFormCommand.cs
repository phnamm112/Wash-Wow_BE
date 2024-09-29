using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wash_Wow.Application.Common.Interfaces;

namespace WashAndWow.Application.Form.SendForm
{
    public class SendFormCommand : IRequest<string>, ICommand
    {
        public SendFormCommand()
        {
            
        }
        public string Title {  get; set; }
        public string Content {  get; set; }
        public List<string> ImageUrl { get; set; }
    }
}
