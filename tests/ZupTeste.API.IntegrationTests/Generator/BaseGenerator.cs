using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bogus;
using FluentValidation;
using ZupTeste.Core;
using ZupTeste.Core.Contracts;
using ZupTeste.Repository.Repository;
using ZupTeste.Repository.UnitOfWork.Factories;

namespace ZupTeste.API.IntegrationTests.Generator
{
    public class BaseGenerator<TModel>
        where TModel : EntityBase, IAggregateRoot, new()
    {
        protected Faker<TModel> Rules;

        protected readonly IRepository<TModel> Repository;
        protected readonly IUnitOfWorkScopeFactory UnitOfWork;
        protected readonly Faker Faker;

        public BaseGenerator(
            IUnitOfWorkScopeFactory unitOfWork,
            IRepository<TModel> repository)
        {
            Repository = repository;
            UnitOfWork = unitOfWork;
            Faker = new Faker();
        }

        public TModel Generate()
        {
            var entity = Rules.Generate();
            return entity;
        }

        public virtual async Task<TModel> GenerateAndSaveAsync()
        {
            var entity = Rules.Generate();
            await SaveAsync(entity);
            return entity;
        }

        public virtual List<TModel> Generate(int count)
        {
            return Rules.Generate(count);
        }

        public virtual async Task<List<TModel>> GenerateAndSaveAsync(int count)
        {
            var entities = Rules.Generate(count);

            await SaveAsync(entities);
            
            return entities;
        }

        public virtual TModel Populate(TModel entity)
        {
            Rules.Populate(entity);
            return entity;
        }

        public virtual async Task<TModel> PopulateAndSaveAsync(TModel entity)
        {
            Rules.Populate(entity);
            await SaveAsync(entity);
            return entity;
        }

        public virtual async Task<TModel> SaveAsync(TModel entity)
        {
            var scope = UnitOfWork.Get();
            await Repository.SaveAsync(entity);
            await scope.CommitAsync();

            return entity;
        }
        
        public virtual async Task<List<TModel>> SaveAsync(List<TModel> entities)
        {
            var scope = UnitOfWork.Get();

            foreach (var entity in entities)
            {
                await Repository.SaveAsync(entity);
            }
            
            await scope.CommitAsync();

            return entities;
        }

        public BaseGenerator<TModel> WithRule<TProperty>(Expression<Func<TModel, TProperty>> property, TProperty value)
        {
            return WithRule<BaseGenerator<TModel>, TProperty>(property, value);
        }

        public TGenerator WithRule<TGenerator, TProperty>(Expression<Func<TModel, TProperty>> property, TProperty value)
            where TGenerator : BaseGenerator<TModel>
        {
            var generator = (TGenerator)MemberwiseClone();
            generator.Rules = Rules.Clone();
            generator.Rules.RuleFor(property, value);
            return generator;
        }
    }
}