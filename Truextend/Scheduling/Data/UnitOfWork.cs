using System;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Truextend.Scheduling.Data.Exceptions;
using Truextend.Scheduling.Data.Repository;
using Truextend.Scheduling.Data.Repository.Interfaces;

namespace Truextend.Scheduling.Data
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly SchedulingDBContext _schedulingDBContext;
        private readonly IStudentRepository _student;
        private readonly ICourseRepository _course;
        private readonly IStudentCourseRepository _studentCourse;

        public UnitOfWork(SchedulingDBContext dbContext)
		{
            _schedulingDBContext = dbContext;
            _student = new StudentRepository(_schedulingDBContext);
            _course = new CourseRepository(_schedulingDBContext);
            _studentCourse = new StudentCourseRepository(_schedulingDBContext);
        }

        public void BeginTransaction()
        {
            _schedulingDBContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _schedulingDBContext.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _schedulingDBContext.Database.RollbackTransaction(); ;
        }

        public void Save()
        {
            try
            {
                BeginTransaction();
                _schedulingDBContext.SaveChanges();
                CommitTransaction();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                RollBackTransaction();
                string message = $"Error to save changes on Database -> Save() {Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
                Log.Error(ex, $"{message}{Environment.NewLine} Stack trace: {Environment.NewLine}");
                throw new DatabaseException("Can not save changes, error in Database", ex.InnerException);
            }
            catch (DbUpdateException ex)
            {
                RollBackTransaction();
                string message = $"Error to save changes on Database -> Save() {Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
                Log.Error(ex, $"{message}{Environment.NewLine} Stack trace: {Environment.NewLine}");
                throw new DatabaseException("Can not save changes, error in Database", ex.InnerException);
            }
            catch (Exception ex)
            {
                string message = $"Error to save changes on Database -> Save() {Environment.NewLine}Message: {ex.Message}{Environment.NewLine}";
                Log.Error(ex, $"{message}{Environment.NewLine} Stack trace: {Environment.NewLine}");
                throw new DatabaseException("Can not save changes, error in Database", ex.InnerException);
            }
        }

        public IStudentRepository StudentRepository
        {
            get { return _student; }
        }

        public ICourseRepository CourseRepository
        {
            get { return _course; }
        }

        public IStudentCourseRepository StudentCourseRepository
        {
            get { return _studentCourse; }
        }
    }
}

