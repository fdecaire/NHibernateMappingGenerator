using NHibernate;
using NHibernate.Engine;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace NHibernateDataLayer
{
    public partial class StatelessSessionWrapper : IStatelessSession
	{
		private readonly IStatelessSession _Session;

		public StatelessSessionWrapper(IStatelessSession session)
		{
			this._Session = session;
		}

		public void Dispose()
		{
			_Session.Dispose();
		}

		public ISessionImplementor GetSessionImplementation()
		{
			return _Session.GetSessionImplementation();
		}

		public void Close()
		{
			_Session.Close();
		}

		public object Insert(object entity)
		{
			return _Session.Insert(entity);
		}

		public object Insert(string entityName, object entity)
		{
			return _Session.Insert(entityName, entity);
		}

		public void Update(object entity)
		{
			_Session.Update(entity);
		}

		public void Update(string entityName, object entity)
		{
			_Session.Update(entityName, entity);
		}

		public void Delete(object entity)
		{
			_Session.Delete(entity);
		}

		public void Delete(string entityName, object entity)
		{
			_Session.Delete(entityName, entity);
		}

		public object Get(string entityName, object id)
		{
			return _Session.Get(entityName, id);
		}

		public T Get<T>(object id)
		{
			return _Session.Get<T>(id);
		}

		public object Get(string entityName, object id, LockMode lockMode)
		{
			return _Session.Get(entityName, id, lockMode);
		}

		public T Get<T>(object id, LockMode lockMode)
		{
			return _Session.Get<T>(id, lockMode);
		}

		public void Refresh(object entity)
		{
			_Session.Refresh(entity);
		}

		public void Refresh(string entityName, object entity)
		{
			_Session.Refresh(entityName, entity);
		}

		public void Refresh(object entity, LockMode lockMode)
		{
			_Session.Refresh(entity, lockMode);
		}

		public void Refresh(string entityName, object entity, LockMode lockMode)
		{
			_Session.Refresh(entityName, entity, lockMode);
		}

		public IQuery CreateQuery(string queryString)
		{
			return _Session.CreateQuery(queryString);
		}

		public IQuery GetNamedQuery(string queryName)
		{
			return _Session.GetNamedQuery(queryName);
		}

		public ICriteria CreateCriteria<T>() where T : class
		{
			return _Session.CreateCriteria<T>();
		}

		public ICriteria CreateCriteria<T>(string alias) where T : class
		{
			return _Session.CreateCriteria<T>(alias);
		}

		public ICriteria CreateCriteria(Type entityType)
		{
			return _Session.CreateCriteria(entityType);
		}

		public ICriteria CreateCriteria(Type entityType, string alias)
		{
			return _Session.CreateCriteria(entityType, alias);
		}

		public ICriteria CreateCriteria(string entityName)
		{
			return _Session.CreateCriteria(entityName);
		}

		public ICriteria CreateCriteria(string entityName, string alias)
		{
			return _Session.CreateCriteria(entityName, alias);
		}

		public IQueryOver<T, T> QueryOver<T>() where T : class
		{
			return _Session.QueryOver<T>();
		}

		public IQueryOver<T, T> QueryOver<T>(Expression<Func<T>> alias) where T : class
		{
			return _Session.QueryOver(alias);
		}

		public ISQLQuery CreateSQLQuery(string queryString)
		{
			return _Session.CreateSQLQuery(queryString);
		}

		public ITransaction BeginTransaction()
		{
			return _Session.BeginTransaction();
		}

		public ITransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return _Session.BeginTransaction(isolationLevel);
		}

		public IStatelessSession SetBatchSize(int batchSize)
		{
			return _Session.SetBatchSize(batchSize);
		}

		public IDbConnection Connection
		{
			get { return _Session.Connection; }
		}

		public ITransaction Transaction
		{
			get { return _Session.Transaction; }
		}

		public bool IsOpen
		{
			get { return _Session.IsOpen; }
		}

		public bool IsConnected
		{
			get { return _Session.IsConnected; }
		}

        DbConnection IStatelessSession.Connection => throw new NotImplementedException();

        public bool IsTypeMapped(Type t)
		{
			return GetSessionImplementation().Factory.GetClassMetadata(t) != null;
		}

        public Task<object> InsertAsync(object entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> InsertAsync(string entityName, object entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(object entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string entityName, object entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string entityName, object entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string entityName, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(string entityName, object id, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(object id, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(object entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string entityName, object entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(object entity, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(string entityName, object entity, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void JoinTransaction()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>(string entityName)
        {
            throw new NotImplementedException();
        }
    }
}