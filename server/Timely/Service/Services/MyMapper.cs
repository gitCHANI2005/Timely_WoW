﻿using AutoMapper;
using Repository.Entity;
using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MyMapper : Profile
    {
        public MyMapper()
        {

            https://localhost:7013/api/OrderCreateMap<MenuDose, MenuDoseDto>().ForMember(dest => dest.Image, src => src.MapFrom(s => s.ImageUrl != null ? System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images", s.ImageUrl)) : null));
            //מהתצוגה לשרת
            CreateMap<MenuDoseDto, MenuDose>().ForMember(dest => dest.ImageUrl, src => src.MapFrom(s => s.File.FileName));

            CreateMap<Store, StoreDto>().ForMember(dest => dest.Image, src => src.MapFrom(s => s.ImgUrl != null ? System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images", s.ImgUrl)) : null));
            //מהתצוגה לשרת
            CreateMap<StoreDto, Store>().ForMember(dest => dest.ImgUrl , src => src.MapFrom(s => s.File.FileName));

            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ForMember(dest => dest.Image, src => src.MapFrom(s => s.ImageUrl != null ? System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "Images", s.ImageUrl)) : null));
            //מהתצוגה לשרת
            CreateMap<CategoryDto, Category>().ForMember(dest => dest.ImageUrl, src => src.MapFrom(s => s.File.FileName));

            CreateMap<Deliver, DeliverDto>().ReverseMap();

            CreateMap<Owner, OwnerDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>()
                  .ForMember(dest => dest.CityHome, opt => opt.Ignore()) // נמפה ידנית
            .ForMember(dest => dest.CityWork, opt => opt.Ignore());


        }
        public byte[] convertToByte(string image)
        {
            var res = System.IO.File.ReadAllBytes(image);
            return res;
        }
    
    }
}
