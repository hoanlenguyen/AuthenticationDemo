using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationDemo.Roles.Permissions
{
    public static class Permissions
    {
        public static class Students
        {
            public const string View = "Permissions.Students.View";
            public const string Create = "Permissions.Students.Create";
            public const string Edit = "Permissions.Students.Edit";
            public const string Delete = "Permissions.Students.Delete";
        }
        
        public static class Configurations
        {
            public const string View = "Permissions.Configurations.View";
            public const string Create = "Permissions.Configurations.Create";
            public const string Edit = "Permissions.Configurations.Edit";
            public const string Delete = "Permissions.Configurations.Delete";
        }
    }
}
