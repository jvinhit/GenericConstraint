using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




/*Sử dụng từ khóa where: 
 khi sử dụng từ khóa where cho phép ta yêu cầu sử dụng đúng kiểu dữ liệu gọi đến Class là Generic ứng với tham số T.
 Nếu cố tình khởi tạo đối tượng khác thì sẽ dính lỗi 
 Ví dụ 1 số ràng buộc về tham số : 
    Where T : class 
        - kiểu Reference này bao gồm interface, delegate và Array
 *  Where T : new(): Khởi tạo class với constructor là không tham số
 *  
 */

namespace GenericAndConstraint
{
    /// <summary>
    /// Khai báo lớp nhân viên với các biến và các thuộc tình
    /// 
    /// </summary>
    public class Employees
    {
        // bien
        string pCode;
        string pName;
        string pAddress;
        int pAge;

        public Employees() { }
        public Employees(string pcode, string pname, string paddress, int page)
        {
            this.pCode = pcode;
            this.pName = pname;
            this.pAddress = paddress;
            this.pAge = page;
        }
        // property: 
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }


    /// <summary>
    ///  Class genericEmployees dùng từ khóa where để ràng buộc kiểu dữ liệu cho tham số T là đối tượng Employees
    ///  TRong lớp này có lớp EmployeesNode
    /// </summary>
    /// 
    public class GenricEmployees<T> where T : Employees
    {

        // Khai bao doi tuong lop employeeNode
        EmployeeNode Info;
        public GenricEmployees()
        {
            // khoi tao info la null
            Info = null;

        }
        // Phuong thuc de them thong tin cua nhan vien
        public void AddInfo(T t)
        {
            // Khoi tao bien kieu doi tuong EmployeeNode
            EmployeeNode n = new EmployeeNode(t);
            n.NextNode = Info;
            Info = n;

        }
        // Khai bao phuong thuc de tra ve tung mẫu tin bằng cách dùng yield return
        public IEnumerator<T> GetEnumerator()
        {
            EmployeeNode current = Info;
            // Su dung phat bieu yield return 
            while(current != null)
            {
                yield return current.Data;
                // Trỏ đến mẫu tin kế tiếp
                current = current.NextNode;

            }
        }
        // Khai bao phuong thuc cho phep tim kiem tung nhan vien
        public T  FindEmployee (string strName)
        {
            EmployeeNode current = Info;
            T t = null;
            while(current != null)
            {

                // Tìm kiếm theo tên của nhân viên
                if(current.Data.Name == strName)
                {
                    // Lay mau tin tìm thấy
                    t = current.Data;
                    break;

                }
                else
                {
                    // Nếu không tìm thấy thì trỏ đến mẩu tin kế tiếp
                    current = current.NextNode;

                }
            }
            // Tìm thấy thì trả về thông tin của nhân viên 
            return t;
        }

        public class EmployeeNode
        {
            //Khai báo biến kiểu EmployeeNode
            EmployeeNode next;
            // Khai bao bien kieu T ( rang buoc la kieu Employee)
            T data;
            public EmployeeNode(T t)
            {
                next = null;
                data = t;
            }
            // Khai bao thuoc tinh NextNode de lay mẫu tin kế tiếp


            public EmployeeNode NextNode
            {
                get { return next; }
                set { next = value; }
            }

            // Khai báo thuộc tính data để lấy thông tin của nhân viên
            public T Data
            {
                get { return this.data; }
                set
                {
                    this.data = value;
                }
            }


        }

        
    }

    class Program
    {
        // Phương thức GetInfo để thêm thông tin cho một số nhân viên : 
        public static GenricEmployees<Employees> GetInfo()
        {
            Console.WriteLine("Adding info for staff");
            // Khai báo biến GenericEmployees: 
            GenricEmployees<Employees> clsGeneric;
            // Khởi tạo biến GenericEmployee
            clsGeneric = new GenricEmployees<Employees>();
            // Khoi tao bien kieu Emplpyee

            Employees emp;
            emp = new Employees();
            // Gan gia tri cho cac thuoc tinh 
            emp.Name = "Vinh";
            emp.Address = "01 Vo Van Ngan , Quan Thu Duc";
            emp.Code = "70000";
            emp.Age = 100;
            // Thêm đối tượng Employees và Generic Employee: 
            clsGeneric.AddInfo(emp);
            // Khoi tao bien kieu Employees
            emp = new Employees("80000", "Tuong Vy", "Phu Nhuan", 22);
            // them doi tuong Employees vao GenericEmployee
            clsGeneric.AddInfo(emp);

            Console.WriteLine();
            // TRa ve doi tuong GenericEmployee
            return clsGeneric;
        }

        // Phuong thuc In thong tin của Nhân Viên ma GenericEmployee dang nắm giữ
        // bằng cách truyển đối tượng này như tham số 

        static void PrintInfo(GenricEmployees<Employees> myGeneric)
        {
            Console.WriteLine("Constraint Data Type");
            // Duyet tung phan tu cua GenericEmployee
            foreach(Employees emp in myGeneric)
            {
                Console.WriteLine("{0}", emp.Name);
                Console.WriteLine("{0}", emp.Address);
                Console.WriteLine("{0}", emp.Code);
                Console.WriteLine("{0}", emp.Age);
                Console.WriteLine();
            
            }
        }

        // Phuong thuc SearchInfo de tim kiem nhan vien
        public static void SearchInfo(GenricEmployees<Employees> myGeneric)
        {
            Console.WriteLine("Search in Generic: ");
            // Yêu cuầ nhập tên cần tìm:
            Console.Write("Nhap ten can tim: ");
            string name = Console.ReadLine();
            Employees emp = myGeneric.FindEmployee(name);
            if (emp != null)
            {
                Console.WriteLine("Tim Thay ");
                Console.Write("{0} ", emp.Name);
                Console.Write("{0} ", emp.Code);
                Console.Write("{0} ", emp.Age);
                Console.Write("{0} ", emp.Address);
            }
            else
                Console.WriteLine("Not Found!");
        }



        static void Main(string[] args)
        {
            GenricEmployees<Employees> clsGeneric;
            // Goi phuong thuc GetInfo de them thong tin

            clsGeneric = GetInfo();
            // in
            PrintInfo(clsGeneric);
            // 
            Console.ReadLine();
            //
            SearchInfo(clsGeneric);
            Console.ReadLine();
        }
    }
}
