using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoRhinoMock;
using Ploeh.AutoFixture.NUnit3;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tasker.Core.BL.Managers;
using Tasker.Core.DAL.Contracts;
using Tasker.Core.DAL.Entities;

namespace Tasker.Core.Tests.BL.Managers
{
    [TestFixture]
    public class ProjectManagerTests
    {
        private IFixture fixture;

        [SetUp]
        public void SetUp()
        {
            fixture = new Fixture().Customize(new AutoRhinoMockCustomization());
        }

        private IUnitOfWork GetFakeUnitOfWork()
        {
            var unitOfWork = fixture.Create<IUnitOfWork>();
            var projectRepository = fixture.Create<IRepository<Project>>();
            var taskRepository = fixture.Create<IRepository<Task>>();
            unitOfWork.Stub(u =>u.ProjectsRepository).Return(projectRepository);
            unitOfWork.Stub(u => u.TasksRepository).Return(taskRepository);            
            return unitOfWork;
        }

        [Test]
        public void GetAll_IsRepositoryCalled()
        {
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new ProjectManager(unitOfWork);
            var list = manager.GetAll();
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.GetAll());
        }

        [Test]
        public void Get_IsRepositoryCalled()
        {
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new ProjectManager(unitOfWork);
            var id = fixture.Create<int>();
            var returnId = manager.Get(id);
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.GetById(id)); 
        }

        [Test]
        [TestCase(0)]
        public void Get_ZeroParameter_IsNullReturn(int id)
        {
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new ProjectManager(unitOfWork);
            var item = manager.Get(id);
            Assert.IsNull(item);
        }

        [Test]
        public void GetProjectTasks_IsRepositoryCalled()
        {
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new ProjectManager(unitOfWork);
            var id = fixture.Create<int>();
            var taskList = manager.GetProjectTasks(id);
            unitOfWork.TasksRepository.AssertWasCalled(u => u.Find(Arg<Func<Task,bool>>.Is.Anything));
        }

        [Test, AutoData]
        public void SaveItem_IsRepositoryCalled(Project project)
        {
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new ProjectManager(unitOfWork);
            var id = manager.SaveItem(project);
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.Save(project));          
        }

        [Test, AutoData]
        public void SaveItem_IsReturnValueFromRepositiry(Project project)
        {
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new ProjectManager(unitOfWork);
            var value = fixture.Create<int>();
            unitOfWork.ProjectsRepository.Stub(x => x.Save(project)).Return(value);
            var id = manager.SaveItem(project);
            Assert.AreEqual(id, value);
        }

        [Test]
        public void SaveItem_NullArg_IsRepositoryNotCalledAndReturnZero()
        {
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new ProjectManager(unitOfWork);            
            var id = manager.SaveItem(null);
            unitOfWork.ProjectsRepository.AssertWasNotCalled(u => u.Save(Arg<Project>.Is.Anything));
            Assert.AreEqual(id, 0);
        }

        [Test]
        public void Delete_IsRepositoryCalled()
        {
            var unitOfWork = GetFakeUnitOfWork();
            var manager = new ProjectManager(unitOfWork);
            var value = fixture.Create<int>();
            var id = manager.Delete(value);
            unitOfWork.ProjectsRepository.AssertWasCalled(u => u.Delete(Arg<int>.Is.Anything));                        
        }
    }
}