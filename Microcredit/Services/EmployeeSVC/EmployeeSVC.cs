using Microcredit.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Microcredit.ClassProject.EmployeeSVC
{
    public class EmployeeSVC : IEmployee
    {

        private readonly ApplicationDbContext _db;
        public EmployeeSVC(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<List<EmployeesT>> GETEmployeesAsync()
        {
            List<EmployeesT> GETALLCustomer = new();

            try
            {
                GETALLCustomer = await _db.Employees.OrderBy(x => x.EmployeeName).ToListAsync();

            }
            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

            }
            return GETALLCustomer;
        }

        public async Task<EmployeesT> GetEmployeesByIdAsync(int EmployeeId)
        {
            var GetEmployyeById = (EmployeesT)null;
            try
            {
                if (EmployeeId != 0)
                {
                    GetEmployyeById = await _db.Employees.FindAsync(EmployeeId);

                }

            }

            catch (Exception ex)
            {

                Log.Error("Error while creating user {Error} {StackTrace} {InnerException} {Source}",
                                      ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return GetEmployyeById;

        }
        public async Task<ResponseObject> CreateEmployeesAsync(EmployeesT employeeT)
        {

            ResponseObject responseObject = new ResponseObject();

            await using var dbContextTransaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var AddEmployee = new EmployeesT
                {
                    EmployeeName = employeeT.EmployeeName,
                    EmployeeAddress = employeeT.EmployeeAddress,
                    EmployeePhone = employeeT.EmployeePhone,
                    EmployeeSalary = employeeT.EmployeeSalary,
                    Notes = employeeT.Notes,
                    UsersID = 1
                };
                var result = await _db.Employees.AddAsync(AddEmployee);
                await _db.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
                responseObject.IsValid = true;
                responseObject.Message = "Added successfully";
                responseObject.Data = DateTime.Now.ToString();



            }
            catch (Exception ex)
            {

                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
        ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                await dbContextTransaction.RollbackAsync();

            }
            return responseObject;



        }
        public async Task<bool> UpdateEmployeesAsync(int EmployeeId, EmployeesT employeeT)
        {
            ResponseObject responseObject = new();

            if (EmployeeId == employeeT.EmployeeId)
            {
                _db.Entry(employeeT).State = EntityState.Modified;

            }
            try
            {
                if (employeeT == null)
                {
                    responseObject.Message = "Error Please check that all fields are entered";

                }

                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                if (!EmployeeExists(EmployeeId))

                    Log.Error("Error while Update Category {Error} {StackTrace} {InnerException} {Source}",
                ex.Message, ex.StackTrace, ex.InnerException, ex.Source);



                return false;
            }


        }



        private bool EmployeeExists(int EmployeeId)
        {

            return _db.Employees.Any(x => x.EmployeeId == EmployeeId);
        }

        public async Task<bool> DeleteEmployeesAsync(int EmployeeId)
        {
            var GETEmployeeId = await _db.Employees.FindAsync(EmployeeId);
            ResponseObject responseObject = new();
            if (GETEmployeeId == null)
            {
                responseObject.Message = "Error Id IS NULL";
                return false;
            }

            _db.Employees.Remove(GETEmployeeId);
            _db.SaveChanges();

            return true;
        }
    }
}
