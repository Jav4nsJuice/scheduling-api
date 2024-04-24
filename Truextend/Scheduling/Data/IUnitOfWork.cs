using System;
using Truextend.Scheduling.Data.Repository.Interfaces;

namespace Truextend.Scheduling.Data
{
	public interface IUnitOfWork
	{
		IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }
        IStudentCourseRepository StudentCourseRepository { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        void Save();
    }
}

