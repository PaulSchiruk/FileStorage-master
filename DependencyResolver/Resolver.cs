using System;
using System.Data.Entity;
using ORM;
using DAL.Repositories;
using DAL.Interfaces.Interfaces.RepositoryInterfaces;
using BLL.Services;
using BLL.Interfaces.Services;
using DAL;
using DAL.Interfaces.Interfaces;
using SimpleInjector;

namespace DependencyResolver
{
    public static class Resolver
    {
        public static void ConfigureResolver(this Container container)
        {
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register<DbContext, AppDbContext>(Lifestyle.Scoped);

            container.Register<IUserRepository, UserRepository>();
            container.Register<IFileRepository, AppFileRepository>();
            container.Register<IFolderRepository, AppFolderRepository>();

            container.Register<IUserService, UserService>();
            container.Register<IFileService, FileService>();
            container.Register<IFolderService, FolderService>();
            container.Register<IComponentFacadeService, ComponentFacadeService>();
        }

    }
}
