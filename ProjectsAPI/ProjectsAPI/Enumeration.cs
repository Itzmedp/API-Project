    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

namespace ProjectAPI.Business
{
    public enum Enumeration
    {
        [Description("Admin")]
        Admin,
        [Description("Junior")]
        Junior,
        [Description("Intern")]
        Intern,
        [Description("User")]
        User,
        [Description("Lead")]
        Lead

    }
}
