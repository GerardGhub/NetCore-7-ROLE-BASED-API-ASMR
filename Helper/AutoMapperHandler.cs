using AutoMapper;
using LearnAPI.Modal;
using LearnAPI.Repos.Models;

namespace LearnAPI.Helper
{
    // This class defines the mapping profile for AutoMapper
    public class AutoMapperHandler:Profile
    {
        // Constructor where the mappings are configured
        public AutoMapperHandler() {
            // Mapping configuration between TblCustomer and Customermodal
            CreateMap<TblCustomer, Customermodal>()
                // Custom mapping for the Statusname property
                .ForMember(item => item.Statusname, opt => opt.MapFrom(
                item => (item.IsActive != null && item.IsActive.Value) ? "Active" : "In active"))
                // Enable reverse mapping from Customermodal back to TblCustomer
                .ReverseMap() ;


            // Mapping configuration between TblUser and UserModel
            CreateMap<TblUser, UserModel>()
                // Custom mapping for the Statusname property
                .ForMember(item => item.Statusname, opt => opt.MapFrom(
                item => (item.Isactive != null && item.Isactive.Value) ? "Active" : "In active"))
                // Enable reverse mapping from UserModel back to TblUser
                .ReverseMap();
        }
    }
}
