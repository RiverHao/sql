using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace sqlHelper.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class DataServerAttribute : Attribute
    {
        private String _dataServerInfo;
        private String _type;

        public DataServerAttribute(String type)
        {
            _type = type;
        }

        public String DataServerInfo
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"DataBaseOption.xml");
                var result = doc.SelectSingleNode(_type).InnerText;
                return result;
            }
            set
            {
                _dataServerInfo = value;
            }
        }
    }
}
