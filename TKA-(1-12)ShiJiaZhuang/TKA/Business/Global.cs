using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TKA.Business
{
    public class Global
    {
        private static readonly Global m_Instence = new Global();
        public static Global Instence
        {
            get { return m_Instence; }
        }
        private Global()
        {
        }
    }
}
