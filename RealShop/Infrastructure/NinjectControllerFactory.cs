using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DomainModel.Classes;
using DomainModel.Interfaces;
using DomainModel.Entity;
using Ninject;

namespace RealShop.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel ninjectkernel;

        public NinjectControllerFactory()
        {
            ninjectkernel = new StandardKernel();
            AddBindings();
        }

        //метод для автоматического создания объектов при запуске контролллеров
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectkernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectkernel.Bind<IProductRepository>().To<ProductRepository>();
            ninjectkernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            ninjectkernel.Bind<IOrderRepository>().To<OrderRepository>();
            ninjectkernel.Bind<ICosmeticRepository>().To<CosmeticRepository>();
            ninjectkernel.Bind<ICareRepository>().To<CareRepository>();
            ninjectkernel.Bind<IColor>().To<ColorRepository>();
            ninjectkernel.Bind<IDataForPrice>().To<DataForPriceRepository>();
        }
    }
}