using Autofac;
using GLIMPSE.Application;
using GLIMPSE.Application.Interfaces;
using GLIMPSE.Domain.Services;
using GLIMPSE.Domain.Services.Interfaces;
using GLIMPSE.Infrastructure.Data.Interfaces;
using GLIMPSE.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLIMPSE.Infrastructure.IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {

            builder.RegisterType<BoardApplicationService>().As<IBoardApplicationService>();
            builder.RegisterType<BoardService>().As<IBoardService>();
            builder.RegisterType<BoardRepository>().As<IBoardRepository>();

            builder.RegisterType<CardApplicationService>().As<ICardApplicationService>();
            builder.RegisterType<CardService>().As<ICardService>();
            builder.RegisterType<CardRepository>().As<ICardRepository>();

            builder.RegisterType<TagApplicationService>().As<ITagApplicationService>();
            builder.RegisterType<TagService>().As<ITagService>();
            builder.RegisterType<TagRepository>().As<ITagRepository>();

            builder.RegisterType<CheckboxApplicationService>().As<ICheckboxApplicationService>();
            builder.RegisterType<CheckboxService>().As<ICheckboxService>();
            builder.RegisterType<CheckboxRepository>().As<ICheckboxRepository>();

            builder.RegisterType<LaneApplicationService>().As<ILaneApplicationService>();
            builder.RegisterType<LaneService>().As<ILaneService>();
            builder.RegisterType<LaneRepository>().As<ILaneRepository>();

            builder.RegisterType<ProjectApplicationService>().As<IProjectApplicationService>();
            builder.RegisterType<ProjectService>().As<IProjectService>();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();

            builder.RegisterType<RepositoryService>().As<IRepositoryService>();
            builder.RegisterType<RepositoryService>().As<IRepositoryService>();
            builder.RegisterType<RepositoryRepository>().As<IRepositoryRepository>();

            builder.RegisterType<UserApplicationService>().As<IUserApplicationService>();
            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

            builder.RegisterType<RoleApplicationService>().As<IRoleApplicationService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
        }
    }
}
