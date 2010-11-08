#region Copyright & License
//
// Copyright 2001-2005 The Apache Software Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Data;
using System.Data.SqlClient;
using log4net.Appender;
using log4net.Core;
using System.Transactions;

namespace Spring2.Core.Log4Net.Appender {
    /// <summary>
    /// Simple database appender
    /// </summary>
    /// <remarks>
    /// <para>
    /// This database appender is very simple and does not support a configurable
    /// data schema. The schema supported is hardcoded into the appender.
    /// Also by not extending the AppenderSkeleton base class this appender
    /// avoids the serializable locking that it enforces.
    /// </para>
    /// <para>
    /// This appender can be subclassed to change the database connection
    /// type, or the database schema supported.
    /// </para>
    /// <para>
    /// To change the database connection type the <see cref="GetConnection"/>
    /// method must be overridden.
    /// </para>
    /// <para>
    /// To change the database schema supported by the appender the <see cref="InitializeCommand"/>
    /// and <see cref="SetCommandValues"/> methods must be overridden.
    /// </para>
    /// </remarks>
    public class FastDbAppender : IAppender, IOptionHandler {
	private string m_name;
	private string m_connectionString;

	public string Name {
	    get { return m_name; }
	    set { m_name = value; }
	}

	public string ConnectionString {
	    get { return m_connectionString; }
	    set { m_connectionString = value; }
	}

	public virtual void ActivateOptions() {
	}

	public virtual void Close() {
	}

	public virtual void DoAppend(LoggingEvent loggingEvent) {
#if (!NET_1_1)
	    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress)) {
#endif

		using (IDbConnection connection = GetConnection()) {
		    // Open the connection for each event, this takes advantage
		    // of the builtin connection pooling
		    connection.Open();

		    using (IDbCommand command = connection.CreateCommand()) {
			InitializeCommand(command);

			SetCommandValues(command, loggingEvent);
			command.ExecuteNonQuery();
		    }
		}
#if (!NET_1_1)
		scope.Complete();
	    }
#endif

	}

	public virtual void DoAppend(LoggingEvent[] loggingEvents) {
#if (!NET_1_1)
	    using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress)) {
#endif

		using (IDbConnection connection = GetConnection()) {
		    // Open the connection for each event, this takes advantage
		    // of the builtin connection pooling
		    connection.Open();

		    using (IDbCommand command = connection.CreateCommand()) {
			InitializeCommand(command);

			foreach (LoggingEvent loggingEvent in loggingEvents) {
			    SetCommandValues(command, loggingEvent);
			    command.ExecuteNonQuery();
			}
		    }

#if (!NET_1_1)
		    scope.Complete();
		}
#endif

	    }
	}

	/// <summary>
	/// Create the connection object
	/// </summary>
	/// <returns>the connection</returns>
	/// <remarks>
	/// <para>
	/// This implementation returns a <see cref="SqlConnection"/>.
	/// To change the connection subclass this appender and
	/// return a different connection type.
	/// </para>
	/// </remarks>
	virtual protected IDbConnection GetConnection() {
	    return new SqlConnection(m_connectionString);
	}

	/// <summary>
	/// Initialize the command object supplied
	/// </summary>
	/// <param name="command">the command to initialize</param>
	/// <remarks>
	/// <para>
	/// This method must setup the database command and the
	/// parameters.
	/// </para>
	/// </remarks>
	virtual protected void InitializeCommand(IDbCommand command) {
	    command.CommandType = CommandType.Text;
	    command.CommandText = "INSERT INTO [Log] ([Date],[Logger],[Level],[Thread],[Message],[Exception],[Machine], [Identity], [Application]) VALUES (@Date,@Logger,@Level,@Thread,@Message, @Exception, @Machine, @Identity, @Application)";

	    IDbDataParameter param = null;
			
	    // @Date
	    param = command.CreateParameter();
	    param.ParameterName = "@Date";
	    param.DbType = DbType.DateTime;
	    command.Parameters.Add(param);
			
	    // @Logger
	    param = command.CreateParameter();
	    param.ParameterName = "@Logger";
	    param.DbType = DbType.String;
	    command.Parameters.Add(param);
			
	    // @Level
	    param = command.CreateParameter();
	    param.ParameterName = "@Level";
	    param.DbType = DbType.String;
	    command.Parameters.Add(param);
			
	    // @Thread
	    param = command.CreateParameter();
	    param.ParameterName = "@Thread";
	    param.DbType = DbType.String;
	    command.Parameters.Add(param);
			
	    // @Message
	    param = command.CreateParameter();
	    param.ParameterName = "@Message";
	    param.DbType = DbType.String;
	    command.Parameters.Add(param);
	
	    // @Exception
	    param = command.CreateParameter();
	    param.ParameterName = "@Exception";
	    param.DbType = DbType.String;
	    command.Parameters.Add(param);

	    // @Host
	    param = command.CreateParameter();
	    param.ParameterName = "@Machine";
	    param.DbType = DbType.String;
	    command.Parameters.Add(param);

	    // @Identity
	    param = command.CreateParameter();
	    param.ParameterName = "@Identity";
	    param.DbType = DbType.String;
	    command.Parameters.Add(param);

	    // @Application
	    param = command.CreateParameter();
	    param.ParameterName = "@Application";
	    param.DbType = DbType.String;
	    command.Parameters.Add(param);
	}

	/// <summary>
	/// Set the values for the command parameters
	/// </summary>
	/// <param name="command">the command to update</param>
	/// <param name="loggingEvent">the current logging event to retrieve the values from</param>
	/// <remarks>
	/// <para>
	/// Set the values of the parameters created by the
	/// <see cref="InitializeCommand"/> method.
	/// </para>
	/// </remarks>
	virtual protected void SetCommandValues(IDbCommand command, LoggingEvent loggingEvent) {
	    ((IDbDataParameter)command.Parameters["@Date"]).Value = loggingEvent.TimeStamp;
	    ((IDbDataParameter)command.Parameters["@Logger"]).Value = loggingEvent.LoggerName;
	    ((IDbDataParameter)command.Parameters["@Level"]).Value = loggingEvent.Level.Name;
	    ((IDbDataParameter)command.Parameters["@Thread"]).Value = loggingEvent.ThreadName;
	    ((IDbDataParameter)command.Parameters["@Message"]).Value = loggingEvent.RenderedMessage;
	    if (loggingEvent.ExceptionObject != null) {
		((IDbDataParameter)command.Parameters["@Exception"]).Value = loggingEvent.ExceptionObject.ToString();
	    } else {
		((IDbDataParameter)command.Parameters["@Exception"]).Value = DBNull.Value;
	    }
	    Object machine = loggingEvent.LookupProperty("log4net:HostName");
	    if (machine != null) {
		((IDbDataParameter)command.Parameters["@Machine"]).Value = machine;
	    } else {
		((IDbDataParameter)command.Parameters["@Machine"]).Value = Environment.MachineName;
	    }
	    if (loggingEvent.Identity != null) {
		((IDbDataParameter)command.Parameters["@Identity"]).Value = loggingEvent.Identity;
	    } else {
		((IDbDataParameter)command.Parameters["@Identity"]).Value = DBNull.Value;
	    }
	    Object application = loggingEvent.LookupProperty("Application");
	    if (application != null) {
		((IDbDataParameter)command.Parameters["@Application"]).Value = application;
	    } else {
		((IDbDataParameter)command.Parameters["@Application"]).Value = DBNull.Value;
	    }
	}
    }
}
