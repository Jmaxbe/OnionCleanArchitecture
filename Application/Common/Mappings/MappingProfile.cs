﻿using System.Reflection;
using Application.Common.Models.Dto.Employees;
using Application.Common.Models.Dto.Employees.Response;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

        CreateMap<Employee, GetEmployeesResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UniqueId));
        CreateMap<Employee, UpdateEmployeeResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UniqueId));
        CreateMap<Employee, CreateEmployeeResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UniqueId));
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);
        
        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
        
        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();
        
        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            
            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count <= 0) continue;
                
                foreach (var interfaceMethodInfo in interfaces.Select(@interface => @interface.GetMethod(mappingMethodName, argumentTypes)))
                {
                    interfaceMethodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}