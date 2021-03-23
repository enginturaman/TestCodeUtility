using System;
using System.Linq;
using System.Reflection;
using TestCodeUtility.Models;

namespace TestCodeUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            var code = ConvertCurrency<VakifKatilimCurrencyStrings>("Rus Rublesi");
        }

        //Belli bir class a göre 
        public static string ConvertCurrency<T>(string currrencyCode) where T : new() 
        {
            try
            {
                //VakifKatilimCurrencyStrings sınıfının statik fieldlarının adı ve değerleri dictionary'e atılır.
                var currencyDic = typeof(T)
                    .GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Where(f => f.FieldType == typeof(string))
                    .ToDictionary(f => f.Name, f => (string)f.GetValue(null));

                var currency = currencyDic.FirstOrDefault(x => x.Value.Equals(currrencyCode));

                if (!String.IsNullOrEmpty(currency.Key))
                {
                    return currency.Key;
                }
                else
                    return currrencyCode;
            }
            catch
            {
                return currrencyCode;
            }
        }
    }
}
