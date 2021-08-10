// <copyright file="MediatorModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Autofac;
using FluentValidation;
using MediatR;
using TweetApplication.Behaviors;

namespace TweetApplication.Configurations.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        /// <summary>
        /// Load mediator
        /// </summary>
        /// <param name="builder">Container Builder</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(Mediator)).As(typeof(IMediator));

            // Get the assembly name
            var assembly = typeof(Startup).GetType().Assembly;

            builder.RegisterAssemblyTypes(assembly)
            .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
            .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
