using Dapper;
using HRMS.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace HRMS.Data.Context;

public class DbContext(IOptionsSnapshot<AppConfigs> appConfigs)
{
	readonly string _connectionString = appConfigs.Value.HRMSConnection;

	public async Task<IEnumerable<T>> CallStoreProcedureDirectly<T>(string storeName, object? storeParameters = null)
	{
		using var dbConnection = new SqlConnection(_connectionString);
		dbConnection.Open();
		var result = await dbConnection.QueryAsync<T>(storeName, storeParameters, commandTimeout: 99999, commandType: CommandType.StoredProcedure);
		dbConnection.Close();
		return result;

	}

	public async Task<IEnumerable<T>> ExecuteQuery<T>(string query, object? param = null)
	{
		using var dbConnection = new SqlConnection(_connectionString);
		dbConnection.Open();
		var result = await dbConnection.QueryAsync<T>(query, param, commandTimeout: 99999, commandType: CommandType.Text);
		dbConnection.Close();
		return result;
	}
}
