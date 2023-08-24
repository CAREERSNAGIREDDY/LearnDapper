using Dapper;
using LearnDapper.Model;
using LearnDapper.Model.Data;
using System.Data;

namespace LearnDapper.Repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DapperDBContext _context;
        public EmployeeRepo(DapperDBContext context)
        {
            this._context = context;
        }

        public async Task<List<Employee>> GetAll()
        {
            string query = "select * from tbl_employee";
            using (var connection = this._context.CreateConnection())
            {
                var emplist = await connection.QueryAsync<Employee>(query);
                return emplist.ToList();
            }
        }

        public async Task<Employee> Getbycode(int code)
        {
            string query = "select * from tbl_employee where code=@code";
            using (var connection = this._context.CreateConnection())
            {
                var emplist = await connection.QueryFirstOrDefaultAsync<Employee>(query, new { code });
                return emplist;
            }
        }

        public async Task<List<Employee>> GetAllbyrole(string designation)
        {
            //string query = "exec SP_GETEMPLOYEEBYROLE @designation";
            //using (var connection = this._context.CreateConnection())
            //{
            //    var emplist = await connection.QueryAsync<Employee>(query, new { designation });
            //    return emplist.ToList();
            //}

            string query = "SP_GETEMPLOYEEBYROLE";
            using (var connection = this._context.CreateConnection())
            {
                var emplist = await connection.QueryAsync<Employee>(query, new { designation }, commandType: CommandType.StoredProcedure);
                return emplist.ToList();
            }
        }

        public async Task<string> Create(Employee employee)
        {
            string response = string.Empty;
            string query = "insert into tbl_employee(name,email,Phone,designation) values (@name,@email,@Phone,@designation)";
            var parameters = new DynamicParameters();
            parameters.Add("name",employee.name,DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            parameters.Add("Phone", employee.phone, DbType.String);
            parameters.Add("designation", employee.designation, DbType.String);
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                response = "Inseeted Successfully";
            }
            return response;
        }

        public async Task<string> Update(Employee employee, int code)
        {
            string response = string.Empty;
            string query = "update tbl_employee set name = @name, email = @email, Phone = @Phone, designation = @designation where code=@code";
            var parameters = new DynamicParameters();
            parameters.Add("code", code, DbType.Int32);
            parameters.Add("name", employee.name, DbType.String);
            parameters.Add("email", employee.email, DbType.String);
            parameters.Add("Phone", employee.phone, DbType.String);
            parameters.Add("designation", employee.designation, DbType.String);
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                response = "Updated Successfully";
            }
            return response;
        }

        public async Task<string> Remove(int code)
        {
            string response = string.Empty;
            string query = "delete from tbl_employee where code=@code";
            using (var connection = this._context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { code });
                response = "pass";                
            }
            return response;
        }


    }
}
