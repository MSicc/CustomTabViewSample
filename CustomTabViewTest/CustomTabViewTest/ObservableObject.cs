//refactored version of https://github.com/lbugnion/mvvmlight/blob/master/GalaSoft.MvvmLight/GalaSoft.MvvmLight%20(PCL)/ObservableObject.cs

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;


namespace SliMvvm.Core
{
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public event PropertyChangingEventHandler? PropertyChanging;


        public virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            VerifyPropertyName(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = GetPropertyName(propertyExpression);
            if (!string.IsNullOrEmpty(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void RaisePropertyChanging([CallerMemberName] string? propertyName = null)
        {
            VerifyPropertyName(propertyName);

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        public virtual void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = GetPropertyName(propertyExpression);
            if (!string.IsNullOrEmpty(propertyName))
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        public void VerifyPropertyName(string? propertyName)
        {
            var info = GetType().GetTypeInfo();

            if (!string.IsNullOrEmpty(propertyName) && info.GetDeclaredProperty(propertyName) == null)
            {
                // Check base types
                var found = false;

                while (info.BaseType != typeof(object))
                {
                    info = info.BaseType.GetTypeInfo();

                    if (info.GetDeclaredProperty(propertyName) != null)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    throw new ArgumentException("Property not found", propertyName);
                }
            }
        }


        protected PropertyChangedEventHandler? PropertyChangedEventHandler => PropertyChanged;
        protected PropertyChangingEventHandler? PropertyChangingEventHandler => PropertyChanging;


        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            if (!(propertyExpression.Body is MemberExpression body))
                throw new ArgumentException("Invalid argument", nameof(propertyExpression));

            if (body.Member is PropertyInfo property)
                return property.Name;
            else
                throw new ArgumentException("Argument is not a property", nameof(propertyExpression));
        }

        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            RaisePropertyChanging(propertyExpression);
            field = newValue;
            RaisePropertyChanged(propertyExpression);
            return true;
        }

        protected bool Set<T>(string? propertyName, ref T field, T newValue)
        {
            if (string.IsNullOrEmpty(propertyName))
                return false;

            if (EqualityComparer<T>.Default.Equals(field, newValue))
                return false;

            RaisePropertyChanging(propertyName);
            field = newValue;
            RaisePropertyChanged(propertyName);

            return true;
        }

        protected bool Set<T>(ref T field, T newValue, [CallerMemberName]string? propertyName = null) => Set(propertyName, ref field, newValue);

    }
}