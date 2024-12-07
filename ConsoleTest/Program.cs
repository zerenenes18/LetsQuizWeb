// See https://aka.ms/new-console-template for more information

using DataAccess;
using DataAccess.Abstract;
using LetsQuizCore.Entities;

IAdminDal adminDal = new AdminDal();


List<Admin> admins = await adminDal.GetAllAsync();


foreach (Admin admin in admins)
{
    Console.WriteLine(admin.FirstName + " " + admin.LastName);
}
Console.WriteLine("Hello, World!");