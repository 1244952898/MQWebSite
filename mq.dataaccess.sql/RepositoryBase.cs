using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using match.dataaccess.mysql;

namespace mq.dataaccess.sql
{
    /// <summary>
    /// 数据仓储基类涉及到的操作都是针对单表的操作，需要多表联合操作，需要在数据仓储基类的子类中另外实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        //初始化实例对象的数据库操作连接对象
        protected IDbConnection conn = null;// MPConnection.GetConnection();
        public RepositoryBase()
        {
            //if (conn.State != ConnectionState.Open)
            //{
            //    conn.Open();
            //}
        }

        public IDbConnection Connection
        {
            get { return conn; }
            set { conn = value; }
        }
        private void OpenConnection()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
        }

        private void Close()
        {
            if (conn != null)
            {
                conn.Close();
            }

        }


        //Predicates 的用法
        //https://github.com/tmsmith/Dapper-Extensions/wiki/Predicates#simple-fieldpredicate-operation 
        public bool IsExist(PredicateGroup predicateGroup)
        {
            //var existsPred = Predicates.Exists<T>(subPred);
            bool bExistEntity = false;
            T entity = null;

            //谓词系统在Dapper-Extensions中是非常容易去使用的。现在我们使用下面这个Model来展开它的用法。
            //http://www.cnblogs.com/Sinte-Beuve/p/4617374.html 

            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();
                if (conn.State == ConnectionState.Open)
                {
                    var list = conn.GetList<T>(predicateGroup, null, null, null, false);
                    entity = list.Any() ? list.First<T>() : null;
                    if (entity != null)
                    {
                        bExistEntity = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                Close();
            }
            return bExistEntity;

        }
        public bool IsExist(string existsSqlWithParams, T entity)
        {
            bool bExistEntity = false;
            T _entity = null;

            //谓词系统在Dapper-Extensions中是非常容易去使用的。现在我们使用下面这个Model来展开它的用法。
            //http://www.cnblogs.com/Sinte-Beuve/p/4617374.html    

            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    object countObject = conn.ExecuteScalar(existsSqlWithParams, entity);
                    if (countObject != null)
                    {
                        long countRecourd = (long)countObject;
                        if (countRecourd > 0)
                        {
                            bExistEntity = true;
                        }
                    }
                    else
                    {
                        bExistEntity = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return bExistEntity;

        }
        public T GetModel(PredicateGroup predicateGroup)
        {
            T entity = null;

            //谓词系统在Dapper-Extensions中是非常容易去使用的。现在我们使用下面这个Model来展开它的用法。
            //http://www.cnblogs.com/Sinte-Beuve/p/4617374.html 

            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    var list = conn.GetList<T>(predicateGroup, null, null, null, false);
                    entity = list != null && list.Any() ? list.First<T>() : null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return entity;
        }

        public T GetModel(string strSql, DynamicParameters dynamicParams)
        {
            T entity = null;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    var list = conn.Query<T>(strSql.ToString(), dynamicParams);
                    entity = list != null && list.Any() ? list.First<T>() : null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return entity;
        }

        public long Add(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            long autoID = 0;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    autoID = conn.Insert(entity, transaction, commandTimeout);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    Close();
                }
            }
            return autoID;
        }
        public void BatchAdd(IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Insert(entityList, transaction, commandTimeout);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    Close();
                }
            }
        }
        public long Add(string insertSqlWithParams, T entity, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int effectRow = 0;
            #region  这是一个Insert语句的例子
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("INSERT INTO `mp_user`( ");
            //strSql.Append("`UserName`,`UserNick`,`UserType`,`Tel`,`IsVerifyTel`,`InstitutionName`,`TotalIntegral`,`IsInstituAuthentication`,`IsRegisterHere`,`CreateOn`,`LatestUpdate`)");
            //strSql.Append(" VALUES (");
            //strSql.Append("@UserName,@UserNick,@UserType,@Tel,@IsVerifyTel,@InstitutionName,@TotalIntegral,@IsInstituAuthentication,@IsRegisterHere,@CreateOn,@LatestUpdate);"); 
            #endregion

            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    effectRow = conn.Execute(insertSqlWithParams, entity, transaction, commandTimeout, commandType);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    Close();
                }

            }

            return effectRow;
        }
        public bool Delete(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            bool bDeleted = false;//默认没有删除成功
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    bDeleted = conn.Delete<T>(entity, transaction, commandTimeout);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    Close();
                }

            }
            return bDeleted;

        }
        public bool Delete(string deleteSqlWithParams, T entity, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            bool bDeleted = false;//默认没有删除成功
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    int delrows = conn.Execute(deleteSqlWithParams, entity, transaction, commandTimeout, commandType);
                    if (delrows > 0)
                    {
                        bDeleted = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    Close();
                }

            }
            return bDeleted;

        }

        public bool BatchDelete(string deleteSqlWithParams, IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            bool bDeleted = false;//默认没有删除成功
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                using (transaction = conn.BeginTransaction())
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        var excuteCOunt = 0;
                        foreach (T item in entityList)
                        {
                            int updaterows = conn.Execute(deleteSqlWithParams, item, transaction, commandTimeout, commandType);
                            if (updaterows > 0)
                            {
                                bDeleted = true;
                                excuteCOunt++;
                            }
                            else
                            {
                                break;
                            }

                        }
                        if (excuteCOunt != entityList.Count())
                        {
                            bDeleted = false;
                            transaction.Rollback();
                        }
                        else
                        {
                            transaction.Commit();
                            bDeleted = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                if (transaction == null)
                {
                    Close();
                }
            }
            return bDeleted;
        }

        public bool Update(T entity, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            bool bUpdated = false;//默认没有更新成功

            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    bUpdated = conn.Update<T>(entity, transaction, commandTimeout);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    Close();
                }

            }
            return bUpdated;
        }
        public bool Update(string updateSqlWithParams, T entity, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            bool bUpdated = false;//默认没有更新成功

            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    int updaterows = conn.Execute(updateSqlWithParams, entity, transaction, commandTimeout, commandType);
                    if (updaterows > 0)
                    {
                        bUpdated = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (transaction == null)
                {
                    Close();
                }

            }
            return bUpdated;
        }

        public bool BatchUpdate(IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            bool bUpdated = false;//默认没有更新成功

            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();
                using (transaction = conn.BeginTransaction())
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        var excuteCOunt = 0;
                        foreach (T item in entityList)
                        {
                            bool updaterows = conn.Update<T>(item, transaction, commandTimeout);
                            if (updaterows)
                            {
                                bUpdated = true;
                                excuteCOunt++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (excuteCOunt != entityList.Count())
                        {
                            bUpdated = false;
                            transaction.Rollback();
                        }
                        else
                        {
                            transaction.Commit();
                            bUpdated = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                if (transaction == null)
                {
                    Close();
                }
            }
            return bUpdated;
        }

        public bool BatchUpdate(string updateSqlWithParams, IEnumerable<T> entityList, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            bool bUpdated = false;//默认没有更新成功

            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                using (transaction = conn.BeginTransaction())
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        var excuteCOunt = 0;
                        foreach (T item in entityList)
                        {
                            int updaterows = conn.Execute(updateSqlWithParams, item, transaction, commandTimeout, commandType);
                            if (updaterows > 0)
                            {
                                bUpdated = true;
                                excuteCOunt++;
                            }
                            else
                            {
                                break;
                            }

                        }
                        if (excuteCOunt != entityList.Count())
                        {
                            bUpdated = false;
                            transaction.Rollback();
                        }
                        else
                        {
                            transaction.Commit();
                            bUpdated = true;
                        }
                    }
                }

                //transaction = conn.BeginTransaction();
                //transaction.Rollback();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                if (transaction == null)
                {
                    Close();
                }

            }

            return bUpdated;
        }
        public object ExcuteScalar(string strSql, DynamicParameters dynamicParams)
        {
            object returnValue = null;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    returnValue = conn.ExecuteScalar(strSql, dynamicParams);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return returnValue;
        }

        public int ExcuteProcedure(string procedureName, DynamicParameters dynamicParams)
        {
            int result = -1;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    result = conn.Execute(procedureName, dynamicParams, null, null, CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return result;
        }

        public IEnumerable<T> QueryProcedure(string procedureName, DynamicParameters dynamicParams)
        {
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    return conn.Query<T>(procedureName, dynamicParams, null, true, null, CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return null;
        }

        //获取符合条件的记录数
        public long QueryRecordCount(PredicateGroup predicateGroup)
        {
            long resultCount = 0;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    resultCount = conn.Count<T>(predicateGroup, null, null);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return resultCount;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicateGroup"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex">分页从0开始，第一页传0</param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryList(PredicateGroup predicateGroup, IList<ISort> sort, int pageIndex, int pageSize, out long recordCount)
        {
            IEnumerable<T> result = null;
            #region 查询条件和排序字段
            //var pgMain = new PredicateGroup { Operator = GroupOperator.Or, Predicates = new List<IPredicate>() };
            //var pga = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            //pga.Predicates.Add(Predicates.Field<mp_user>(f => f.IsVerifyTel, Operator.Eq, 0));
            ////pga.Predicates.Add(Predicates.Field<mp_user>(f => f.LastName, Operator.Like, "Br%"));
            //pgMain.Predicates.Add(pga);

            //var pgb = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            ////pgb.Predicates.Add(Predicates.Field<mp_user>(f => f.Active, Operator.Eq, false));
            ////pgb.Predicates.Add(Predicates.Field<mp_user>(f => f.FirstName, Operator.Like, "Pa%", true /* NOT */ ));
            //pgMain.Predicates.Add(pgb);

            //IList<ISort> sort = new List<ISort>() { Predicates.Sort<mp_user>(f => f.CreateOn, true) };   

            #endregion
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    result = conn.GetPage<T>(predicateGroup, sort, pageIndex, pageSize);//,HY.DataAccess.DatabaseType.MySql
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }

            //获取符合条件的总记录数            
            recordCount = QueryRecordCount(predicateGroup);
            return result;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicateGroup"></param>
        /// <param name="sort"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryList(PredicateGroup predicateGroup, IList<ISort> sort)
        {
            IEnumerable<T> result = null;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    result = conn.GetList<T>(predicateGroup, sort);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return result;
        }


        /// <summary>
        /// 查询符合条件的记录数
        /// </summary>
        /// <param name="fullCountSql"></param>
        /// <returns></returns>
        public long QueryRecordCount(string fullCountSql)
        {
            long resultCount = 0;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    object countObject = conn.ExecuteScalar(fullCountSql);
                    resultCount = (long)countObject;//装箱后的数据，直接拆箱                  
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return resultCount;

        }

        /// <summary>
        /// 查询执行sum语句的返回值
        /// </summary>
        /// <param name="fullCountSql"></param>
        /// <returns></returns>
        public long QuerySumRecordCount(string fullCountSql)
        {
            long resultCount = 0;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    //Sum 返回值是Decimal类型 需要先转换为Decimal再转换为Long
                    object countObject = conn.ExecuteScalar(fullCountSql);
                    decimal tempCount = (decimal)countObject;
                    resultCount = (long)tempCount;//装箱后的数据，直接拆箱                  
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return resultCount;

        }
        /// <summary>
        /// 查询带有参数执行sum，avg语句的返回值
        /// </summary>
        /// <param name="fullCountSql"></param>
        /// <returns></returns>
        public float QuerySumOrAvgRecordCount(string fullCountSql, DynamicParameters dynamicParams)
        {
            float resultCount = 0;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    //Sum 返回值是Decimal类型 需要先转换为Decimal再转换为Long
                    object countObject = conn.ExecuteScalar(fullCountSql, dynamicParams);
                    decimal tempCount = (decimal)countObject;
                    resultCount = (float)tempCount;//装箱后的数据，直接拆箱                  
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return resultCount;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="querySqlStruct">pageIndex分页从0开始，第一页传0</param>
        /// <param name="recordCount"></param>
        /// <param name="limitResultSetMaxCount"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryList(QuerySqlStruct querySqlStruct, out long recordCount, bool limitResultSetMaxCount = true)
        {
            IEnumerable<T> result = null;
            recordCount = 0;
            if (string.IsNullOrEmpty(querySqlStruct.TableName))
            {
                throw new ArgumentNullException("QuerySqlStruct.TableName", "QuerySqlStruct.TableName is null");
            }
            try
            {
                #region 查询符合条件的有排序的，限定返回条数的分页结果集

                StringBuilder strSql = new StringBuilder();
                if (querySqlStruct.Fields.Count > 0)
                {
                    strSql.AppendFormat("select {0} from {1} ", string.Join(",", querySqlStruct.Fields.ToArray<string>()), querySqlStruct.TableName);
                }
                else
                {
                    throw new ArgumentNullException("QuerySqlStruct.Fields", "QuerySqlStruct.Fields.Count<=0");
                }
                strSql.AppendFormat("where  1=1 ");

                if (querySqlStruct.QueryCriterias.Count > 0)
                {
                    strSql.AppendFormat(" {0} ", string.Join(" ", querySqlStruct.QueryCriterias.ToArray<string>()));
                }

                if (!string.IsNullOrEmpty(querySqlStruct.OrderBy))
                {
                    strSql.AppendFormat(" {0} ", querySqlStruct.OrderBy);
                }
                if (querySqlStruct.PageIndex >= 0)
                {
                    strSql.AppendFormat(" limit {0}", querySqlStruct.PageIndex * querySqlStruct.PageSize);

                    //如果是pageSize是等于0或者pageSize大于等于100 并且limitResultSetMaxCount=true，那么取记录数默认给出最多1000条，否则根据指定查找。
                    if (querySqlStruct.PageSize > 0 && querySqlStruct.PageSize < 100)
                    {
                        strSql.AppendFormat(",{0}", querySqlStruct.PageSize);
                    }
                    else
                    {
                        if (limitResultSetMaxCount)
                            strSql.AppendFormat(",{0}", 1000);
                        else
                            strSql.AppendFormat(",{0}", querySqlStruct.PageSize <= 0 ? 1000 : querySqlStruct.PageSize);
                    }
                }
                try
                {
                    if (conn == null)
                    {
                        conn = MPConnection.GetConnection();
                    }
                    OpenConnection();

                    if (conn.State == ConnectionState.Open)
                    {
                        result = conn.Query<T>(strSql.ToString());
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                #endregion

                //获取符合条件的总记录数
                StringBuilder strSqlCount = new StringBuilder();
                strSqlCount.AppendFormat("select {0} from {1} ", "count(*)", querySqlStruct.TableName);
                strSqlCount.Append("where 1=1  ");
                strSqlCount.AppendFormat(" {0} ", string.Join(" ", querySqlStruct.QueryCriterias.ToArray<string>()));
                recordCount = QueryRecordCount(strSqlCount.ToString());
                Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return result;
        }

        /// <summary>
        /// 查询符合条件的记录数
        /// </summary>
        /// <param name="fullCountSql"></param>
        /// <returns></returns>
        public long QueryRecordCount(string fullCountSql, DynamicParameters dynamicParams)
        {
            long resultCount = 0;
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                if (conn.State == ConnectionState.Open)
                {
                    object countObject = conn.ExecuteScalar(fullCountSql, dynamicParams);
                    resultCount = (long)countObject;//装箱后的数据，直接拆箱                  
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return resultCount;

        }

        /// <summary>
        /// 参数化查询符合条件的记录,返回总记录数
        /// </summary>
        /// <param name="strSql">分页取数据的完整sql语句，所有赋值都参数化，例如：select * from mp_user where 1=1 username=@username and usernick like @usernick and usertype=@usertype " </param>
        /// <param name="dynamicParams">参数的动态参数列表</param>
        /// <param name="recordCount">如果需要返回总记录数，则保证strSqlCount是一条获取参数的语句</param>
        /// <param name="strSqlCount">获取符合条件的总记录数，所有赋值都参数化，例如：select count(*) from mp_user  where 1=1 username=@username and usernick like @usernick and usertype=@usertype </param>
        /// <param name="limitResultSetMaxCount">是否限制最多返回的记录数</param>
        /// <returns></returns>
        public IEnumerable<T> QueryList(string strSql, DynamicParameters dynamicParams, out long recordCount, string strSqlCount, bool limitResultSetMaxCount = true)
        {
            #region 动态参数如何组织
            //string sqlText = "select * from mp_user where 1=1";
            //var p = new DynamicParameters();
            //if (!string.IsNullOrEmpty(username))
            //{
            //    sqlText += " and username like @username";
            //    p.Add("username", username + "%", System.Data.DbType.String, System.Data.ParameterDirection.Input);
            //}

            //if (!string.IsNullOrEmpty(usernick))
            //{
            //    sqlText += " and usernick like @usernick";
            //    p.Add("usernick", "%" + usernick + "%", System.Data.DbType.String, System.Data.ParameterDirection.Input);
            //}

            //if (!string.IsNullOrEmpty(usertype))
            //{
            //    sqlText += " and usertype=@usertype";
            //    p.Add("usertype", usertype, System.Data.DbType.Int16, System.Data.ParameterDirection.Input);
            //}
            #endregion

            IEnumerable<T> result = null;
            recordCount = 0;
            if (string.IsNullOrEmpty(strSql))
            {
                throw new ArgumentNullException("sql", "sql is null or empty");
            }
            try
            {
                #region 查询符合条件的有排序的，限定返回条数的分页结果集
                try
                {
                    if (conn == null)
                    {
                        conn = MPConnection.GetConnection();
                    }
                    OpenConnection();
                    if (conn.State == ConnectionState.Open)
                    {
                        result = conn.Query<T>(strSql.ToString(), dynamicParams);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                #endregion

                //获取符合条件的总记录数
                if (!string.IsNullOrEmpty(strSqlCount))
                {
                    recordCount = QueryRecordCount(strSqlCount, dynamicParams);
                }
                else
                {
                    recordCount = 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return result;
        }


        /// <summary>
        /// 参数化查询符合条件的记录，不返回总记录数
        /// </summary>
        /// <param name="strSql">分页取数据的完整sql语句，所有赋值都参数化，例如：select * from mp_user where 1=1 username=@username and usernick like @usernick and usertype=@usertype " </param>
        /// <param name="dynamicParams">参数的动态参数列表</param>
        /// <param name="recordCount">如果需要返回总记录数，则保证strSqlCount是一条获取参数的语句</param>
        /// <param name="strSqlCount">获取符合条件的总记录数，所有赋值都参数化，例如：select count(*) from mp_user  where 1=1 username=@username and usernick like @usernick and usertype=@usertype </param>
        /// <param name="limitResultSetMaxCount">是否限制最多返回的记录数</param>
        /// <returns></returns>
        public IEnumerable<T> QueryList(string strSql, DynamicParameters dynamicParams, bool limitResultSetMaxCount = true)
        {
            #region 动态参数如何组织
            //string sqlText = "select * from mp_user where 1=1";
            //var p = new DynamicParameters();
            //if (!string.IsNullOrEmpty(username))
            //{
            //    sqlText += " and username like @username";
            //    p.Add("username", username + "%", System.Data.DbType.String, System.Data.ParameterDirection.Input);
            //}

            //if (!string.IsNullOrEmpty(usernick))
            //{
            //    sqlText += " and usernick like @usernick";
            //    p.Add("usernick", "%" + usernick + "%", System.Data.DbType.String, System.Data.ParameterDirection.Input);
            //}

            //if (!string.IsNullOrEmpty(usertype))
            //{
            //    sqlText += " and usertype=@usertype";
            //    p.Add("usertype", usertype, System.Data.DbType.Int16, System.Data.ParameterDirection.Input);
            //}
            #endregion

            IEnumerable<T> result = null;
            if (string.IsNullOrEmpty(strSql))
            {
                throw new ArgumentNullException("sql", "sql is null or empty");
            }
            try
            {
                #region 查询符合条件的有排序的，限定返回条数的分页结果集
                try
                {
                    if (conn == null)
                    {
                        conn = MPConnection.GetConnection();
                    }
                    OpenConnection();
                    if (conn.State == ConnectionState.Open)
                    {
                        result = conn.Query<T>(strSql.ToString(), dynamicParams);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                #endregion
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
            return result;
        }

        /// <summary>
        /// 执行带参数的SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns>返回受影响的行数</returns>
        public int ExecuteSql(string sql, object param = null)
        {
            try
            {
                if (conn == null)
                {
                    conn = MPConnection.GetConnection();
                }
                OpenConnection();

                return conn.Execute(sql, param);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Close();
            }
        }

        #region 析构函数，释放资源
        ~RepositoryBase()
        {
            if (conn != null)
            {
                conn.Close();
            }
            Dispose();
        }

        private bool IsDisposed = false;//是否释放资源，默认是没有释放
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool Disposing)
        {
            if (!IsDisposed)
            {
                if (Disposing)
                {
                    if (conn != null && conn.State == ConnectionState.Open)
                        conn.Close();
                    //Clean Up managed resources  
                }
                //Clean up unmanaged resources  
            }
            IsDisposed = true;
        }
        #endregion
    }

    /// <summary>
    /// 按照“读取数据库字段”，“表名”，“条件”，排序等参数，拼接出需要使用的sql语句。
    /// </summary>
    public struct QuerySqlStruct
    {
        /// <summary>
        /// 需要查询出来的字段名
        /// </summary>
        public IList<string> Fields { get; set; }//

        public string TableName { get; set; }

        /// <summary>
        /// 需要查询条件，如：and (username='abc' and sex='男')   or (age>20 and city='北京')
        /// </summary>
        public IList<string> QueryCriterias { get; set; }//需要查询条件，如：and (username='abc' and sex='男')   or (age>20 and city='北京')

        //排序字段，如：order by id desc,name asc
        public string OrderBy { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public DynamicParameters DynamicParams { get; set; }

        //public QuerySqlStruct(IList<string> fields,string tableName,IList<string> queryCriterias,string orderBy,int pageIndex,int pageSize)
        //{
        //    //结构体不能包含无参的构造函数
        //    this.Fields = fields;
        //    TableName = tableName;
        //    QueryCriterias = new List<string>() {  };
        //    OrderBy = orderBy;
        //    PageIndex = pageIndex;
        //    PageSize = pageSize;
        //    #region
        //    //Fields = new List<string>() { "*"};
        //    //TableName = null;
        //    //QueryCriterias = new List<string>() {  };
        //    //OrderBy = string.Empty;
        //    //PageIndex = 1;
        //    //PageSize = 10;
        //    #endregion
        //}
    }
}
