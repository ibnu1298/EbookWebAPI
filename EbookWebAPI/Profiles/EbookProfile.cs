using AutoMapper;
using EbookWebAPI.Dtos.Ebook;
using EbookWebAPI.Models;

namespace EbookWebAPI.Profiles
{
    public class EbookProfile : Profile
    {
        public EbookProfile() 
        {
            CreateMap<AddEBookDTO, LinkEbook>();

            CreateMap<ReadEBookDTO, LinkEbook>();
            CreateMap<LinkEbook, ReadEBookDTO>();

            CreateMap<ReadSingleEBookDTO, LinkEbook>();
            CreateMap<LinkEbook, ReadSingleEBookDTO>();

            CreateMap<ReadMultipleEBookDTO, LinkEbook[]>();
            CreateMap<LinkEbook[], ReadMultipleEBookDTO>();
        }
    }
}
