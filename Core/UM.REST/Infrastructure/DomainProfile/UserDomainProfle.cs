using AutoMapper;
using UM.DataAccess.Entity.Identity;
using UM.Sdk.Model.Identity.User.Request;

namespace UM.REST.Infrastructure.DomainProfile
{
    public class UserDomainProfle : Profile
    {
        public UserDomainProfle()
        {
            CreateMap<AddRequest, User>().ReverseMap();

            CreateMap<UpdateRequest, User>();
                //.ForMember(des => des.Gender, op =>
                //{
                //    op.UseDestinationValue();
                //    op.Ignore();
                //})
                //.ForMember(des => des.Status, op =>
                //{
                //    op.UseDestinationValue();
                //    op.Ignore();
                //})
                //.ForMember(des => des.Id, op =>
                //{
                //    op.UseDestinationValue();
                //    op.Ignore();
                //});

            CreateMap<User, Sdk.Model.Identity.User.User>();
        }
    }
}
