using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RhiultaUI
{
    public class ValidatableModel : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool _changed;

        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            ValidateAsync();
        }

        public void OnErrorsChanged(string propertyName)
        {
            var handler = ErrorsChanged;
            if (handler != null)
                handler(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errorsForName;
            if (propertyName == null) return null;
            _errors.TryGetValue(propertyName, out errorsForName);
            return errorsForName;
        }

        public bool HasErrors
        {
            get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }

        public bool HasChanged
        {
            get; set;
        }

        public Task ValidateAsync()
        {
            return Task.Run(() => Validate());
        }

        private object _lock = new object();
        public void Validate()
        {
            lock (_lock)
            {
                var validationContext = new ValidationContext(this, null, null);
                var validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (var kv in _errors.ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        List<string> outLi;
                        _errors.TryRemove(kv.Key, out outLi);
                        OnErrorsChanged(kv.Key);
                    }
                }

                var q = from r in validationResults
                        from m in r.MemberNames
                        group r by m into g
                        select g;

                foreach (var prop in q)
                {
                    var messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        List<string> outLi;
                        _errors.TryRemove(prop.Key, out outLi);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    OnErrorsChanged(prop.Key);
                }
            }
        }
    }

    public class Helper
    {
        public static bool PublicInstancePropertiesEqual<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                var xd1 = type.GetProperties();

                foreach(var pi in xd1)
                {
                    var a1 = pi.GetValue(self);
                    var a2 = pi.Name;
                    var a3 = type.GetProperty(pi.Name).GetValue(self, null);

                    //Console.WriteLine(a1);
                    Console.WriteLine(a1);

                    if (!ignoreList.Contains(pi.Name))
                    {
                        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                        var selfValueType = selfValue?.GetType().GetTypeInfo();
                        var toValueType = toValue?.GetType().GetTypeInfo();

                        if ((toValueType?.IsClass) == true)
                        {
                            if (toValueType.Name != "String")
                            {
                                var xd = Helper.PublicInstancePropertiesEqual(selfValue, toValue, new string[] { "HasChanged", "HasErrors" });
                                return xd;
                            }
                        }

                        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                        {
                            return false;
                        }
                    }
                }

                //foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance ))
                //{

                //    if (!ignoreList.Contains(pi.Name))
                //    {
                //        object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                //        object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                //        var selfValueType = selfValue?.GetType().GetTypeInfo();
                //        var toValueType = toValue?.GetType().GetTypeInfo();

                //        if ((toValueType?.IsClass) == true)
                //        {
                //            if(toValueType.Name != "String")
                //            {
                //                var xd = Helper.PublicInstancePropertiesEqual(selfValue, toValue, new string[] { "HasChanged", "HasErrors" });
                //                Console.WriteLine(xd);
                //                return xd;
                //            }
                //        }

                //        //Console.WriteLine($"selfValue: {selfValue} toValue: {toValue}");

                //        if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                //        {
                //            return false;
                //        }
                //    }
                //}
                return true;
            }
            return self == to;
        }
    }
}
