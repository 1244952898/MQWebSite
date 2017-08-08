using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DapperExtensions;
using mq.dataaccess.sql;

namespace mq.dataaccess
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        bool IsExist(PredicateGroup predicateGroup);
        bool IsExist(string existsSqlWithParams, T entity);
        T GetModel(PredicateGroup predicateGroup);
        T GetModel(string strSql, DynamicParameters dynamicParams);
        long Add(T entity, IDbTransaction transaction = null, int? commandTimeout = null);
        long Add(string updateSqlWithParams, T entity, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        void BatchAdd(IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null);

        bool Delete(T entity, IDbTransaction transaction = null, int? commandTimeout = null);
        bool Delete(string deleteSqlWithParams, T entity, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool BatchUpdate(IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="deleteSqlWithParams"></param>
        /// <param name="entityList"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        bool BatchDelete(string deleteSqlWithParams, IEnumerable<T> entityList, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null);

        bool Update(T entity, IDbTransaction transaction = null, int? commandTimeout = null);
        bool Update(string updateSqlWithParams, T entity, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        bool BatchUpdate(string updateSqlWithParams, IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        long QueryRecordCount(PredicateGroup predicateGroup);
        /// <summary>
        /// 查询执行sum语句的返回值 （第一行第一个结果）
        /// </summary>
        /// <param name="fullCountSql"></param>
        /// <returns></returns>
        long QuerySumRecordCount(string fullCountSql);
        long QueryRecordCount(string fullCountSql);
        long QueryRecordCount(string fullCountSql, DynamicParameters dynamicParams);
        float QuerySumOrAvgRecordCount(string fullCountSql, DynamicParameters dynamicParams);
        IEnumerable<T> QueryList(PredicateGroup predicateGroup, IList<ISort> sort, int pageIndex, int pageSize, out long recordCount);
        IEnumerable<T> QueryList(PredicateGroup predicateGroup, IList<ISort> sort);
        IEnumerable<T> QueryList(QuerySqlStruct querySqlStruct, out long recordCount, bool limitResultSetMaxCount = true);
        IEnumerable<T> QueryList(string strSql, DynamicParameters dynamicParams, out long recordCount, string strSqlCount, bool limitResultSetMaxCount = true);
        IEnumerable<T> QueryList(string strSql, DynamicParameters dynamicParams, bool limitResultSetMaxCount = true);
        /// <summary>
        /// Execute parameterized SQL
        /// </summary>
        int ExecuteSql(string strSql, object param = null);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        int ExcuteProcedure(string procedureName, DynamicParameters dynamicParams);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        IEnumerable<T> QueryProcedure(string procedureName, DynamicParameters dynamicParams);
    }
}
