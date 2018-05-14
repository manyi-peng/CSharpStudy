using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace S101
{
    public partial class _Default : Page, IEmployeeView
    {
        public EmployeePresenter presenter { get; private set; }
        public event EventHandler<DepartmentSelectedEventArgs> DepartmentSelected;

        public _Default()
        {
            this.presenter = new EmployeePresenter(this);
        }

        public void BindDepartments(IEnumerable<string> departments)
        {
            this.DropDownListDepartments.DataSource = departments;
            this.DropDownListDepartments.DataBind();
        }

        public void BindEmployees(IEnumerable<Employee> employees)
        {
            this.GridViewEmployees.DataSource = employees;
            this.GridViewEmployees.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.presenter.Initialize();
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            string department = this.DropDownListDepartments.SelectedValue;
            DepartmentSelectedEventArgs args = new DepartmentSelectedEventArgs(department);
            DepartmentSelected?.Invoke(this, args);
        }
    }

    public class Employee
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Gender { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Department { get; private set; }

        public Employee(string id, string name, string gender, DateTime birthdate, string department)
        {
            this.Id = id;
            this.Name = name;
            this.Gender = gender;
            this.BirthDate = birthdate;
            this.Department = department;
        }
    }

    public class EmployeeRepository
    {
        private static IList<Employee> employees;
        static EmployeeRepository()
        {
            employees = new List<Employee>();
            employees.Add(new Employee("001", "张三", "男", DateTime.Now, "销售部"));
            employees.Add(new Employee("002", "李四", "男", DateTime.Now, "销售部"));
            employees.Add(new Employee("003", "王五", "男", DateTime.Now, "人事部"));
            employees.Add(new Employee("004", "赵六", "男", DateTime.Now, "人事部"));
            employees.Add(new Employee("005", "周斌", "男", DateTime.Now, "销售部"));
        }

        public IEnumerable<Employee> GetEmployees(string department = null)
        {
            if (string.IsNullOrEmpty(department))
            {
                return employees;
            }
            return employees.Where(e => e.Department == department).ToArray();
        }
    }

    public interface IEmployeeView
    {
        void BindEmployees(IEnumerable<Employee> employees);
        void BindDepartments(IEnumerable<string> departments);
        event EventHandler<DepartmentSelectedEventArgs> DepartmentSelected;
    }

    public class DepartmentSelectedEventArgs : EventArgs
    {
        public string Department { get; private set; }
        public DepartmentSelectedEventArgs(string department)
        {
            this.Department = department;
        }
    }

    public class EmployeePresenter
    {
        public IEmployeeView View { get; private set; }
        public EmployeeRepository Repository { get; private set; }

        public EmployeePresenter(IEmployeeView view)
        {
            this.View = view;
            this.Repository = new EmployeeRepository();
            this.View.DepartmentSelected += OnDepartmentSelected;
        }

        public void Initialize()
        {
            IEnumerable<Employee> employees = this.Repository.GetEmployees();
            this.View.BindEmployees(employees);
            string[] departments = new string[] { "", "销售部", "采购部", "人事部", "IT部" };
            this.View.BindDepartments(departments);
        }

        private void OnDepartmentSelected(object sender, DepartmentSelectedEventArgs e)
        {
            string department = e.Department;
            var employees = this.Repository.GetEmployees(department);
            this.View.BindEmployees(employees);
        }
    }
}