using LearnDapper.Model;

namespace LearnDapper.Repo
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetAll();

        Task<Employee> Getbycode(int code);

        Task<List<Employee>> GetAllbyrole(string designation);

        Task<string> Create(Employee employee);

        Task<string> Update(Employee employee, int code);
        Task<string> Remove(int code);
    }
}
