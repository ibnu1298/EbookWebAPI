using AutoMapper;
using EbookWebAPI.Dtos.Ebook;
using EbookWebAPI.Models;

namespace EbookWebAPI.Profiles
{
    public class EmailProfile : Profile
    {
        public EmailProfile() 
        {
            CreateMap<CreateEmailDTO, EmailCustomer>();

            CreateMap<CreateMultipleEmailDTO, EmailCustomer[]>();

            CreateMap<EmailDTO, EmailCustomer>();
            CreateMap<EmailCustomer, EmailDTO>();

            CreateMap<ResponseCreateEmailDTO, EmailCustomer>();
            CreateMap<EmailCustomer, ResponseCreateEmailDTO>();


            CreateMap<MultipleEmailDTO, EmailCustomer[]>();
            CreateMap<EmailCustomer[], MultipleEmailDTO>();

            CreateMap<EmailDTO[], EmailCustomer[]>();
            CreateMap<EmailCustomer[],EmailDTO[]>();
        }
    }
}
