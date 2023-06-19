using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightDocsSystem.Model.DTOs;
using FlightDocsSystem.Model.Models;
using FlightDocsSystem.Models;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Aircraft, AircraftsDTO>().ReverseMap();
            CreateMap<AircraftType, AircraftTypeDTO>().ReverseMap();
            CreateMap<Airport, AirportDTO>().ReverseMap();
            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<Flight, FlightDTO>().ReverseMap();
            CreateMap<FlightDocumentType, FlightDocumentTypeDTO>().ReverseMap();
            CreateMap<Passenger, PassengerDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<GroupPermission, GroupPermissionDTO>().ReverseMap();

        }
    }
}