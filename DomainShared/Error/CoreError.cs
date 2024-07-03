using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DomainShared.Error
{
    public static class CoreError
    {


        public static string IsDuplicateException(string name)
        {
            return $"{name} تکراری است";
        }

        public static string NotFound(string name)
        {
            return $"{name} یافت نشد";
        }

        public static string InvalidId(string name)
        {
            return $"شناسه {name} نامعتبر است";
        }

        public static string Invalid(string name)
        {
            return $"{name} نامعتبر است";
        }

        /// <summary>
        /// از ... کمتر باشد. برای کاراکتر 
        /// </summary>
        /// <param name="name1">ParamName</param>
        /// <param name="name2">MinChar</param>
        /// <returns></returns>
        public static string IsLess(string paramName, string name2)
        {
            return $"{paramName} باید از {name2} کاراکتر کمتر باشد";
        }

        /// <summary>
        /// از ... بیشتر باشد. برای کاراکترها
        /// </summary>
        /// <param name="name1">ParamName</param>
        /// <param name="name2">MinChar</param>
        /// <returns></returns>
        public static string IsMore(string paramName, string name2)
        {
            return $"{paramName} باید از {name2} کاراکتر بیشتر باشد";
        }

        /// <summary>
        /// از ... بزرگتر باشد.برای اعداد
        /// </summary>
        /// <param name="paramName">ParamName</param>
        /// <param name="name2">MaxNum</param>
        /// <returns></returns>
        public static string IsMoreThan(string paramName, string name2)
        {
            return $"{paramName} باید از {name2} بزرگتر بیشتر باشد";
        }  
               
        /// <summary>
        /// از ... کوچکتر باشد.
        /// </summary>
        /// <param name="name1"></param>
        /// <param name="name2"></param>
        /// <returns></returns>
        public static string IsLessThan(string paramName, string name2)
        {
            return $"{paramName} باید از {name2}  کوچکتر باشد";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string NotNegetive(string paramName)
        {
            return $"{paramName} منفی نباید باشد.";
        }

        public static string MaxLength(string name, int length)
        {
            return $"طول {name} از {length} بیشتر است";
        }

        public static string EqualLength(string name, int length)
        {
            return $"طول {name} باید {length} باشد";
        }

        public static string IsMandatory(string name)
        {
            return $"وارد کردن {name} الزامی است !!!";
        }

        public static string Useing(string name)
        {
            return $"{name} مورد استفاده قرار گرفته شده است";
        }

    }
}

