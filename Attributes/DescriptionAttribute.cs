using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sqlHelper.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class DescriptionAttribute:Attribute
    {
        private String _description;

        public String Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public DescriptionAttribute(String str)
        {
            _description = str;
        }
    }
}
