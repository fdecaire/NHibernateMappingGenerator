using NHibernate;
using NHibernate.Engine;
using NHibernate.Stat;
using NHibernate.Type;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace NHibernateDataLayer
{
    public partial class SessionWrapper : ISession
	{
		private ISession _Session;

		public SessionWrapper(ISession session)
		{
			this._Session = session;
		}

		public void Dispose()
		{
			_Session.Dispose();
		}

		public void Flush()
		{
			_Session.Flush();
		}

		public IDbConnection Disconnect()
		{
			return _Session.Disconnect();
		}

		public void Reconnect()
		{
			_Session.Reconnect();
		}

		public IDbConnection Close()
		{
			return _Session.Close();
		}

		public void CancelQuery()
		{
			_Session.CancelQuery();
		}

		public bool IsDirty()
		{
			return _Session.IsDirty();
		}

		public bool IsReadOnly(object entityOrProxy)
		{
			return _Session.IsReadOnly(entityOrProxy);
		}

		public void SetReadOnly(object entityOrProxy, bool readOnly)
		{
			_Session.SetReadOnly(entityOrProxy, readOnly);
		}

		public object GetIdentifier(object obj)
		{
			return _Session.GetIdentifier(obj);
		}

		public bool Contains(object obj)
		{
			return _Session.Contains(obj);
		}

		public void Evict(object obj)
		{
			_Session.Evict(obj);
		}

		public object Load(Type theType, object id, LockMode lockMode)
		{
			return _Session.Load(theType, id, lockMode);
		}

		public object Load(string entityName, object id, LockMode lockMode)
		{
			return _Session.Load(entityName, id, lockMode);
		}

		public object Load(Type theType, object id)
		{
			return _Session.Load(theType, id);
		}

		public T Load<T>(object id, LockMode lockMode)
		{
			return _Session.Load<T>(id, lockMode);
		}

		public T Load<T>(object id)
		{
			return _Session.Load<T>(id);
		}

		public object Load(string entityName, object id)
		{
			return _Session.Load(entityName, id);
		}

		public void Load(object obj, object id)
		{
			_Session.Load(obj, id);
		}

		public void Replicate(object obj, ReplicationMode replicationMode)
		{
			_Session.Replicate(obj, replicationMode);
		}

		public void Replicate(string entityName, object obj, ReplicationMode replicationMode)
		{
			_Session.Replicate(entityName, obj, replicationMode);
		}

		public object Save(object obj)
		{
			return _Session.Save(obj);
		}

		public void Save(object obj, object id)
		{
			_Session.Save(obj, id);
		}

		public object Save(string entityName, object obj)
		{
			return _Session.Save(entityName, obj);
		}

		public void SaveOrUpdate(object obj)
		{
			_Session.SaveOrUpdate(obj);
		}

		public void SaveOrUpdate(string entityName, object obj)
		{
			_Session.SaveOrUpdate(entityName, obj);
		}

		public void Update(object obj)
		{
			_Session.Update(obj);
		}

		public void Update(object obj, object id)
		{
			_Session.Update(obj, id);
		}

		public void Update(string entityName, object obj)
		{
			_Session.Update(entityName, obj);
		}

		public object Merge(object obj)
		{
			return _Session.Merge(obj);
		}

		public object Merge(string entityName, object obj)
		{
			return _Session.Merge(entityName, obj);
		}

		public T Merge<T>(T entity) where T : class
		{
			return _Session.Merge(entity);
		}

		public T Merge<T>(string entityName, T entity) where T : class
		{
			return _Session.Merge(entityName, entity);
		}

		public void Persist(object obj)
		{
			_Session.Persist(obj);
		}

		public void Persist(string entityName, object obj)
		{
			_Session.Persist(entityName, obj);
		}

		public void Delete(object obj)
		{
			_Session.Delete(obj);
		}

		public void Delete(string entityName, object obj)
		{
			_Session.Delete(entityName, obj);
		}

		public int Delete(string query)
		{
			return _Session.Delete(query);
		}

		public int Delete(string query, object value, IType type)
		{
			return _Session.Delete(query, value, type);
		}

		public int Delete(string query, object[] values, IType[] types)
		{
			return _Session.Delete(query, values, types);
		}

		public void Lock(object obj, LockMode lockMode)
		{
			_Session.Lock(obj, lockMode);
		}

		public void Lock(string entityName, object obj, LockMode lockMode)
		{
			_Session.Lock(entityName, obj, lockMode);
		}

		public void Refresh(object obj)
		{
			_Session.Refresh(obj);
		}

		public void Refresh(object obj, LockMode lockMode)
		{
			_Session.Refresh(obj, lockMode);
		}

		public LockMode GetCurrentLockMode(object obj)
		{
			return _Session.GetCurrentLockMode(obj);
		}

		public ITransaction BeginTransaction()
		{
			return _Session.BeginTransaction();
		}

		public ITransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return _Session.BeginTransaction(isolationLevel);
		}

		public ICriteria CreateCriteria<T>() where T : class
		{
			return _Session.CreateCriteria<T>();
		}

		public ICriteria CreateCriteria<T>(string alias) where T : class
		{
			return _Session.CreateCriteria<T>(alias);
		}

		public ICriteria CreateCriteria(Type persistentClass)
		{
			return _Session.CreateCriteria(persistentClass);
		}

		public ICriteria CreateCriteria(Type persistentClass, string alias)
		{
			return _Session.CreateCriteria(persistentClass, alias);
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

		public IQueryOver<T, T> QueryOver<T>(string entityName) where T : class
		{
			return _Session.QueryOver<T>(entityName);
		}

		public IQueryOver<T, T> QueryOver<T>(string entityName, Expression<Func<T>> alias) where T : class
		{
			return _Session.QueryOver(entityName, alias);
		}

		public IQuery CreateQuery(string queryString)
		{
			return _Session.CreateQuery(queryString);
		}

		public IQuery CreateFilter(object collection, string queryString)
		{
			return _Session.CreateFilter(collection, queryString);
		}

		public IQuery GetNamedQuery(string queryName)
		{
			return _Session.GetNamedQuery(queryName);
		}

		public ISQLQuery CreateSQLQuery(string queryString)
		{
			return _Session.CreateSQLQuery(queryString);
		}

		public void Clear()
		{
			_Session.Clear();
		}

		public object Get(Type clazz, object id)
		{
			return _Session.Get(clazz, id);
		}

		public object Get(Type clazz, object id, LockMode lockMode)
		{
			return _Session.Get(clazz, id, lockMode);
		}

		public object Get(string entityName, object id)
		{
			return _Session.Get(entityName, id);
		}

		public T Get<T>(object id)
		{
			return _Session.Get<T>(id);
		}

		public T Get<T>(object id, LockMode lockMode)
		{
			return _Session.Get<T>(id, lockMode);
		}

		public string GetEntityName(object obj)
		{
			return _Session.GetEntityName(obj);
		}

		public IFilter EnableFilter(string filterName)
		{
			return _Session.EnableFilter(filterName);
		}

		public IFilter GetEnabledFilter(string filterName)
		{
			return _Session.GetEnabledFilter(filterName);
		}

		public void DisableFilter(string filterName)
		{
			_Session.DisableFilter(filterName);
		}

		public IMultiQuery CreateMultiQuery()
		{
			return _Session.CreateMultiQuery();
		}

		public ISession SetBatchSize(int batchSize)
		{
			return _Session.SetBatchSize(batchSize);
		}

		public ISessionImplementor GetSessionImplementation()
		{
			return _Session.GetSessionImplementation();
		}

		public IMultiCriteria CreateMultiCriteria()
		{
			return _Session.CreateMultiCriteria();
		}

		public ISession GetSession(EntityMode entityMode)
		{
			return _Session.GetSession(entityMode);
		}

		public FlushMode FlushMode
		{
			get { return _Session.FlushMode; }
			set { _Session.FlushMode = value; }
		}

		public CacheMode CacheMode
		{
			get { return _Session.CacheMode; }
			set { _Session.CacheMode = value; }
		}

		public ISessionFactory SessionFactory
		{
			get { return _Session.SessionFactory; }
		}

		public IDbConnection Connection
		{
			get { return _Session.Connection; }
		}

		public bool IsOpen
		{
			get { return _Session.IsOpen; }
		}

		public bool IsConnected
		{
			get { return _Session.IsConnected; }
		}

		public bool DefaultReadOnly
		{
			get { return _Session.DefaultReadOnly; }
			set { _Session.DefaultReadOnly = value; }
		}

		public ITransaction Transaction
		{
			get { return _Session.Transaction; }
		}

		public ISessionStatistics Statistics
		{
			get { return _Session.Statistics; }
		}

        DbConnection ISession.Connection => throw new NotImplementedException();

        public bool IsTypeMapped(Type t)
		{
			return SessionFactory.GetClassMetadata(t) != null;
		}

        public Task FlushAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsDirtyAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task EvictAsync(object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> LoadAsync(Type theType, object id, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> LoadAsync(string entityName, object id, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> LoadAsync(Type theType, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> LoadAsync<T>(object id, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> LoadAsync<T>(object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> LoadAsync(string entityName, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync(object obj, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task ReplicateAsync(object obj, ReplicationMode replicationMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task ReplicateAsync(string entityName, object obj, ReplicationMode replicationMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> SaveAsync(object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(object obj, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> SaveAsync(string entityName, object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(string entityName, object obj, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdateAsync(object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdateAsync(string entityName, object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveOrUpdateAsync(string entityName, object obj, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(object obj, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string entityName, object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string entityName, object obj, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> MergeAsync(object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> MergeAsync(string entityName, object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> MergeAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> MergeAsync<T>(string entityName, T entity, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }

        public Task PersistAsync(object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task PersistAsync(string entityName, object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string entityName, object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string query, object value, IType type, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(string query, object[] values, IType[] types, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task LockAsync(object obj, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task LockAsync(string entityName, object obj, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync(object obj, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IQuery> CreateFilterAsync(object collection, string queryString, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(Type clazz, object id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<object> GetAsync(Type clazz, object id, LockMode lockMode, CancellationToken cancellationToken = default)
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

        public Task<T> GetAsync<T>(object id, LockMode lockMode, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEntityNameAsync(object obj, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ISharedSessionBuilder SessionWithOptions()
        {
            throw new NotImplementedException();
        }

        DbConnection ISession.Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Reconnect(DbConnection connection)
        {
            throw new NotImplementedException();
        }

        DbConnection ISession.Close()
        {
            throw new NotImplementedException();
        }

        public void Save(string entityName, object obj, object id)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(string entityName, object obj, object id)
        {
            throw new NotImplementedException();
        }

        public void Update(string entityName, object obj, object id)
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
