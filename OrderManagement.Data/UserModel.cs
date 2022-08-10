using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Data
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserTypeId { get; set; }
        public UserTypeModel UserType { get; set; }
    }


    public class UserTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
