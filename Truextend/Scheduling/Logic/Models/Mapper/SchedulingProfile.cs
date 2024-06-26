﻿using System;
using AutoMapper;
using Truextend.Scheduling.Data.Models;

namespace Truextend.Scheduling.Logic.Models.Mapper
{
	public class SchedulingProfile : Profile
	{
		public SchedulingProfile()
		{
            this.CreateMap<Student, StudentDto>()
                .ReverseMap();
            this.CreateMap<Course, CourseDto>()
                .ReverseMap();
            this.CreateMap<StudentCourse, StudentCourseDto>()
                .ReverseMap();
        }
	}
}

