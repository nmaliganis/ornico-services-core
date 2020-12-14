﻿using ornico.common.infrastructure.Domain;

namespace magic.button.common.infrastructure.Domain
{
    public interface IRepository<T, TId> : IReadOnlyRepository<T, TId>
        where T : IAggregateRoot
    {
        void Save(T entity);
        void Add(T entity);
        void Remove(T entity);
    }
}
